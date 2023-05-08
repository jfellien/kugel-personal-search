param location string
@minLength(2)
@maxLength(60)
param apiWebAppName string
@minLength(1)
@maxLength(260)
param apiAppInsightsName string
@minLength(3)
@maxLength(24)
param frontendName string
@minLength(3)
@maxLength(24)
param keyVaultName string
@minLength(1)
@maxLength(260)
param frontDoorName string
@minLength(1)
@maxLength(63)
param sqlServerName string
@minLength(1)
@maxLength(128)
param sqlDatabaseName string
param sqlAdministratorUserName string
@secure()
param sqlAdministratorPassword string
param publicDomainName string
param isDevEnvironment bool

var roleDefinitions = loadJsonContent('../roleDefinitions.json')
var sqlServerConnectionStringSecretName = '${sqlServerName}-ConnectionString'

var apiPathBaseName = 'api'

module keyVault '../.bicep/keyvault.bicep' = {
  name: 'staff-search-keyVault'
  params: {
    location: location
    keyVaultName: keyVaultName
  }
}

module frontDoor '../.bicep/frontdoor.bicep' = {
  name: 'staff-search-frontDoor'
  params:{
    location: 'Global'
    frontDoorName: frontDoorName
    domainName: publicDomainName
  }
}

module sqlServer '../.bicep/sql-server.bicep' = {
  name: 'staff-search-sql-server'
  params: {
    location: location
    dbServerName: sqlServerName
    administratorUsername: sqlAdministratorUserName
    administratorPassword: sqlAdministratorPassword
  }
}

module sqlServerDatabase '../.bicep/sql-db.bicep' = {
  name: 'staff-search-sql-database'
  params: {
      location: location
      sqlServerName: sqlServer.outputs.name
      sqlDbName: sqlDatabaseName
  }
  dependsOn:[
    sqlServer
  ]
}

module sqlServerConnectionSecret '../.bicep/add-keyvault-secret.bicep' = {
  name: 'add-sql-server-connection-to-key-vault'
  params:{
    keyVaultName: keyVault.outputs.keyVaultName
    secretName: sqlServerConnectionStringSecretName
    secretValue: 'Server=tcp:${sqlServer.outputs.name}${environment().suffixes.sqlServerHostname},1433;Initial Catalog=${sqlServerDatabase.outputs.name};Persist Security Info=False;User ID=${sqlAdministratorUserName};Password=${sqlAdministratorPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
  }
  dependsOn:[
    keyVault
    sqlServer
    sqlServerDatabase
  ]
}

module deploymentUser '../.bicep/user-identity.bicep' = {
  name: 'static-web-user-identity'
  params:{
    location: location
    userIdentityName: 'deployer'
  }
}

module deploymentUserAsContributor '../.bicep/role-assignment.bicep' = {
  name: 'set-deployment-user-as-contributor'
  params: {
    principalId: deploymentUser.outputs.principalId
    roleDefinitionId: roleDefinitions.contributor
  }
  dependsOn:[
    deploymentUser
  ]
}

module frontend '../.bicep/staticweb.bicep' = {
  name: 'staff-search-frontend'
  params: {
    location: location
    staticWebSiteName: frontendName
    userId: deploymentUser.outputs.id
  }
  dependsOn:[
    deploymentUserAsContributor
  ]
}

module frontendRoute '../.bicep/frontdoor-route.bicep' = {
  name: 'staff-search-frontend-route'
  params: {
    frontDoorName: frontDoor.outputs.name
    originHostName: frontend.outputs.hostName
    originGroupName: 'frontend'
    patternsToMatch: [
      '/*'
      '/'
    ]
    isCachingEnabled: true
    isCompressionEnabled: false
  }
  dependsOn:[
    frontDoor
    frontend
  ]
}

module apiAppInsights '../.bicep/application-insights.bicep' = {
  name: 'staff-search-api-app-insights'
  params:{
    location: location
    appInsightsName: apiAppInsightsName
  }
}

module apiService '../.bicep/app-service.bicep' = {
  name: 'staff-search-api'
  params: {
    location: location
    appServiceName: apiWebAppName
    appInsightsConnectionString: apiAppInsights.outputs.connectionString
    securityRestrictions: [
      {
        ipAddress: 'AzureFrontDoor.Backend'
        tag: 'ServiceTag'
        action: 'Allow'
        priority: 100
        name: 'Frontdoor'
        headers: {
          'x-azure-fdid': [
            frontDoor.outputs.id
          ]
        }
      }
    ]
    additionalAppSettings: [
      {
        name: 'SqlServerConnection'
        value: '@Microsoft.KeyVault(SecretUri=${sqlServerConnectionSecret.outputs.secretUri})'
      }
      {
        name: 'APIPathBase'
        value: '/${apiPathBaseName}'
      }
    ]
    allowedOrigins: (isDevEnvironment ? ['http://localhost:4200'] : [])
  }
  dependsOn:[
    apiAppInsights
    frontDoor
    sqlServerConnectionSecret
  ]
}

module apiRoute '../.bicep/frontdoor-route.bicep' = {
  name: 'staff-search-api-frontdoor-route'
  params: {
    frontDoorName: frontDoor.outputs.name
    originHostName: apiService.outputs.hostName
    originGroupName: apiPathBaseName
    patternsToMatch: [
      '/${apiPathBaseName}/*'
      '/${apiPathBaseName}/'
    ]
    isCachingEnabled: false
    isCompressionEnabled: false
  }
  dependsOn:[
    frontDoor
    apiService
  ]
}

module assignApiServiceToKeyVault '../.bicep/keyvault-principal-assignment.bicep' = {
  name: 'assign-staff-search-api-to-keyvault'
  params:{
    keyVaultName:keyVault.outputs.keyVaultName
    principalId: apiService.outputs.principalId
  }
  dependsOn:[
    keyVault
    apiService
  ]
}

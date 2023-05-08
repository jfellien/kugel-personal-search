targetScope = 'subscription'

param location string
param resourceGroupName string
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

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: location
}

module services 'staff-search-resources.bicep' = {
  name: 'staff-search-resources'
  scope: resourceGroup
  params: {
    location: location
    apiWebAppName: apiWebAppName
    apiAppInsightsName: apiAppInsightsName
    frontendName: frontendName
    keyVaultName: keyVaultName
    frontDoorName: frontDoorName
    sqlServerName: sqlServerName
    sqlDatabaseName: sqlDatabaseName
    sqlAdministratorUserName: sqlAdministratorUserName
    sqlAdministratorPassword: sqlAdministratorPassword
    publicDomainName: publicDomainName
    isDevEnvironment: endsWith(resourceGroupName, '-dev')
  }
}

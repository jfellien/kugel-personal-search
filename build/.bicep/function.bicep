param location string
param appInsightsConnection string
param functionAppStorageConnection string
@minLength(2)
@maxLength(60)
param functionName string
param skuName string
param skuTier string
param allowedOrigins array = []
param additionalAppSettings array = []

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: functionName
  location: location
  kind: 'linux'
  properties: {
    reserved: true
  }
  sku: {
    name: skuName
    tier: skuTier
  }
}

resource functionApp 'Microsoft.Web/sites@2021-02-01' = {
  name: functionName
  location: location
  kind: 'functionapp,linux'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    httpsOnly: true
    serverFarmId: appServicePlan.id
    clientAffinityEnabled: true
    siteConfig: {
      cors: {
        allowedOrigins: allowedOrigins
      }
      linuxFxVersion: 'DOTNET|6.0'
      appSettings: union([
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsightsConnection
        }
        {
          name: 'AzureWebJobsStorage'
          value: functionAppStorageConnection
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
      ], additionalAppSettings)
    }
  }
}

output name string = functionApp.name
output principalId string = functionApp.identity.principalId
output hostName string = 'https://${functionApp.properties.defaultHostName}'

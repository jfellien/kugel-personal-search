param location string
@minLength(2)
@maxLength(60)
param appServiceName string
param appInsightsConnectionString string
param allowedOrigins array = []
param additionalAppSettings array = []
param securityRestrictions array = []
param linuxFxVersion string = 'DOTNETCORE|6.0'

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  kind: 'linux'
  name: appServiceName
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: 'S1'
  }
}

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: appServiceName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    httpsOnly: true
    serverFarmId: appServicePlan.id
    siteConfig: {
      alwaysOn: true
      http20Enabled: true
      autoHealEnabled: true
      linuxFxVersion: linuxFxVersion
      ftpsState: 'Disabled'
      cors: {
        allowedOrigins: allowedOrigins 
      }
      ipSecurityRestrictions: securityRestrictions
      appSettings: union([
        {
          name: 'WEBSITES_ENABLE_APP_SERVICE_STORAGE'
          value: 'false'
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsightsConnectionString
        }
      ], additionalAppSettings)
    }
  }
}

output hostName string = appService.properties.defaultHostName
output principalId string = appService.identity.principalId

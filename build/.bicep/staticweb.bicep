param location string
@minLength(3)
@maxLength(24)
param staticWebSiteName string
param userId string

resource staticWebSiteStorage 'Microsoft.Storage/storageAccounts@2021-06-01' = {
  name: staticWebSiteName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

resource deploymentScript 'Microsoft.Resources/deploymentScripts@2020-10-01' = {
  name: 'EnableStaticWebsiteInStorageAccount'
  location: location
  kind: 'AzureCLI'
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${userId}': {}
    }
  }
  properties: {
    azCliVersion: '2.26.0'
    scriptContent: 'az storage blob service-properties update --account-name ${staticWebSiteStorage.name} --static-website --404-document index.html --index-document index.html'    
    retentionInterval: 'P1D'
  }
}

var webSiteEndpoint  = substring(staticWebSiteStorage.properties.primaryEndpoints.web, 0, length(staticWebSiteStorage.properties.primaryEndpoints.web) - 1)

output endpoint string = webSiteEndpoint
output hostName string = substring(webSiteEndpoint, 8)

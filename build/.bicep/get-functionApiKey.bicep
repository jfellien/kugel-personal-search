param functionAppName string

resource functionApp 'Microsoft.Web/sites@2021-02-01' existing = {
  name: functionAppName
}

output defaultApiKey string = listKeys('${functionApp.id}/host/default', '2016-08-01').functionKeys.default

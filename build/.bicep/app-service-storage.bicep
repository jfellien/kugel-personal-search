param location string
@minLength(3)
@maxLength(24)
param storageName string

resource storage 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: storageName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

var primaryKey = storage.listKeys().keys[0].value

output connectionString string = 'DefaultEndpointsProtocol=https;AccountName=${storageName};AccountKey=${primaryKey};EndpointSuffix=core.windows.net'

param location string
@minLength(3)
@maxLength(24)
param storageName string
param containerNames array = []

resource storage 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: storageName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

resource blobStorageService 'Microsoft.Storage/storageAccounts/blobServices@2021-06-01' = {
  name: 'default'
  parent: storage
}

resource containers 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = [for name in containerNames: {
  name: name
  parent: blobStorageService
}]

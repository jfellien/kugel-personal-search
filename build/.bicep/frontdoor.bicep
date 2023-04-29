param location string = 'Global'
@minLength(1)
@maxLength(260)
param frontDoorName string
param domainName string

resource frontDoor 'Microsoft.Cdn/profiles@2021-06-01' = {
  name: frontDoorName
  location: location
  sku: {
    name: 'Standard_AzureFrontDoor'
  }
  resource endpoint 'afdEndpoints' = {
    name: frontDoorName
    location: 'Global'
  }
}

resource customDomain 'Microsoft.Cdn/profiles/customdomains@2021-06-01' = {
  parent: frontDoor
  name: frontDoorName
  properties: {
    hostName: domainName
    tlsSettings: {
      certificateType: 'ManagedCertificate'
      minimumTlsVersion: 'TLS12'
    }
  }
}

output id string = frontDoor.properties.frontDoorId
output name string = frontDoor.name

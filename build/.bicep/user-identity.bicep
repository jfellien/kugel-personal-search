param location string
param userIdentityName string

resource userAssignedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2018-11-30' = {
  location: location
  name: userIdentityName
}

output id string = userAssignedIdentity.id
output principalId string = userAssignedIdentity.properties.principalId

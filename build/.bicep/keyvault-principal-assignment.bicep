param keyVaultName string
param principalId string

resource keyVault 'Microsoft.KeyVault/vaults@2021-04-01-preview' existing = {
  name: keyVaultName
}

resource keyVaultAssignment 'Microsoft.Authorization/roleAssignments@2020-08-01-preview' = {
  name: guid(subscription().subscriptionId, principalId)
  scope: keyVault
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '4633458b-17de-408a-b874-0445c86b69e6')
    principalId: principalId
    principalType: 'ServicePrincipal'
  }
  dependsOn: [
    keyVault
  ]
}

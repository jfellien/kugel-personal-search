param keyVaultName string
param secretName string
@secure()
param secretValue string

resource keyVault 'Microsoft.KeyVault/vaults@2021-06-01-preview' existing = {
  name: keyVaultName
  resource keyvaultSecret 'secrets' = {
    name: secretName
    properties: {
      value:  secretValue
    }
  }
}

output secretUri string = keyVault::keyvaultSecret.properties.secretUri

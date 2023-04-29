param location string
@minLength(1)
@maxLength(63)
param dbServerName string
param administratorUsername string
@secure()
param administratorPassword string

resource sqlServer 'Microsoft.Sql/servers@2021-05-01-preview' = {
  name: dbServerName
  location: location
  properties: {
    administratorLogin: administratorUsername
    administratorLoginPassword: administratorPassword
  }
  resource fwRule 'firewallRules' = {
    name: '${dbServerName}-fwAzure'
    properties: {
      startIpAddress: '0.0.0.0'
      endIpAddress: '0.0.0.0'
    }
  }  
}

output name string = sqlServer.name
output hostname string = sqlServer.properties.fullyQualifiedDomainName

param location string

param sqlServerName string
@minLength(1)
@maxLength(128)
param sqlDbName string

resource sqlServer 'Microsoft.Sql/servers@2021-05-01-preview' existing = {
    name: sqlServerName
}

resource sqlDb 'Microsoft.Sql/servers/databases@2022-05-01-preview' = {
    parent: sqlServer
    name: sqlDbName
    location: location
    properties: {
        collation: 'SQL_Latin1_General_CP1_CI_AS'
    }
    sku: {
        capacity: 10
        name: 'Standard'
        tier: 'Standard'
    }
}

output name string = sqlDb.name

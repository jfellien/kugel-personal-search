param location string
@minLength(6)
@maxLength(50)
param serviceBusName string
param topics array = []

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-06-01-preview' = {
  name: serviceBusName
  location: location
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
}

module serviceBusTopics 'servicebus-topic.bicep' = [for topic in topics: {
  name: 'serviceBusTopic-${topic.name}'
  params: {
    topicName: topic.name
    subscriptions: topic.subscriptions
    namespaceName: serviceBusNamespace.name
  }
}]

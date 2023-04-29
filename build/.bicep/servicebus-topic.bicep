@minLength(1)
@maxLength(260)
param topicName string
@minLength(1)
@maxLength(50)
param subscriptions array
param namespaceName string

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-06-01-preview' existing = {
  name: namespaceName
}

resource serviceBusTopic 'Microsoft.ServiceBus/namespaces/topics@2021-06-01-preview' = {
  name: topicName
  parent: serviceBusNamespace
}

resource serviceBusSubscription 'Microsoft.ServiceBus/namespaces/topics/subscriptions@2021-06-01-preview' = [for subscription in subscriptions: {
  name: subscription
  parent: serviceBusTopic
}]

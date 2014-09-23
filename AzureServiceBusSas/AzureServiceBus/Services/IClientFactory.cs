using Microsoft.ServiceBus.Messaging;
using System;

namespace AzureServiceBus.Services
{
    public interface IClientFactory
    {
        TopicClient CreateTopicClient(string nsName, string topicName, string keyName, string key);
        SubscriptionClient CreateSubscriptionClient(string nsName, string topicName, string subscriptionName, string keyName, string key);
    }
}

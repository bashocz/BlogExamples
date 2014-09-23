using System.Threading.Tasks;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus.Messaging;

namespace AzureServiceBus.Services
{
    public class TokenProviderBasedClientFactory : IClientFactory
    {
        private TokenProvider GetTokenProvider(string keyName, string key)
        {
            TokenProvider tp = TokenProvider.CreateSharedAccessSignatureTokenProvider(keyName, key);
            return tp;
        }

        public TopicClient CreateTopicClient(string nsName, string topicName, string keyName, string key)
        {
            Uri runtimeUri = ServiceBusEnvironment.CreateServiceUri("sb", nsName, string.Empty);

            TokenProvider tp = GetTokenProvider(keyName, key);

            MessagingFactory mf = MessagingFactory.Create(runtimeUri, tp);
            return mf.CreateTopicClient(topicName);
        }

        public SubscriptionClient CreateSubscriptionClient(string nsName, string topicName, string subscriptionName, string keyName, string key)
        {
            Uri runtimeUri = ServiceBusEnvironment.CreateServiceUri("sb", nsName, string.Empty);

            TokenProvider tp = GetTokenProvider(keyName, key);

            MessagingFactory mf = MessagingFactory.Create(runtimeUri, tp);
            return mf.CreateSubscriptionClient(topicName, subscriptionName);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus.Messaging;

namespace AzureServiceBus.Services
{
    public class ConnectionStringBasedClientFactory : IClientFactory
    {
        public IConnectionStringProvider _csp;

        public ConnectionStringBasedClientFactory(IConnectionStringProvider csp)
        {
            if (csp == null)
                throw new ArgumentNullException("csp");

            _csp = csp;
        }

        public TopicClient CreateTopicClient(string nsName, string topicName, string keyName, string key)
        {
            string cs = _csp.GetConnectionString(nsName, keyName, key);
            MessagingFactory mf = MessagingFactory.CreateFromConnectionString(cs);
            return mf.CreateTopicClient(topicName);
        }

        public SubscriptionClient CreateSubscriptionClient(string nsName, string topicName, string subscriptionName, string keyName, string key)
        {
            string cs = _csp.GetConnectionString(nsName, keyName, key);
            MessagingFactory mf = MessagingFactory.CreateFromConnectionString(cs);
            return mf.CreateSubscriptionClient(topicName, subscriptionName);
        }
    }
}
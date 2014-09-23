using System.Threading.Tasks;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus.Messaging;

namespace AzureServiceBus.Services
{
    public class AsbUsingNamespaceKeysService : BaseAsbService
    {
        private const string topicName = "my-ns-topic";

        private const string topicSubName = "my-ns-topic-sub";

        public AsbUsingNamespaceKeysService(IClientFactory cf, IConnectionStringProvider csp)
            : base(cf, csp) { }

        protected override void CreateTopic(NamespaceManager nsm)
        {
            if (nsm.TopicExists(topicName))
                nsm.DeleteTopic(topicName);

            if (!nsm.TopicExists(topicName))
            {
                // this topic will use Namespace related Shared Access Key
                TopicDescription td = new TopicDescription(topicName);
                nsm.CreateTopic(td);
            }

            _tc = _cf.CreateTopicClient(nsName, topicName, nsKeyName, nsKey);


            if (!nsm.SubscriptionExists(topicName, topicSubName))
            {
                nsm.CreateSubscription(topicName, topicSubName);
            }
            _sc = _cf.CreateSubscriptionClient(nsName, topicName, topicSubName, nsKeyName, nsKey);
        }
    }
}
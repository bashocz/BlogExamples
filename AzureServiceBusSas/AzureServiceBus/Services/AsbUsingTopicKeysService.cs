using System.Threading.Tasks;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus.Messaging;

namespace AzureServiceBus.Services
{
    public class AsbUsingTopicKeysService : BaseAsbService
    {
        private const string topicName = "my-topic";

        private const string topicSubName = "my-topic-sub";

        // topic specific shared access keys
        private const string tsKeyName = "TopicSendKey";
        private string tsKey; // generated in the code
        private const string tlKeyName = "TopicListenKey";
        private string tlKey; // generated in the code

        public AsbUsingTopicKeysService(IClientFactory cf, IConnectionStringProvider csp)
            : base(cf, csp) { }

        protected override void CreateTopic(NamespaceManager nsm)
        {
            if (nsm.TopicExists(topicName))
                nsm.DeleteTopic(topicName);

            if (!nsm.TopicExists(topicName))
            {
                // this topic will use Topic related Shared Access Keys
                TopicDescription td = new TopicDescription(topicName);

                // create a new SAS for topic
                tsKey = SharedAccessAuthorizationRule.GenerateRandomKey();
                td.Authorization.Add(new SharedAccessAuthorizationRule(tsKeyName, tsKey, new[] { AccessRights.Send }));
                tlKey = SharedAccessAuthorizationRule.GenerateRandomKey();
                td.Authorization.Add(new SharedAccessAuthorizationRule(tlKeyName, tlKey, new[] { AccessRights.Listen }));

                nsm.CreateTopic(td);

            }

            _tc = _cf.CreateTopicClient(nsName, topicName, tsKeyName, tsKey);


            if (!nsm.SubscriptionExists(topicName, topicSubName))
            {
                nsm.CreateSubscription(topicName, topicSubName);
            }
            _sc = _cf.CreateSubscriptionClient(nsName, topicName, topicSubName, tlKeyName, tlKey);
        }
    }
}
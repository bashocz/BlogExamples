using AzureServiceBus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace AzureServiceBus.Controllers
{
    public class DependencyResolver : IDependencyResolver
    {
        IAsbService _nsKeyService;
        IAsbService NsKeyService
        {
            get { return _nsKeyService ?? (_nsKeyService = new AsbUsingNamespaceKeysService(new ConnectionStringBasedClientFactory(new ConnectionStringProvider()), new ConnectionStringProvider())); }
        }

        IAsbService _tKeyService;
        IAsbService TKeyService
        {
            get { return _tKeyService ?? (_tKeyService = new AsbUsingTopicKeysService(new TokenProviderBasedClientFactory(), new ConnectionStringProvider())); }
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.Equals(typeof(MessageController)))
            {
                return new MessageController( NsKeyService);
            }
            else if (serviceType.Equals(typeof(NotificationController)))
            {
                return new NotificationController(TKeyService);
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType.Equals(typeof(MessageController)))
            {
                return new List<object> { new MessageController(NsKeyService) };
            }
            else if (serviceType.Equals(typeof(NotificationController)))
            {
                return new List<object> { new NotificationController(TKeyService) };
            }
            return new List<object>();
        }

        public void Dispose()
        {

        }
    }
}
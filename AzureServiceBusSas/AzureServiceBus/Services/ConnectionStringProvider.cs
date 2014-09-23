using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureServiceBus.Services
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        // this method creates connection string
        // but it could be defined in the web.config as well
        public string GetConnectionString(string nsName, string keyName, string key)
        {
            string cs = string.Format("Endpoint=sb://{0}.servicebus.windows.net/;SharedAccessKeyName={1};SharedAccessKey={2}", nsName, keyName, key);
            return cs;
        }

    }
}
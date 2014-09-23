using System;

namespace AzureServiceBus.Services
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(string nsName, string keyName, string key);
    }
}

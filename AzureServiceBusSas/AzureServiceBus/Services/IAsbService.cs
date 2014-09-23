using System;

namespace AzureServiceBus.Services
{
    public interface IAsbService
    {
        void SendMessage(string message);
        string ReceiveMessage();
    }
}

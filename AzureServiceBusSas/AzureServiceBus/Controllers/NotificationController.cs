using AzureServiceBus.Models;
using AzureServiceBus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureServiceBus.Controllers
{
    public class NotificationController : ApiController
    {
        private IAsbService _sb;

        public NotificationController(IAsbService sb)
        {
            if (sb == null)
                throw new ArgumentNullException("sb");

            _sb = sb;
        }

        public void Send(AsbMessage msg)
        {
            _sb.SendMessage(msg.Message);
        }
    }
}

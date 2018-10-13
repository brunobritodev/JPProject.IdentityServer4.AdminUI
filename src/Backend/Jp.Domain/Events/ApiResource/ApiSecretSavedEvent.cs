using System;
using System.Collections.Generic;
using System.Text;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.ApiResource
{

    public class ApiSecretSavedEvent : Event
    {
        public string ResourceName { get; }

        public ApiSecretSavedEvent(int id, string resourceName)
        {
            AggregateId = id.ToString();
            ResourceName = resourceName;
        }
    }
}

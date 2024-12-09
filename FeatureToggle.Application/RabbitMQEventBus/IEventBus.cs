using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Application.RabbitMQEventBus
{
    public interface IEventBus
    {
        Task Publish(string routingKey, object @event);
    }
}

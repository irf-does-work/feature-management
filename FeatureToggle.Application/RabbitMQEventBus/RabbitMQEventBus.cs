using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FeatureToggle.Application.RabbitMQEventBus
{
    public class RabbitMQEventBus
    {
        private readonly ConnectionFactory _connectionFactory;
        private string ExchangeName = "FeatureEvents";
        public RabbitMQEventBus(string hostname)
            //string username, string password)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = hostname,
                //UserName = username,
                //Password = password
            };
            
        }

        public async Task Publish(string routingKey, object @event)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(exchange: ExchangeName, type: ExchangeType.Fanout);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: ExchangeName, routingKey: string.Empty, body: body);
            Console.WriteLine($"[RabbitMQ] Published Event: {message}");
        }
    }
}

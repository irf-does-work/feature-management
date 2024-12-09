using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FeatureToggle.Application.RabbitMQEventBus
{
    public class RabbitMQPublisher : IEventBus
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _exchangeName ;
        public RabbitMQPublisher(string hostname, string exchangeName)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(hostname),
            };
            _exchangeName = exchangeName;
        }

        public async Task Publish(string routingKey, object @event)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync("update-toggle-q",
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
            //await channel.ExchangeDeclareAsync(exchange: _exchangeName, type: ExchangeType.Fanout);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync("", routingKey: "update-toggle-q", body: body);
            Console.WriteLine($"[RabbitMQ] Published Event successfullyyyyyyyyyyy!!!!: {message}");
        }
    }
}

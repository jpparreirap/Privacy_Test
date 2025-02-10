using ECommerceProcessamento.Interfaces;
using ECommerceProcessamento.Services;
using RabbitMQ.Client;
using System.Text;

namespace ECommerceProcessamento.Consumers
{
    public class PedidosProcessadosRespostaConsumer : DefaultBasicConsumer
    {
        private IModel _channel;
        private ILogger<PedidosProcessadosRespostaConsumer> _logger;
        public PedidosProcessadosRespostaConsumer(IModel channel, ILogger<PedidosProcessadosRespostaConsumer> logger) : base(channel)
        {
            _channel = channel;
            _logger = logger;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var resposta = Encoding.UTF8.GetString(body.ToArray());
            _logger.LogInformation($"Resposta obtida da outra aplicação: {resposta}");
            _logger.LogInformation("Comunicação concluida");

            _channel.BasicAck(deliveryTag, false);
        }
    }
}

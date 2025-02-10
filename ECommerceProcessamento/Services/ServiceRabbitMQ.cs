using ECommerceProcessamento.DTOs;
using ECommerceProcessamento.Interfaces;
using ECommerceProcessamento.Consumers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ECommerceProcessamento.Services
{
    public class ServiceRabbitMQ : IServiceRabbitMQ
    {
        private IConnection _connection;
        private IModel _channel;
        private ILogger<ServiceRabbitMQ> _logger;
        private string _exchange = "PrivacyTest";

        public ServiceRabbitMQ(IConfiguration config, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ServiceRabbitMQ>();

            _logger.LogInformation("Inicializando Conexao RabbitMQ");

            var factory = new ConnectionFactory()
            {
                HostName = config["RabbitMQ:Servidor"],
                UserName = config["RabbitMQ:Usuario"],
                Password = config["RabbitMQ:Senha"],
                VirtualHost = config["RabbitMQ:VirtualHost"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            InicializaQueues(loggerFactory);
        }

        private void InicializaQueues(ILoggerFactory loggerFactory)
        {
            _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Topic, durable: true, autoDelete: false);

            _channel.QueueDeclare(queue: "PedidosProcessados", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: "PedidosProcessados", exchange: _exchange, "PedidosProcessados");

            _channel.QueueDeclare(queue: "PedidosProcessados.Resposta", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: "PedidosProcessados.Resposta", exchange: _exchange, "PedidosProcessados.Resposta");
            _channel.BasicConsume(queue: "PedidosProcessados.Resposta", autoAck: false, new PedidosProcessadosRespostaConsumer(_channel, loggerFactory.CreateLogger<PedidosProcessadosRespostaConsumer>()));
        }

        public async Task PublicarPedidoProcessadoAsync(PedidoProcessadoDTO pedido)
        {
            var mensagem = JsonConvert.SerializeObject(pedido);
            var body = Encoding.UTF8.GetBytes(mensagem);
            string queueName = "PedidosProcessados";
            var properties = _channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.Headers = new Dictionary<string, object?>()
            {
                { "datahora", DateTime.Now.ToFileTime() }
            };

            
            _logger.LogInformation($"Publicando para a fila {queueName}");

            await Task.Run(() =>
            {
                _channel.BasicPublish(exchange: _exchange, routingKey: queueName, basicProperties: properties, body: body);
            });
            
            _logger.LogInformation($"Mensagem enviada: {mensagem}");
        }
    }
}

using ECommerceProcessamento.Database.Settings;
using ECommerceProcessamento.Entities;
using ECommerceProcessamento.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerceProcessamento.Database.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger<MongoDbContext> _logger;

        public MongoDbContext(IOptions<MongoDbSettings> settings, ILogger<MongoDbContext> logger)
        {
            _logger = logger;
            _logger.LogInformation("Inicializando Banco de dados MongoDB");

            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Produto> Produtos => _database.GetCollection<Produto>("Produtos");
        public IMongoCollection<Pedido> Pedidos => _database.GetCollection<Pedido>("Pedidos");
        public IMongoCollection<Cliente> Clientes => _database.GetCollection<Cliente>("Clientes");
    }
}

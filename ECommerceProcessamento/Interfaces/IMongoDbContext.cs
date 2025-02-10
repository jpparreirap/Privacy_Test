using ECommerceProcessamento.Entities;
using MongoDB.Driver;

namespace ECommerceProcessamento.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<Produto> Produtos { get; }
        IMongoCollection<Cliente> Clientes { get; }
        IMongoCollection<Pedido> Pedidos { get; }
    }
}

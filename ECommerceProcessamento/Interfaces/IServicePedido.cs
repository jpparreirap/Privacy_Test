using ECommerceProcessamento.Entities;
using ECommerceProcessamento.Enums;

namespace ECommerceProcessamento.Interfaces
{
    public interface IServicePedido
    {
        Task<List<Pedido>> ObterPedidosPorStatusAsync(StatusProcessamento statusPendente);
        Task ProcessarPedidosPendentesAsync(Pedido pedido);
    }
}

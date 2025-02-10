using ECommerceProcessamento.DTOs;

namespace ECommerceProcessamento.Interfaces
{
    public interface IServiceRabbitMQ
    {
        Task PublicarPedidoProcessadoAsync(PedidoProcessadoDTO pedido);
    }
}

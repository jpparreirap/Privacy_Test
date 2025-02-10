using ECommerceProcessamento.DTOs;
using ECommerceProcessamento.Entities;
using ECommerceProcessamento.Enums;
using ECommerceProcessamento.Interfaces;
using MongoDB.Driver;

namespace ECommerceProcessamento.Services
{
    public class ServicePedido : IServicePedido
    {
        private readonly IServiceRabbitMQ _serviceRabbitMQ;
        private readonly IMongoDbContext _dbContext;
        private readonly ILogger<ServicePedido> _logger;
        public ServicePedido(IMongoDbContext dbContext, ILogger<ServicePedido> logger, IServiceRabbitMQ serviceRabbitMQ)
        {
            _dbContext = dbContext;
            _logger = logger;
            _serviceRabbitMQ = serviceRabbitMQ;
        }

        public async Task<List<Pedido>> ObterPedidosPorStatusAsync(StatusProcessamento statusPendente)
        {
            List<Pedido> pedidos = await _dbContext.Pedidos.Find(p => p.StatusProcessamento == statusPendente).ToListAsync();

            return pedidos;
        }

        public async Task ProcessarPedidosPendentesAsync(Pedido pedido)
        {
            Cliente cliente = pedido.Cliente;
            decimal descontoAplicado = 0;
            _logger.LogInformation($"Processando pedido {pedido.Id}, cliente {cliente.Nome}");

            CalcularDescontoOuAcrescimoMeioDePagamento(ref pedido, ref descontoAplicado);
            CalcularDescontoComAssinatura(ref pedido, cliente, ref descontoAplicado);
            CalcularFrete(ref pedido, cliente);
            AtualizarQuantidadeEmEstoque(ref pedido);
            AtualizarStatus(ref pedido);

            PedidoProcessadoDTO pedidoProcessado = new PedidoProcessadoDTO()
            {
                 Id  = pedido.Id,
                 Cliente = pedido.Cliente,
                 Produtos = pedido.Produtos,
                 TotalFinal = Math.Round(pedido.TotalPedido, 2),
                 DescontoAplicado = descontoAplicado,
                 StatusProcessamento = pedido.StatusProcessamento,
                 SituacaoPagamento = SituacaoPagamento.Pago,
                 ProcessadoEm = DateTime.Now
            };

            _logger.LogInformation($"Pedido processado");

            await _serviceRabbitMQ.PublicarPedidoProcessadoAsync(pedidoProcessado);
        }

        private static void CalcularFrete(ref Pedido pedido, Cliente cliente)
        {
            if (pedido.TotalPedido > 300 || cliente.Assinatura.EstaAtiva)
                return;

            pedido.TotalPedido += (10 + Convert.ToDecimal(pedido.DistanciaEmKM * 2.5));
        }

        private static void CalcularDescontoComAssinatura(ref Pedido pedido, Cliente cliente, ref decimal descontoAplicado)
        {
            if (!cliente.Assinatura.EstaAtiva)
                return;

            decimal desconto = 0;

            switch (cliente.Assinatura.QuantidadeDeMesesAtiva)
            {
                case int meses when meses >= 12:
                    desconto = 0.10m;
                    break;
                case int meses when meses >= 6:
                    desconto = 0.05m;
                    break;
                case int meses when meses >= 3:
                    desconto = 0.02m;
                    break;
                default:
                    desconto = 0;
                    break;
            }

            descontoAplicado += desconto;
            pedido.TotalPedido *= (1 - desconto);
        }

        private static void CalcularDescontoOuAcrescimoMeioDePagamento(ref Pedido pedido, ref decimal descontoAplicado)
        {
            if(pedido.MeioDePagamento == MeioDePagamento.Avista)
            {
                descontoAplicado = 0.10m;
                pedido.TotalPedido *= (1 - descontoAplicado);
            }
            else if (pedido.MeioDePagamento.HasFlag(MeioDePagamento.CartaoDeCredito) && pedido.MeioDePagamento.HasFlag(MeioDePagamento.Parcelado))
            {
                if (pedido.NumeroDeParcelas.HasValue && pedido.NumeroDeParcelas > 1)
                {
                    decimal taxaDeJuros = 0.02m;
                    pedido.TotalPedido *= (1 + (taxaDeJuros * Convert.ToDecimal(pedido.NumeroDeParcelas.Value)));
                }
            }
        }

        private static void AtualizarStatus(ref Pedido pedido)
        {
            pedido.StatusProcessamento = StatusProcessamento.Processado;
        }

        private static void AtualizarQuantidadeEmEstoque(ref Pedido pedido)
        {
            var produtosAgrupados = pedido.Produtos.GroupBy(p => p.Nome)
                                                   .Select(g => new
                                                   {
                                                       Nome = g.Key,
                                                       Quantidade = g.Count()
                                                   })
                                                   .ToList();

            foreach (var produtoAgrupado in produtosAgrupados)
            {
                pedido.Produtos.Where(p => p.Nome == produtoAgrupado.Nome)
                               .ToList()
                               .ForEach(produto => produto.QuantidadeEmEstoque -= produtoAgrupado.Quantidade);
            }
        }
    }
}

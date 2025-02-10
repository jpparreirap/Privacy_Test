using Microsoft.AspNetCore.Mvc;
using ECommerceProcessamento.Enums;
using ECommerceProcessamento.Interfaces;

namespace ECommerceProcessamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;

        private IServicePedido _servicePedido;

        public PedidosController(ILogger<PedidosController> logger, IServicePedido servicePedido)
        {
            _logger = logger;
            _servicePedido = servicePedido;
        }

        [HttpGet("processapedido")]
        public async Task<IActionResult> ProcessaPedidosPendentes()
        {
            var pedidos = await _servicePedido.ObterPedidosPorStatusAsync(StatusProcessamento.Pendente);

            if (pedidos == null || !pedidos.Any())
            {
                _logger.LogInformation("Nenhum pedido pendente a ser processado");
                return NoContent();
            }

            foreach (var pedido in pedidos)
            {
                await _servicePedido.ProcessarPedidosPendentesAsync(pedido);
            }

            return Ok();
        }
    }
}

import { ServiceRabbitMQ } from '../Services/ServiceRabbitMQ';
import { IPedido, Pedido } from '../Models/PedidoModel';
import { IProduto, Produto } from '../Models/ProdutoModel';

export class PedidosProcessadosConsumer_Received {
    private serviceRabbitMQ: ServiceRabbitMQ;

    constructor(serviceRabbitMQ: ServiceRabbitMQ) {
        this.serviceRabbitMQ = serviceRabbitMQ;
    }

    public async iniciarConsumer(): Promise<void> {
        try {
            await this.serviceRabbitMQ.consumidorMensagens('PedidosProcessados', this.handleMessage.bind(this));
        } catch (error) {
            console.error('Erro ao iniciar consumer para a fila PedidosProcessados:', error);
        }
    }

    private async handleMessage(message: string): Promise<void> {
        try {
            const pedido = JSON.parse(message);
            console.log('Pedido recebido:', pedido);

            const pedidoId = pedido.Id;

            const updateData: Partial<IPedido> = {
                TotalPedido: pedido.TotalFinal,
                DescontoAplicado: pedido.DescontoAplicado,
                StatusProcessamento: pedido.StatusProcessamento,
                SituacaoPagamento: pedido.SituacaoPagamento,
                DataDeProcessamento: pedido.ProcessadoEm
            };

            const produtosIdUnicos = new Set(pedido.Produtos.map((produto: any) => produto.Id));
            const produtosUnicos =  Array.from(produtosIdUnicos).map(id => {
                const produto = pedido.Produtos.find((p: any) => p.Id === id);
                return {
                    Id: produto.Id,
                    QuantidadeEmEstoque: produto.QuantidadeEmEstoque
                };
            });

            for (const produto of produtosUnicos) {
                const updateData2: Partial<IProduto> = {
                    QuantidadeEmEstoque: produto.QuantidadeEmEstoque
                };
                
                await Produto.findByIdAndUpdate(produto.Id, updateData2);
            }

            await Pedido.findByIdAndUpdate(pedidoId, updateData);
        } catch (error) {
            console.error('Erro ao processar pedido:', error);
            throw error;
        }
    }
}
<template>
    <div class="pedidos-table">
      <b-table
        :data="pedidos"
        :loading="isLoading"
        striped
        hoverable
        paginated
        :per-page="10"
      >
        <b-table-column field="pedidoId" label="Identificador do Pedido" v-slot="props">
            {{ props.row._id }}
        </b-table-column>
  
        <b-table-column field="clienteNome" label="Cliente" v-slot="props">
            {{ props.row.Cliente.Nome }}
        </b-table-column>

        <b-table-column field="clienteContato" label="Contato do cliente (Telefone/E-mail)" v-slot="props">
            <div class="is-flex is-flex-direction-column">
                <p>{{ props.row.Cliente.Telefone }} / {{ props.row.Cliente.Email }}</p>
            </div>
        </b-table-column>

        <b-table-column field="produtosPedidos" label="Items do Pedido - Quantidade" v-slot="props">
            <div class="is-flex is-flex-direction-column">
                <p class="pb-1" v-for="(produto, index) in listaItensDoPedido(props.row.Produtos)" :key="index">
                    {{ produto.Nome }} - {{ produto.QuantidadePedida }}
                </p>
            </div>
        </b-table-column>

        <b-table-column field="statusProcessamento" label="Status" v-slot="props">
            <b-tag :type="getStatusTag(props.row.StatusProcessamento)">
                {{ getStatusLabel(props.row.StatusProcessamento) }}
            </b-tag>
        </b-table-column>

        <b-table-column field="statusPagamento" label="Status Pagamento" v-slot="props">
            <b-tag :type="getStatusTag(props.row.SituacaoPagamento)">
                {{ getStatusPagamentoLabel(props.row.SituacaoPagamento) }}
            </b-tag>
        </b-table-column>

        <b-table-column field="descontoAplicado" label="Desconto Aplicado" v-slot="props">
            {{ props.row.DescontoAplicado * 100 }}%
        </b-table-column>

        <b-table-column field="totalPedido" label="Total" v-slot="props">
            R$ {{ props.row.TotalPedido }}
        </b-table-column>
  
        <b-table-column field="dataProcessamento" label="Data de Processamento" v-slot="props">
            {{ formataData(props.row.DataDeProcessamento) || "Ainda não processado" }}
        </b-table-column>
      </b-table>
    </div>
</template>
  
<style scoped>
    .pedidos-table {
        padding: 20px;
    }
</style>

<script>
    import moment from 'moment';
    export default {
        name: 'PedidosTable',
        props: {
        pedidos: {
            type: Array,
            required: true
        },
        isLoading: {
            type: Boolean,
            default: false
        }
        },
        methods: {
            getStatusLabel(statusProcessamento) {
                return statusProcessamento === 1 ? 'Pendente' : 'Processado';
            },
            getStatusTag(status) {
                return status === 1 ? 'is-warning' : 'is-success';
            },
            getStatusPagamentoLabel(statusPagamento) {
                return statusPagamento === 1 ? 'Pendente' : 'Pago';
            },
            formataData(data) {
                if(!data)
                    return;

                return moment(data).format('DD/MM/YYYY HH:mm:ss');
            },
            listaItensDoPedido(produtos){
                const produtosUnicos = new Set(produtos.map(produto => produto.Nome));

                const produtosUnicosEQuantidadePedida =  Array.from(produtosUnicos).map(nome => {
                    const produto = produtos.find(p => p.Nome === nome);
                    const quantidadePedida = produtos.filter(p => p.Nome === nome).length;
                    console.log(quantidadePedida)
                    return {
                        Nome: produto.Nome,
                        QuantidadePedida: quantidadePedida
                    };
                });

                return produtosUnicosEQuantidadePedida;
            }
        }
    };
</script>
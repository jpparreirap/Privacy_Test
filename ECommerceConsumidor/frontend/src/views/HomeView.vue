<template>
    <div class="container is-fluid">
      <div class="box">
        <h1 class="title has-text-weight-bold has-text-centered">Bem vindo, Drogaria ABCD</h1>
        <h2 class="title mb-1">Relatório geral do centro de distribuição</h2>
        <p>Relatório geral de acompanhamento</p>
  
        <div class="columns is-multiline pt-5">

          <div class="column is-half">
            <StatusProcessamentoChart
                :ckey="chartKey"
                :cDatas="chartDatas"
                :cLabels="chartLabels"
                :colors="chartColors"
                >
            </StatusProcessamentoChart>
          </div>

          <div class="column is-half">
            <div class="columns is-4">
              <div class="column is-half">
                <div class="box">
                  <h3 class="subtitle">Total de produtos em estoque</h3>
                  <p class="title is-4">{{ quantidadeTotalEmEstoque }}</p>
                </div>
              </div>
              <div class="column is-half">
                <div class="box">
                  <h3 class="subtitle">Total de pedidos</h3>
                  <p class="title is-4">{{ pedidos.length }}</p>
                </div>
              </div>
            </div>
            
            <div class="columns is-4">
              <div class="column is-half">
                <div class="box">
                  <h3 class="subtitle">Pedidos Pendentes</h3>
                  <p class="title is-4">{{ quantidadeDePedidosPendentes }}</p>
                </div>
              </div>
              
              <div class="column is-half">
                <div class="box">
                  <h3 class="subtitle">Pedidos Processados</h3>
                  <p class="title is-4">{{ quantidadeDePedidosProcessados }}</p>
                </div>
              </div>
            </div>

          </div>
        </div>
  
        <div class="columns">
          <div class="column is-half">
            <div class="box action-card">
              <h3 class="subtitle mb-1">Cadastrar Produtos</h3>
              <p>Realize o cadastro de novos produtos no estoque</p>
              <button class="button is-primary mt-3">Cadastrar</button>
            </div>
          </div>
          
          <div class="column is-half">
            <div class="box action-card">
              <h3 class="subtitle mb-1">Acompanhar Pedidos</h3>
              <p>Acompanhe os status de processamento dos pedidos</p>
              <router-link to="/pedidos">
                  <button class="button is-info mt-3">Acompanhar</button>
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>

<style scoped>
    .action-card {
        transition: transform 0.2s;
        cursor: pointer;
    }
    .action-card:hover {
        transform: translateY(-5px);
    }
</style>
    
<script>
    import StatusProcessamentoChart from '../components/ChartDatas/StatusProcessamentoChart.vue';

    export default {
        components: {
        StatusProcessamentoChart
        },
        data() {
        return {
            pedidos: [],
            quantidadeTotalEmEstoque: 0,
            isLoading: false,
            chartKey: 0,
            chartDatas: [0, 0],
            chartColors: ['#38613f', '#ae3949'],
            chartLabels: ['Processados', 'Pendentes']
        }
        },
        created() {
        this.carregarPedidos();
        this.carregarProdutosEmEstoque();
        },
        computed: {
            quantidadeDePedidosPendentes() {
            return this.pedidos.filter(pedido => pedido.StatusProcessamento === 1).length;
            },
            quantidadeDePedidosProcessados() {
            return this.pedidos.filter(pedido => pedido.StatusProcessamento === 2).length;
            }
        },
        methods: {
        carregarPedidos() {
            this.isLoading = true;
            this.$http.get('/api/pedidos')
                .then(response => {
                    this.pedidos = response.data;

                    this.chartDatas = [
                        this.quantidadeDePedidosProcessados,
                        this.quantidadeDePedidosPendentes
                    ];
                    this.chartKey += 1;
                })
                .catch(error => {
                    console.error("Erro ao carregar pedidos:", error);
                })
                .finally(() => {
                    this.isLoading = false;
                });
        },
        carregarProdutosEmEstoque(){
        this.$http.get('/api/produtosEmEstoque')
            .then(response => {
                this.quantidadeTotalEmEstoque = response.data.quantidadeTotalEmEstoque;
            })
            .catch(error => {
                console.error("Erro ao carregar a quantidade total de produtos em estoque:", error);
            })
            .finally(() => {
                this.isLoading = false;
            });
        }
        }
    }
</script>
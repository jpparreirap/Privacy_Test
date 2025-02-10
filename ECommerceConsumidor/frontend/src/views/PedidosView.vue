<template>
    <div class="container is-fluid">
        <h1 class="title has-text-centered pt-5">Acompanhamento de Pedidos</h1>
        <pedidos-table :pedidos="pedidos" :is-loading="isLoading" />
    </div>
</template>

<script>
  import PedidosTable from '@/components/Pedidos/PedidosTable.vue';

  export default {
    name: 'PedidosView',
    components: {
      PedidosTable
    },
    data() {
      return {
        pedidos: [],
        isLoading: false
      };
    },
    async created() {
      await this.carregarPedidos();
    },
    methods: {
      async carregarPedidos() {
        this.isLoading = true;
        try {
          this.$http.get('/api/pedidos').then(response => {
              this.pedidos = response.data
              console.log(response.data);
          });
        } finally {
          this.isLoading = false;
        }
      }
    }
  };
</script>
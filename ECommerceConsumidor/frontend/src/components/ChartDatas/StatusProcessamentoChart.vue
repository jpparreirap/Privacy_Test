<template>
    <b-doughnut-chart
        :key="ckey"
        :data="chartData"
        :options="chartOptions"
        class="has-text-centered"
        style="height: 57vh"
    />
</template>

<script>
    import { Doughnut } from 'vue-chartjs';

    export default {
        name: 'StatusProcessamentoChart',
        components: {
            'b-doughnut-chart': Doughnut
        },
        props: {
            cDatas: {
                type: Array,
                required: true
            },
            cLabels: {
                type: Array,
                required: true
            },
            colors: {
                type: Array,
                required: true
            },
            ckey: {
                type: Number,
                required: true
            }
        },
        watch: {
            cDatas: {
                handler(novoValor) {
                    this.chartData.datasets[0].data = novoValor;
                },
                immediate: true
            },
            colors: {
                handler(novoValor) {
                    this.chartData.datasets[0].backgroundColor = novoValor;
                },
                immediate: true
            },
            cLabels: {
                handler(novoValor) {
                    this.chartData.labels = novoValor;
                },
                immediate: true
            }
        },
        data () {
            return{
                chartData: {
                    datasets: [{
                      data: this.cDatas,
                      backgroundColor: this.colors
                    }],
                    labels: this.cLabels
                },
                chartOptions: {
                  responsive: true,
                  maintainAspectRatio: false,
                }
            }
        }
    };
</script>
  
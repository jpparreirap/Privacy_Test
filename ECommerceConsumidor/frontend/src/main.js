import Vue from 'vue'
import App from './App.vue'
import router from './router';
import Buefy from 'buefy';
import 'buefy/dist/buefy.css';
import '@mdi/font/css/materialdesignicons.css';
import VueResource from 'vue-resource';
import { Chart as ChartJS, registerables } from 'chart.js';

ChartJS.register(...registerables);

Vue.config.productionTip = false

Vue.use(Buefy);

Vue.use(VueResource);

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');

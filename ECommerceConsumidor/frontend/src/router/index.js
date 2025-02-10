import Vue from 'vue';
import VueRouter from 'vue-router';
import PedidosView from '../views/PedidosView.vue';
import HomeView from '../views/HomeView.vue';

Vue.use(VueRouter);

const routes = [
    {
        path: '/',
        name: 'Home',
        component: HomeView
    },
    {
        path: '/pedidos',
        name: 'Pedidos',
        component: PedidosView
    }
];

const router = new VueRouter({
    mode: 'history',
    base: '/',
    routes
});

export default router;
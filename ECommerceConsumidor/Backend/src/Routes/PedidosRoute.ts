import express from 'express';
import { listarPedidos } from '../Controllers/PedidoController';

const router = express.Router();
router.get('/pedidos', listarPedidos);

export default router;
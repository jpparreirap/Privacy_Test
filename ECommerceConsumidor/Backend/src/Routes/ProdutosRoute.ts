import express from 'express';
import { listarProdutos, totalDeProdutosEmEstoque } from '../Controllers/ProdutoController';

const router = express.Router();
router.get('/produtos', listarProdutos);
router.get('/produtosEmEstoque', totalDeProdutosEmEstoque);

export default router;
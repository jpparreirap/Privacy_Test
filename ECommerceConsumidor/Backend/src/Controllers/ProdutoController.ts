import { Request, Response } from 'express';
import { Produto } from '../Models/ProdutoModel';

export const listarProdutos = async (req: Request, res: Response) => {
    try {
        const produtos = await Produto.find();
        res.json(produtos);
    } catch (error) {
        console.error(error)
        res.status(500).json({ message: 'Erro ao buscar produtos' });
    }
};

export const totalDeProdutosEmEstoque = async (req: Request, res: Response) => {
    try {
        const produtos = await Produto.find();
        const quantidadeTotalEmEstoque = produtos.reduce((total, produto) => {
            return total + produto.QuantidadeEmEstoque;
        }, 0);
        res.json({ quantidadeTotalEmEstoque });
    } catch (error) {
        console.error(error)
        res.status(500).json({ message: 'Erro ao buscar quantidade total de produtos em estoque' });
    }
};
import { Request, Response } from 'express';
import { Pedido } from '../Models/PedidoModel';

export const listarPedidos = async (req: Request, res: Response) => {
    try {
        const pedidos = await Pedido.find();
        console.log(pedidos)
        res.json(pedidos);
    } catch (error) {
        console.error(error)
        res.status(500).json({ message: 'Erro ao buscar pedidos' });
    }
};
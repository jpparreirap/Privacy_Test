import { Schema, model, Document } from 'mongoose';
import { ICliente } from './ClienteModel';
import { IProduto } from './ProdutoModel';
import { MeioDePagamento } from './Enums/MeioDePagamento';
import { SituacaoPagamento } from './Enums/SituacaoPagamento';
import { StatusProcessamento } from './Enums/StatusProcessamento';

export interface IPedido extends Document {
    cliente: ICliente;
    Produtos: IProduto[];
    TotalPedido: number;
    StatusProcessamento: StatusProcessamento;
    MeioDePagamento: MeioDePagamento;
    SituacaoPagamento: SituacaoPagamento;
    NumeroDeParcelas?: number;
    DescontoAplicado?: Number,
    DataDeProcessamento?: Date,
    DistanciaEmKM: number;
}

const PedidoSchema = new Schema<IPedido>({
    cliente: { type: Schema.Types.ObjectId, ref: 'Cliente', required: true },
    Produtos: [{ type: Schema.Types.ObjectId, ref: 'Produto', required: true }],
    TotalPedido: { type: Number, required: true },
    StatusProcessamento: { type: Number, enum: Object.values(StatusProcessamento), required: true },
    MeioDePagamento: { type: Number, enum: Object.values(MeioDePagamento), required: true },
    SituacaoPagamento: { type: Number, enum: Object.values(SituacaoPagamento), required: true },
    NumeroDeParcelas: { type: Number },
    DescontoAplicado: { type: Number },
    DataDeProcessamento: { type: Date },
    DistanciaEmKM: { type: Number, required: true },
});

export const Pedido = model<IPedido>('Pedido', PedidoSchema, 'Pedidos');
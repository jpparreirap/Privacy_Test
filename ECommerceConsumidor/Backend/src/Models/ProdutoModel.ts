import { Schema, model, Document } from 'mongoose';

export interface IProduto extends Document {
    Nome: string;
    Preco: number;
    QuantidadeEmEstoque: number;
    ElegivelProgramaDeAssinatura: boolean;
}

const ProdutoSchema = new Schema<IProduto>({
    Nome: { type: String, required: true },
    Preco: { type: Number, required: true },
    QuantidadeEmEstoque: { type: Number, required: true },
    ElegivelProgramaDeAssinatura: { type: Boolean, required: true },
});

export const Produto = model<IProduto>('Produto', ProdutoSchema, 'Produtos');
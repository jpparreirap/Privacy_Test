import { Schema, model, Document } from 'mongoose';
import { IAssinatura, AssinaturaSchema } from './AssinaturaModel';

export interface ICliente extends Document {
    Nome: string;
    Email: string;
    Telefone: string;
    Assinatura: IAssinatura;
}

const ClienteSchema = new Schema<ICliente>({
    Nome: { type: String, required: true },
    Email: { type: String, required: true },
    Telefone: { type: String, required: true },
    Assinatura: { type: AssinaturaSchema, required: true },
});

export const Cliente = model<ICliente>('Cliente', ClienteSchema, 'Clientes');
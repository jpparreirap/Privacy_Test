import { Schema } from 'mongoose';

export interface IAssinatura {
    EstaAtiva: boolean;
    DataDeInicio: Date;
    QuantidadeDeMesesAtiva: number;
}

export const AssinaturaSchema = new Schema<IAssinatura>({
    EstaAtiva: { type: Boolean, required: true },
    DataDeInicio: { type: Date, required: true },
    QuantidadeDeMesesAtiva: { type: Number, required: true },
});
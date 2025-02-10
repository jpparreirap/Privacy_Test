import dotenv from 'dotenv';

dotenv.config();

export const config = {
    mongoUrl: process.env.MONGO_URL,
    rabbitMQUrl: process.env.RABBITMQ_URL,
    port: Number(process.env.PORT) || 3000,
    host: process.env.HOST || "localhost"
};
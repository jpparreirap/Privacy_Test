import express from 'express';
import mongoose from 'mongoose';
import { config } from './src/Config/config';
import pedidosRoute from './src/Routes/PedidosRoute';
import produtosRoute from './src/Routes/ProdutosRoute';
import { ServiceRabbitMQ } from './src/Services/ServiceRabbitMQ';

const app = express();

app.use(express.json());

app.use('/api', pedidosRoute);
app.use('/api', produtosRoute);

(async () => {
    await app.listen(config.port, config.host, () => {
        console.log(`Servidor rodando em http://${config.host}:${config.port}`);
    });

    await mongoose.connect(config.mongoUrl!)
                  .then(() => console.log('Conectado ao MongoDB'))
                  .catch(err => console.error('Erro ao conectar ao MongoDB:', err));

    const serviceRabbitMQ = new ServiceRabbitMQ();
    await serviceRabbitMQ.conectarRabbitMQ();
})();
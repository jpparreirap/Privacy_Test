import amqp from 'amqplib';
import { config } from '../Config/config';
import { PedidosProcessadosConsumer_Received } from '../Consumers/PedidosProcessadosConsumer_Received';

export class ServiceRabbitMQ {
    private connection!: amqp.Connection;
    private channel!: amqp.Channel;

    constructor() {

    }

    public async conectarRabbitMQ(): Promise<void> {
        try {
            this.connection = await amqp.connect(config.rabbitMQUrl!);
            this.channel = await this.connection.createChannel();
            console.log('Conectado ao RabbitMQ');
            
            const pedidosProcessadosConsumer = new PedidosProcessadosConsumer_Received(this);
            pedidosProcessadosConsumer.iniciarConsumer();
        } catch (error) {
            console.error('Erro ao conectar ao RabbitMQ:', error);
            throw error;
        }
    }

    public async publicarMensagem(queueName: string, message: string): Promise<void> {
        if (!this.channel) {
            throw new Error('Canal do RabbitMQ não inicializado.');
        }
        console.log(`Publicando para a fila "${queueName}"`);
        this.channel.sendToQueue(queueName, Buffer.from(message));
        console.log(`Mensagem enviada": ${message}`);
    }

    public async consumidorMensagens(queueName: string, handler: (message: string) => Promise<void>): Promise<void> {
        if (!this.channel) {
            throw new Error('Canal do RabbitMQ não inicializado.');
        }

        console.log(`Aguardando mensagens na fila "${queueName}"...`);

        this.channel.consume(queueName, async (msg) => {
            if (msg) {
                const message = msg.content.toString();
                console.log(`Mensagem recebida da fila ${queueName}: ${message}`);

                try {
                    await handler(message);
                    await this.publicarMensagem("PedidosProcessados.Resposta", "Consumo e persistencia dos dados concluídos");
                    this.channel.ack(msg);
                } catch (error) {
                    console.error('Erro ao processar mensagem:', error);
                    this.channel.nack(msg);
                }
            }
        });
    }
}
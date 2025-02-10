# Privacy_Test
Diretório destinado ao teste da privacy

## Etapas do desafio
### Etapa 1: Processamento e Enfileiramento de Dados
Desenvolver um sistema em .NET (C#) responsável por:<br>
Leitura de Dados: Implementar a leitura de dados de uma base de dados MongoDB.<br>
Tratamento de Dados: Realizar o tratamento e manipulação dos dados lidos, de acordo com as regras de negócio especificadas pelo candidato.<br>
Envio para Fila: Enviar os dados tratados para uma fila de mensagens RabbitMQ.

 
### Etapa 2: Consumo e Apresentação de Dados
Desenvolver um sistema composto por duas partes:<br>
Backend (Node.js): Responsável por consumir as mensagens da fila RabbitMQ e persistir as mensagens na base do MongoDB.<br>
Frontend (Vue.js): Responsável por exibir as informações tratadas em uma interface web. O frontend deverá ser desenvolvido utilizando o framework Vue.js e implementado como um Web Component.

### Tecnologias Envolvidas
O desafio abrange as seguintes tecnologias:<br>
Backend: C# (.NET) e Node.js<br>
Frontend: ASP.NET e Vue.js (Web Components)<br>
Banco de Dados: MongoDB<br>
Mensageria: RabbitMQ

## Regras de negócio implementadas
<ul>
 <li>Desconto calculado de acordo com o meio de pagamento, caso for avista cliente recebe 10%, caso o meio de pagamento seja cartão de crédito e parcelado com parcelas > 1
a taxa de juros é calculada com base no total do pedido, a uma taxa fixa de 2% ao mês.</li>
 <li>Se o cliente for assinate, receberá um desconto adicional, o calculo do desconto será com base na quantidade de meses ativos da assinatura. tempo >= 3 meses 2% a mais,
  >= 6 meses 5% a mais e >= 12 meses 10% a mais de desconto. Este desconto é acumulativo ao desconto anterior em caso de pagamento Avista.</li>
 <li>O calculo do frete será calculado com base na distancia em Km para a entrega ser feita, e será calculado com base no valor total do pedido após os descontos aplicados,
  uma taxa fixa de 10 reais e a 2,5 reais por Km de distância</li>
 <li>Deverá dar baixa do estoque os produtos que foram vendidos no pedido processado com sucesso</li>
 <li>Deverá ser implementado um sistema para acompanhamento do processamento dos pedidos feitos e do estoque disponível, simulando um centro de distribuição</li>
</ul>


## Breve detalhes de execução
<ul>
  <li>É necessário configurar o appsettings.Development.json (aplicação .net) e o .env(aplicação node)</li>
  <li>Na primeira execução da aplicação .net o banco de dados será criado e populado pela aplicação</li>
  <li>Para a requisição de processamento basta fazer uma requisição de get simples ao endpoint da aplicação .net "/Pedidos/processapedido", imagem após o processamento:</li>
</ul>

# Imagens de execução
<ul>
  <li>
    <p>Banco criado ao executar aplicação</p>
    <p>![image](https://github.com/user-attachments/assets/947393e8-aff1-4e1d-8e04-5a6f02a50dda)</p>
    <p>![image](https://github.com/user-attachments/assets/27594805-9f08-4763-8c23-3b9f7e5723df)</p>
    <p>![image](https://github.com/user-attachments/assets/05eef443-4ac3-404b-93c1-c2e0f8c87824)</p>
  </li>
  <li>
    <p>Dados na aplicação Vuejs recuperados pelo node</p>
    <p>![image](https://github.com/user-attachments/assets/d2854832-e2cb-4a0b-bd7f-e641d21f2411)</p>
    <p>![image](https://github.com/user-attachments/assets/cd277d8c-21e8-49ad-82da-d4066c232283)</p>
  </li>
  <li>
    <p>Persistencia realizada, após processamento</p>
    <p>![image](https://github.com/user-attachments/assets/bd7fc765-043f-412b-b7bb-810fd6540986)</p>
    <p>![image](https://github.com/user-attachments/assets/0ee2c28b-ebd3-48df-8389-42c5a1f56532)</p>
  </li>
</ul>

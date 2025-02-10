using ECommerceProcessamento.Entities;
using ECommerceProcessamento.Enums;
using ECommerceProcessamento.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerceProcessamento.Database.Feed
{
    public class FeedDatabase : IFeedDatabase
    {
        private readonly IMongoDbContext _dbContext;

        public FeedDatabase(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task PopularDadosIniciaisDatabaseAsync()
        {
            // Verifica e popula Produtos
            if (await _dbContext.Produtos.CountDocumentsAsync(new BsonDocument()) == 0)
            {
                var produtos = new List<Produto>
                {
                    new Produto
                    {
                        Nome = "Dipirona",
                        Preco = 5.90m,
                        QuantidadeEmEstoque = 200,
                        ElegivelProgramaDeAssinatura = true
                    },
                    new Produto
                    {
                        Nome = "Clonazepam",
                        Preco = 10.00m,
                        QuantidadeEmEstoque = 50,
                        ElegivelProgramaDeAssinatura = false
                    },
                    new Produto
                    {
                        Nome = "Doralgina Cx",
                        Preco = 10.99m,
                        QuantidadeEmEstoque = 100,
                        ElegivelProgramaDeAssinatura = true
                    },
                    new Produto
                    {
                        Nome = "Pantoprazol 40mg",
                        Preco = 11.99m,
                        QuantidadeEmEstoque = 75,
                        ElegivelProgramaDeAssinatura = true
                    },
                    new Produto
                    {
                        Nome = "Fluoxetina 20mg",
                        Preco = 32.68m,
                        QuantidadeEmEstoque = 30,
                        ElegivelProgramaDeAssinatura = false
                    }
                };
                await _dbContext.Produtos.InsertManyAsync(produtos);
            }

            // Verifica e popula Clientes
            if (await _dbContext.Clientes.CountDocumentsAsync(new BsonDocument()) == 0)
            {
                var clientes = new List<Cliente>
                {
                    new Cliente
                    {
                        Nome = "João",
                        Email = "joao@email.com",
                        Telefone = "(34) 98487-9875",
                        Assinatura = new Assinatura
                        {
                            EstaAtiva = true,
                            DataDeInicio = DateTime.UtcNow,
                            QuantidadeDeMesesAtiva = 12
                        }
                    },
                    new Cliente
                    {
                        Nome = "Maria",
                        Email = "maria@email.com",
                        Telefone = "(45) 98435-8975",
                        Assinatura = new Assinatura
                        {
                            EstaAtiva = false,
                            DataDeInicio = DateTime.UtcNow,
                            QuantidadeDeMesesAtiva = 0
                        }
                    },
                    new Cliente
                    {
                        Nome = "Pedro",
                        Email = "pedro@email.com",
                        Telefone = "(35) 93467-2648",
                        Assinatura = new Assinatura
                        {
                            EstaAtiva = true,
                            DataDeInicio = DateTime.UtcNow,
                            QuantidadeDeMesesAtiva = 6
                        }
                    },
                    new Cliente
                    {
                        Nome = "Rafaela",
                        Email = "rafaela@email.com",
                        Telefone = "(11) 96589-3987",
                        Assinatura = new Assinatura
                        {
                            EstaAtiva = true,
                            DataDeInicio = DateTime.UtcNow,
                            QuantidadeDeMesesAtiva = 3
                        }
                    },
                    new Cliente
                    {
                        Nome = "Luiz",
                        Email = "luiz@email.com",
                        Telefone = "(87) 96497-6788",
                        Assinatura = new Assinatura
                        {
                            EstaAtiva = false,
                            DataDeInicio = DateTime.UtcNow,
                            QuantidadeDeMesesAtiva = 0
                        }
                    }
                };
                await _dbContext.Clientes.InsertManyAsync(clientes);
            }

            // Verifica e popula Pedidos (opcional, depende da regra de negócio)
            if (await _dbContext.Pedidos.CountDocumentsAsync(new BsonDocument()) == 0)
            {
                Cliente joao = await _dbContext.Clientes.Find(c => c.Nome == "João").FirstOrDefaultAsync();
                Cliente maria = await _dbContext.Clientes.Find(c => c.Nome == "Maria").FirstOrDefaultAsync();
                Produto diprona = await _dbContext.Produtos.Find(p => p.Nome == "Dipirona").FirstOrDefaultAsync();
                Produto clonazepam = await _dbContext.Produtos.Find(p => p.Nome == "Clonazepam").FirstOrDefaultAsync();
                Produto doralgina = await _dbContext.Produtos.Find(p => p.Nome == "Doralgina Cx").FirstOrDefaultAsync();
                Produto pantoprazol = await _dbContext.Produtos.Find(p => p.Nome == "Pantoprazol 40mg").FirstOrDefaultAsync();
                Produto fluoxetina = await _dbContext.Produtos.Find(p => p.Nome == "Fluoxetina 20mg").FirstOrDefaultAsync();

                Pedido pedidoDoJoao = new Pedido();
                Pedido pedidoDaMaria = new Pedido();
                List<Produto> produtos = new List<Produto>();
                List<Pedido> pedidos = new List<Pedido>();

                if (joao != null && pantoprazol != null && doralgina != null && fluoxetina != null)
                {
                    produtos = new List<Produto> { pantoprazol, pantoprazol, pantoprazol, doralgina, doralgina, doralgina, doralgina, fluoxetina, fluoxetina };
                    pedidoDoJoao = new Pedido()
                    {
                        Cliente = joao,
                        Produtos = produtos,
                        TotalPedido = produtos.Sum(produto => produto.Preco),
                        StatusProcessamento = StatusProcessamento.Pendente,
                        SituacaoPagamento = SituacaoPagamento.Pendente,
                        MeioDePagamento = MeioDePagamento.CartaoDeCredito | MeioDePagamento.Parcelado,
                        NumeroDeParcelas = 3,
                        DistanciaEmKM = 5
                    };
                }

                if (maria != null && diprona != null && clonazepam != null)
                {
                    produtos = new List<Produto> { diprona, diprona, diprona, diprona, clonazepam, clonazepam, clonazepam };
                    pedidoDaMaria = new Pedido()
                    {
                        Cliente = maria,
                        Produtos = produtos,
                        TotalPedido = produtos.Sum(produto => produto.Preco),
                        StatusProcessamento = StatusProcessamento.Pendente,
                        SituacaoPagamento = SituacaoPagamento.Pendente,
                        MeioDePagamento = MeioDePagamento.Avista,
                        DistanciaEmKM = 2
                    };
                }

                if(pedidoDoJoao != null)
                    pedidos.Add(pedidoDoJoao);

                if (pedidoDaMaria != null)
                    pedidos.Add(pedidoDaMaria);

                if (pedidos != null)
                    await _dbContext.Pedidos.InsertManyAsync(pedidos);
            }
        }
    }
}

using ECommerceProcessamento.Database.Context;
using ECommerceProcessamento.Database.Feed;
using ECommerceProcessamento.Database.Settings;
using ECommerceProcessamento.Interfaces;
using ECommerceProcessamento.Services;

namespace ECommerceProcessamento
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));

            builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

            builder.Services.AddTransient<IFeedDatabase, FeedDatabase>();
            builder.Services.AddTransient<IServicePedido, ServicePedido>();
            builder.Services.AddTransient<IServiceRabbitMQ, ServiceRabbitMQ>();

            builder.Services.AddControllers();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var feed = scope.ServiceProvider.GetRequiredService<IFeedDatabase>();
                await feed.PopularDadosIniciaisDatabaseAsync();
                var serviceRabbitMQ = scope.ServiceProvider.GetRequiredService<IServiceRabbitMQ>();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

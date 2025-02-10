using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceProcessamento.Entities
{
    public class Cliente
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Assinatura Assinatura { get; set; }
    }
}

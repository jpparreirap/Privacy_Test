using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceProcessamento.Entities
{
    public class Produto
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public bool ElegivelProgramaDeAssinatura { get; set; }
    }
}

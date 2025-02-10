using ECommerceProcessamento.Entities;
using ECommerceProcessamento.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceProcessamento.DTOs
{
    public class PedidoProcessadoDTO
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Cliente Cliente { get; set; }
        public IList<Produto> Produtos { get; set; }
        public decimal TotalFinal {  get; set; }
        public decimal DescontoAplicado { get; set; }
        public StatusProcessamento StatusProcessamento { get; set; }
        public SituacaoPagamento SituacaoPagamento { get; set; }
        public DateTime ProcessadoEm { get; set; }
    }
}

using ECommerceProcessamento.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceProcessamento.Entities
{
    public class Pedido
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Cliente Cliente { get; set; }
        public IList<Produto> Produtos { get; set; }
        public decimal TotalPedido { get; set; }
        public StatusProcessamento StatusProcessamento { get; set; }
        public SituacaoPagamento SituacaoPagamento { get; set; }
        public MeioDePagamento MeioDePagamento { get; set; }
        public int? NumeroDeParcelas {  get; set; }
        public decimal? DescontoAplicado { get; set; }
        public DateTime? DataDeProcessamento { get; set; }
        public double DistanciaEmKM { get; set; }
    }
}

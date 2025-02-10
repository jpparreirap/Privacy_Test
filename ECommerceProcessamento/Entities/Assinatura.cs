namespace ECommerceProcessamento.Entities
{
    public class Assinatura
    {
        public bool EstaAtiva { get; set; }
        public DateTime DataDeInicio { get; set; }
        public int QuantidadeDeMesesAtiva { get; set; }
    }
}

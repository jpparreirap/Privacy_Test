namespace ECommerceProcessamento.Enums
{
    [Flags]
    public enum MeioDePagamento
    {
        Avista = 1,
        CartaoDeDebito = 2,
        CartaoDeCredito = 3,
        Parcelado = 4
    }
}

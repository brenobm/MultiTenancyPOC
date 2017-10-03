namespace MultiTenancy.Dominio.Entidades
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}

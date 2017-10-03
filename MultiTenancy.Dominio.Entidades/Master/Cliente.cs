namespace MultiTenancy.Dominio.Entidades.Master
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }
        public string Dominio { get; set; }
        public string StringConexaoBanco { get; set; }
        public byte[] Icone { get; set; }
    }
}

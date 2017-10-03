using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiTenancy.Integracao.Dados.Impl.Entities
{
    [Table("TBL_PRODUTO")]
    public class ProdutoEntity
    {
        [Key]
        [Column("COD_PRODUTO")]
        public int Codigo { get; set; }
        [Column("NOM_PRODUTO")]
        public string Nome { get; set; }
        [Column("VLR_PRODUTO")]
        public decimal Valor { get; set; }
        [Column("QTD_PRODUTO")]
        public int Quantidade { get; set; }

    }
}

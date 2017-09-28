using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiTenancy.Integration.Clientes.Models
{
    [Table("TBL_PRODUTO")]
    public class Produto
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MultiTenancyPOC.Models
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
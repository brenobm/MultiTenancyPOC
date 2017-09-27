using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MultiTenancyPOC.Models.Master
{
    [Table("TBL_CLIENTE")]
    public class Cliente
    {
        [Key]
        [Column("COD_CLIENTE")]
        public int Codigo { get; set; }
        [Column("NOM_CLIENTE")]
        public string Nome { get; set; }
        [Column("NOM_DOMINIO")]
        public string Dominio { get; set; }
        [Column("DSC_STRING_CONEXAO")]
        public string StringConexaoBanco { get; set; }
        [Column("BIN_ICONE_CLIENTE", TypeName = "varbinary")]
        public byte[] Icone { get; set; }
    }
}
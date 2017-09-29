using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiTenancy.Integration.Clientes.Models
{
    [Table("TAU_PRODUTO")]
    public class Auditoria
    {
        [Key]
        [Column("COD_AUDITORIA")]
        public int Codigo { get; set; }
        [Column("NOM_USUARIO")]
        public string Usuario { get; set; }
        [Column("DAT_ACAO")]
        public DateTime DataAcao { get; set; }
        [Column("TIP_ACAO")]
        public TipoAcaoBanco TipoAcao { get; set; }
        [Column("NOM_CAMPO")]
        public string NomeCampo { get; set; }
        [Column("VAL_ANTIGO")]
        public string ValorAntigo { get; set; }
        [Column("VAL_NOVO")]
        public string ValorNovo { get; set; }
    }
}

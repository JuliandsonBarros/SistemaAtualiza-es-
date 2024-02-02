using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sisat.Models
{
    [Table("Respostas_Forum")]
    public partial class RespostasForum
    {
        [Key]
        [Column("Id_Resposta")]
        public int IdResposta { get; set; }
        [Column("Id_Autor")]
        public int IdAutorResposta { get; set; }
        [Column("Id_Topico")]
        public int IdTopico { get; set; }
        public string Mensagem { get; set; } = null!;
        [Column("Data_Resposta", TypeName = "datetime")]
        public DateTime DataResposta { get; set; }
        public bool? Visualizacao { get; set; }

        [ForeignKey(nameof(IdAutorResposta))]
        [InverseProperty(nameof(Usuario.Respostas))]
        public virtual Usuario IdAutorNavigation { get; set; } = null!;
        [ForeignKey(nameof(IdTopico))]
        [InverseProperty(nameof(Forum.RespostasForum))]
        public virtual Forum IdTopicoNavigation { get; set; } = null!;
    }
}
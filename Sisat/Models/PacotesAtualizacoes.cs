using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sisat.Models
{
    [Table("Pacotes_Atualizacoes")]
    public partial class PacotesAtualizacoes
    {
        [Key]
        [Column("Id_Pacote")]
        public int IdPacote { get; set; }
        [Column("Id_Proj")]
        public int? IdProj { get; set; }
        [Column("Num_Versao")]
        [StringLength(30)]
        public string NumVersao { get; set; } = null!;
        [Column("Registro_Alteracoes")]
        public string RegistroAlteracoes { get; set; } = null!;
        [Column("Dt_Lancamento", TypeName = "datetime")]
        public DateTime? DtLancamento { get; set; }
        [Column("Dir_Arquivo")]
        [StringLength(255)]
        public string? DirArquivo { get; set; }
        public bool? Ativo { get; set; }

        [ForeignKey(nameof(IdProj))]
        [InverseProperty(nameof(Projetos.PacotesAtualizacoes))]
        public virtual Projetos? IdProjNavigation { get; set; }
    }
}

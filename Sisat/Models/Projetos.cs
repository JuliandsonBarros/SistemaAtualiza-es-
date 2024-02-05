using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sisat.Models
{
    public partial class Projetos
    {

        public Projetos()
        {
            PacotesAtualizacoes = new HashSet<PacotesAtualizacoes>();
        }

        [Key]
        [Column("Id_Projeto")]
        public int IdProjeto { get; set; }
        [Column("Nom_Projeto")]
        [StringLength(30)]
        public string NomProjeto { get; set; } = null!;

        [InverseProperty("IdProjNavigation")]
        public virtual ICollection<PacotesAtualizacoes> PacotesAtualizacoes { get; set; }
    }
}
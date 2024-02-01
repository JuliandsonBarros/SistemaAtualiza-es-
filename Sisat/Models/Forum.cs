using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sisat.Models
{
    public partial class Forum
    {
        public Forum()
        {
            RespostasForum = new HashSet<RespostasForum>();
        }

        [Key]
        [Column("Id_Forum")]
        public int IdForum { get; set; }
        [Column("Id_Autor")]
        public int? IdAutor { get; set; }
        [StringLength(255)]
        public string? Titulo { get; set; }
        public string? Conteudo { get; set; }
        [Column("Data_Postagem", TypeName = "datetime")]
        public DateTime? DataPostagem { get; set; }

        public virtual Usuario UsuarioForum { get; set; }

        [InverseProperty("IdTopicoNavigation")]
        public virtual ICollection<RespostasForum> RespostasForum { get; set; }
    }
}



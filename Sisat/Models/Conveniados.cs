using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sisat.Models
{
    public partial class Conveniados
    {
        public Conveniados()
        {
            ConvenioProjeto = new HashSet<ConvenioProjeto>();
        }

        [Key]
        [Column("Id_Conveniado")]
        public int IdConveniado { get; set; }
        [Column("Nom_Conveniado")]
        [StringLength(100)]
        public string NomConveniado { get; set; } = null!;
        [Column("ID_Usuario")]
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Conveniados))]
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        [InverseProperty("IdConNavigation")]
        public virtual ICollection<ConvenioProjeto> ConvenioProjeto { get; set; }
    }
}

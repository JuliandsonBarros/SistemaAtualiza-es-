using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sisat.Models
{
    public partial class NivelDeAcesso
    {
        public NivelDeAcesso()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        [Column("Id_NivelAcesso")]
        public int IdNivelAcesso { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Descricao { get; set; }

        [InverseProperty("IdNivAcessoNavigation")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}




using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sisat.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Conveniados = new HashSet<Conveniados>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(256)]
        [Unicode(false)]
        public string Email { get; set; } = null!;

        [StringLength(256)]
        [Unicode(false)]
        public string Nome { get; set; } = null!;

        [StringLength(128)]
        [Unicode(false)]
        public string Senha { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string? Login { get; set; }

        [Column("Id_NivAcesso")]
        public int? IdNivAcesso { get; set; }

        [ForeignKey(nameof(IdNivAcesso))]
        [InverseProperty(nameof(NivelDeAcesso.Usuario))]
        public virtual NivelDeAcesso? IdNivAcessoNavigation { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Conveniados> Conveniados { get; set; }
        [InverseProperty("IdAutorNavigation")]
        public virtual ICollection<RespostasForum> RespostasForum { get; set; }

        [InverseProperty("UsuarioForum")]
        public virtual ICollection<Forum> ForumUsuario { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}


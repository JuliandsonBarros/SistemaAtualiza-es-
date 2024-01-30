using Microsoft.EntityFrameworkCore;
using Sisat.Models;
using SQLitePCL;
using System;
using System.Linq;

namespace Sisat.ViewModels
{
    public class BaseViewModel
    {
        public ProjetoListViewModel ProjetoListViewModel { get; set; }
        public IEnumerable<PacotesAtualizacoes> PacotesAtualizacoes { get; set; }

        public static Usuario? UsuarioMemoria { get; set; }
        public Usuario? Usuario => UsuarioMemoria;


        public List<ProjetoComUltimoPacote> HomeProjeto()
        {
            var idProj = (this.Usuario.Conveniados.FirstOrDefault() ?? new()).ConvenioProjeto.Select(c => c.IdProj).ToList();

            using (var _context = new SisatContext())
            {
                var projetosComUltimoPacote = _context.Projetos.Where(ip => (this.Usuario.IdNivAcesso == 1 ? true : idProj.Contains(ip.IdProjeto)))
                    .Select(proj => new ProjetoComUltimoPacote
                    {
                        Projeto = proj,
                        UltimoPacote = _context.PacotesAtualizacoes
                            .Where(pacote => pacote.IdProj == proj.IdProjeto)
                            .OrderByDescending(pacote => pacote.NumVersao)
                            .FirstOrDefault()
                    }).ToList();

                return projetosComUltimoPacote;
            }
        }

        public List<RetornaForuns> ListaForuns()
        {
            using (var _context = new SisatContext())
            {
                var listaMensagens = _context.Forum
                    .Include(rf => rf.RespostasForum)
                    .ThenInclude(u => u.IdAutorNavigation)
                    .ThenInclude(c => c.Conveniados)
                    .Where(a => a.IdAutor == 1 || a.IdAutor == 2)
                    .GroupBy(c => c.IdAutor)
                    .Select(grupo => new RetornaForuns
                    {
                        Forum = grupo.OrderByDescending(c => c.DataPostagem).FirstOrDefault(),
                        RetornaForum = grupo.OrderByDescending(c => c.DataPostagem).FirstOrDefault(),
                    })
                    .ToList();

                return listaMensagens;
            }
        }


        public void Logout(HttpContext httpContext)
        {
            UsuarioMemoria = null;
            httpContext.Response.Redirect("/Index/Login");
        }
    }



    public class RetornaForuns
    {
        public Forum Forum { get; set; }

        public Forum RetornaForum { get; set; }


    }


    public class RetornaRespostas
    {
        public RespostasForum Resposta { get; set; }

        public RespostasForum RetornaResposta { get; set; }


    }

    public class ProjetoComUltimoPacote
    {
        public Projetos Projeto { get; set; }

        public PacotesAtualizacoes UltimoPacote { get; set; }

        public PacotesAtualizacoes RetornoPacote { get; set; }
        public PacotesAtualizacoes HomeUltimoPacote { get; set; }
    }
}

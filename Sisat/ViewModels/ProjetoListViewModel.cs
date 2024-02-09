using Microsoft.EntityFrameworkCore;
using Sisat.Controllers;
using Sisat.Models;

namespace Sisat.ViewModels
{
    public class ProjetoListViewModel : BaseViewModel
    {
        public Projetos Projeto { get; set; }

        public List<Projetos> Projetos { get; set; }
        public PacotesAtualizacoes Pacote { get; set; }

        public List<PacotesAtualizacoes> Pacotes { get; set; }

        public bool CondicaoMsg { get; set; }

        public NivelDeAcesso NivelAcess { get; set; }

        public Conveniados Conveniado { get; set; }

        public ProjetoListViewModel()
        {
            Projeto = new Projetos();
            Pacotes = new List<PacotesAtualizacoes>();
            Pacote = new PacotesAtualizacoes();
            Projetos = new List<Projetos>();
            NivelAcess = new NivelDeAcesso();
            Conveniado = new Conveniados();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sisat.Models;
using Sisat.ViewModels;

namespace Sisat.Controllers
{
    public class ForumController : Controller
    {
        private readonly SisatContext _context;

        private readonly ProjetoListViewModel _projetoListViewModel;

        private readonly ForumViewModel _forumViewModel;

        public ForumController(SisatContext context, ForumViewModel forumViewModel)
        {
            _context = context;
            _forumViewModel = forumViewModel;
        }

        public IActionResult Index()
        {
            _forumViewModel.Foruns = _context.Forum
                   .Include(x => x.IdAutorNavigation)
                        .OrderBy(f => f.DataPostagem)
                        .ToList();

            return View(_forumViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EnviaMensagem(string titulo, string textoMensagem)
        {

            var forum = new Forum
            {
                IdAutor = _forumViewModel.Usuario.Id,
                Titulo = titulo,
                Conteudo = textoMensagem,
                DataPostagem = DateTime.Now,
            };

            _context.Forum.Add(forum);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Forum", new { id = forum });

        }

    }
}
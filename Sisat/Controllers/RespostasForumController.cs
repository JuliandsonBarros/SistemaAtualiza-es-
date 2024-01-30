using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sisat.Models;
using Sisat.ViewModels;

namespace Sisat.Controllers
{
    public class RespostasForumController : Controller
    {

        private readonly SisatContext _context;

        private readonly ProjetoListViewModel _projetoListViewModel;

        private readonly ForumViewModel _forumViewModel;

        public RespostasForumController(SisatContext context, ForumViewModel forumViewModel)
        {
            _context = context;
            _forumViewModel = forumViewModel;
        }
        public IActionResult Index(long id)
        {
            _forumViewModel.RespostasForuns = _context.RespostasForum
                           .Where(f => f.IdTopico == id)
                           .Include(u => u.IdAutorNavigation)
                           .ThenInclude(c => c.Conveniados)
                  .OrderBy(f => f.DataResposta)
                  .ToList();

            return View(_forumViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaResposta(int id, string mensagemResposta)
        {
            var topicoExistente = _context.Forum.FirstOrDefault(t => t.IdForum == id);

            var resposta = new RespostasForum
            {
                IdAutorResposta = _forumViewModel.Usuario.Id,
                IdTopico = id,
                Mensagem = mensagemResposta,
                DataResposta = DateTime.Now
            };

            _context.RespostasForum.Add(resposta);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "RespostasForum", new { id = id });
        }
    }
}

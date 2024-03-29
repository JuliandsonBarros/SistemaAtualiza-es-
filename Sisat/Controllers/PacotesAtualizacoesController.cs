﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Sisat.Models;
using Sisat.Services.InterfaceService;
using Sisat.ViewModels;
using SQLitePCL;

namespace Sisat.Controllers
{
    public class PacotesAtualizacoesController : Controller
    {
        private readonly SisatContext _context;

        private readonly ProjetoListViewModel projetoListViewModel;

        private readonly IEmailService _emailService;

        public PacotesAtualizacoesController(SisatContext context, ProjetoListViewModel _projetoListViewModel, IEmailService emailService)
        {
            _context = context;
            this.projetoListViewModel = _projetoListViewModel;
            _emailService = emailService;
        }


        // GET: PacotesAtualizacoes
        public async Task<IActionResult> Index()
        {
            var sisatContext = _context.PacotesAtualizacoes.Include(p => p.IdProjNavigation);
            return View(await sisatContext.ToListAsync());
        }

        // GET: PacotesAtualizacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PacotesAtualizacoes == null)
            {
                return NotFound();
            }

            var pacotesAtualizacoes = await _context.PacotesAtualizacoes
                .Include(p => p.IdProjNavigation)
                .FirstOrDefaultAsync(m => m.IdPacote == id);
            if (pacotesAtualizacoes == null)
            {
                return NotFound();
            }

            return View(pacotesAtualizacoes);
        }

        // GET: PacotesAtualizacoes/Create
        public IActionResult Create()
        {
            ViewData["IdProj"] = new SelectList(_context.Projetos, "IdProjeto", "IdProjeto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjetoListViewModel projetoListViewModel, IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                var diretorioBase = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "arquivos");
                Directory.CreateDirectory(diretorioBase);

                var diretorioProjeto = Path.Combine(diretorioBase, projetoListViewModel.Pacote.IdProj.ToString());
                Directory.CreateDirectory(diretorioProjeto);

                var caminhoArquivo = Path.Combine(diretorioProjeto, arquivo.FileName);
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                projetoListViewModel.Pacote.DirArquivo = caminhoArquivo;
                _context.PacotesAtualizacoes.Add(projetoListViewModel.Pacote);
            }

            await EnviarEmailParaUsuariosAsync(projetoListViewModel.Pacote.IdProj ?? 0);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Atualização inserida com sussesso!";

            return RedirectToAction("Details", "Projetos", new { id = projetoListViewModel.Pacote.IdProj });
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProjetoListViewModel projetoListViewModel, IFormFile arquivo)
        {

            var pacoteExistente = _context.PacotesAtualizacoes.Find(projetoListViewModel.Pacote.IdPacote);
                try
                {

                    if (pacoteExistente != null)
                    {
                        pacoteExistente.NumVersao = projetoListViewModel.Pacote.NumVersao;
                        pacoteExistente.DtLancamento = projetoListViewModel.Pacote.DtLancamento;
                        pacoteExistente.RegistroAlteracoes = projetoListViewModel.Pacote.RegistroAlteracoes;
                        pacoteExistente.DirArquivo = projetoListViewModel.Pacote.DirArquivo;
                   


                        _context.Update(pacoteExistente);
                        _context.SaveChanges();
                }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacotesAtualizacoesExists(projetoListViewModel.Pacote.IdPacote))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Projetos", new { id = pacoteExistente.IdProj });
        }



// GET: PacotesAtualizacoes/Delete/5
public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PacotesAtualizacoes == null)
            {
                return NotFound();
            }

            var pacotesAtualizacoes = await _context.PacotesAtualizacoes
                .Include(p => p.IdProjNavigation)
                .FirstOrDefaultAsync(m => m.IdPacote == id);
            if (pacotesAtualizacoes == null)
            {
                return NotFound();
            }

            return View(pacotesAtualizacoes);


        }

        // POST: PacotesAtualizacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PacotesAtualizacoes == null)
            {
                return Problem("Entity set 'SisatContext.PacotesAtualizacoes'  is null.");
            }
            var pacotesAtualizacoes = await _context.PacotesAtualizacoes.FindAsync(id);
            if (pacotesAtualizacoes != null)
            {
                _context.PacotesAtualizacoes.Remove(pacotesAtualizacoes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacotesAtualizacoesExists(int id)
        {
            return (_context.PacotesAtualizacoes?.Any(e => e.IdPacote == id)).GetValueOrDefault();
        }

        // GET: PacotesAtualizacoes/Download/
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null || _context.PacotesAtualizacoes == null)
            {
                return NotFound();
            }

            var pacote = await _context.PacotesAtualizacoes
                .FirstOrDefaultAsync(m => m.IdPacote == id);

            if (pacote == null || string.IsNullOrEmpty(pacote.DirArquivo))
            {
                return NotFound();
            }

            var filePath = pacote.DirArquivo;
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memoria = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoria);
            }
            memoria.Position = 0;

            return File(memoria, GetContentType(filePath), Path.GetFileName(filePath));
        }

        public async Task EnviarEmailParaUsuariosAsync(long idProjeto)
        {
            var usuarios = _context.Usuario.Where(u => u.Conveniados.Any(c => c.ConvenioProjeto.Any(id => id.IdProj == idProjeto))).Include(c => c.Conveniados).ThenInclude(cp => cp.ConvenioProjeto).ThenInclude(p => p.IdProjNavigation).ToList();

            foreach (var usuario in usuarios)
            {
                if (!string.IsNullOrEmpty(usuario.Email))
                {
                    await _emailService.SendEmailAsync(usuario.Email, "Atualização de Sistema ", "Caro " + usuario.Nome + ",<br> Gostaríamos de informa-ló que uma nova atualização do sistema " + usuario.Conveniados.SelectMany(x => x.ConvenioProjeto).Select(x => x.IdProjNavigation).Where(x => x.IdProjeto == idProjeto).Select(x => x.NomProjeto).FirstOrDefault() + " está disponível.<br> É importante para manutenção, eficiência e segurança que nossos sistemas estejam sempre<br> atualizados com a última versão disponibilizada. <br>Agradecemos sua atenção a este detalhe, contamos com sua colaboração para <br> manter nossos sistemas operando com exelência.<br><br><br>Atenciosamente, <br><br><br>Admin SPAI- MPM/DF"); 
                }
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
    {
         {".csv", "text/csv"},
        {".zip", "application/zip"},
        {".7z", "application/x-7z-compressed"},
        {".rar", "application/octet-stream"},
        {".z", "application/x-compress"},

              };
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sisat.Models;
using Sisat.ViewModels;
using System.Runtime.CompilerServices;

namespace Sisat.Controllers
{
    public class LoginController : Controller
    {
        private readonly SisatContext _context;
        private readonly LoginViewModel _loginViewModel;
        public LoginController(SisatContext context, LoginViewModel loginViewModel)
        {
            _context = context;
            _loginViewModel = loginViewModel;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = BuscarPorLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            BaseViewModel.UsuarioMemoria = usuario;
                            return RedirectToAction("Index", "Projetos");
                        }
                        //mensagem de erro
                    }
                    //mensagem erro 
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                return RedirectToAction("Index");//mensagem de erro
            }
            
        }
        public IActionResult Logout()
        {
            _loginViewModel.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        public Usuario BuscarPorLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
            {

                return null;
            }

            return _context.Usuario.Include(c => c.Conveniados).ThenInclude(pc => pc.ConvenioProjeto).FirstOrDefault(x => x.Login == login);
        }

    }
}

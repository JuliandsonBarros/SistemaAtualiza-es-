using Sisat.Models;

namespace Sisat.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginModel LoginModel { get; set; }

        public LoginViewModel()
        {
            LoginModel = new LoginModel();
        }
    }
}

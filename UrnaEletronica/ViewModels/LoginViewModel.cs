using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UrnaEletronica.Models;
using UrnaEletronica.Views;

namespace UrnaEletronica.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        public DelegateCommand LoginCommand { get; set; }

        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }
        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { SetProperty(ref _senha, value); }
        }

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(Login);
        }

        public void Login()
        {
            if (AppViewModel.Instance.Db.GetCollection<Usuario>("usuarios").FindAll().Where(u => u.Nome == Usuario && u.Senha == Senha && u.Admin).Count() > 0)
            {
                AppViewModel.Instance.MenuVisibility = Visibility.Visible;
                AppViewModel.Instance.Frame.Navigate(new EleicaoView());
            }

            Usuario = "";
            Senha = "";
        }
    }
}

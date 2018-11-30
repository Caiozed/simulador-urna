using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UrnaEletronica.Models;
using UrnaEletronica.ViewModels;

namespace UrnaEletronica.Views
{
    /// <summary>
    /// Interação lógica para LoginView.xam
    /// </summary>
    public partial class LoginDialog : UserControl
    {
        LoginViewModel viewModel;
        public LoginDialog()
        {
            viewModel = new LoginViewModel();
            InitializeComponent();

            DataContext = viewModel;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (AppViewModel.Instance.Db.GetCollection<Usuario>("usuarios").FindAll().Where(u => u.Nome == viewModel.Usuario && u.Senha == viewModel.Senha && u.Admin).Count() > 0)
            {
                AppViewModel.Instance.Dialog.IsOpen = false;
                AppViewModel.Instance.Menu.IsOpen = true;
            }

            viewModel.Usuario = "";
            viewModel.Senha = "";
        }
    }
}

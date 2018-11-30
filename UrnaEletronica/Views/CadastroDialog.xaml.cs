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
using UrnaEletronica.ViewModels;

namespace UrnaEletronica.Views
{
    /// <summary>
    /// Interação lógica para UsuarioDialog.xam
    /// </summary>
    public partial class CadastroDialog : UserControl
    {
        dynamic viewModel;

        public CadastroDialog(dynamic viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();

            DataContext = this.viewModel;
        }
    }
}

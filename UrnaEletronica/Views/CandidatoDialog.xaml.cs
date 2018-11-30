using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interação lógica para UsuarioDialog.xam
    /// </summary>
    public partial class CandidatoDialog : UserControl
    {
        AdminViewModel viewModel;

        public CandidatoDialog(AdminViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();

            DataContext = this.viewModel;
        }
    }
}

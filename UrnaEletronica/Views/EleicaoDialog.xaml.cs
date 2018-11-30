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
    public partial class EleicaoDialog : UserControl
    {
        AdminViewModel viewModel;

        public EleicaoDialog(dynamic viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();

            DataContext = this.viewModel;
        }

        private void EleicaoCandidatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var candidatos = new List<Candidato>();
            foreach (var item in EleicaoCandidatos.SelectedItems)
            {
                candidatos.Add(item as Candidato);
            }
            viewModel.EleicaoSelecionada.Candidatos = new ObservableCollection<Candidato>(candidatos);
        }

        private void EleicaoCargos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cargos = new List<Cargo>();
            foreach (var item in EleicaoCargos.SelectedItems)
            {
                cargos.Add(item as Cargo);
            }
            viewModel.EleicaoSelecionada.Cargos = new ObservableCollection<Cargo>(cargos);
        }
    }
}

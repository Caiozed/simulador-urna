using MaterialDesignThemes.Wpf;
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
using Forge.Forms;

namespace UrnaEletronica.Views
{
    /// <summary>
    /// Interação lógica para AdminView.xam
    /// </summary>
    public partial class AdminView : Page
    {
        AdminViewModel viewModel;
        public AdminView()
        {
            viewModel = new AdminViewModel();
            InitializeComponent();

            DataContext = viewModel;
        }

        // EVentos de usuarios
        private void NovoUsuario_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UsuarioSelecionado = new Usuario();
            viewModel.UsuarioSelecionado.viewModel = viewModel;

            AppViewModel.Instance.Dialog.ShowDialog(new CadastroDialog(viewModel.UsuarioSelecionado));
        }

        private void UsuarioDatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.UsuarioSelecionado.viewModel = viewModel;
            AppViewModel.Instance.Dialog.ShowDialog(new CadastroDialog(viewModel.UsuarioSelecionado));
        }

        private async void UsuarioDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var datagrid = sender as DataGrid;
            if (e.Key == Key.Delete && datagrid.SelectedIndex > -1)
            {
                var result = await Show.Window().For(new Confirmation("Deseja remover o usuario?", "REMOVER"));
                if (result.Action.ToString() == "positive")
                {
                    viewModel.DeleteUser();
                }
            }
        }


        // Eventos de eleicao
        private void NovaEleicao_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EleicaoSelecionada = new Eleicao();
            viewModel.EleicaoSelecionada.viewModel = viewModel;

            AppViewModel.Instance.Dialog.ShowDialog(new EleicaoDialog(viewModel));
        }

        private async void EleicaoDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var datagrid = sender as DataGrid;
            if (e.Key == Key.Delete && datagrid.SelectedIndex > -1)
            {
                var result = await Show.Window().For(new Confirmation("Deseja remover a eleicao?", "REMOVER"));
                if (result.Action.ToString() == "positive")
                {
                    viewModel.DeleteEleicao();
                }
            }
        }

        private void EleicaoDatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.EleicaoSelecionada.viewModel = viewModel;
            AppViewModel.Instance.Dialog.ShowDialog(new EleicaoDialog(viewModel));
        }


        // Eventos de cargos
        private void CargoDatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.CargoSelecionado.viewModel = viewModel;
            AppViewModel.Instance.Dialog.ShowDialog(new CadastroDialog(viewModel.CargoSelecionado));
        }

        private async void CargoDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var datagrid = sender as DataGrid;
            if (e.Key == Key.Delete && datagrid.SelectedIndex > -1)
            {
                var result = await Show.Window().For(new Confirmation("Deseja remover o cargo?", "REMOVER"));
                if (result.Action.ToString() == "positive")
                {
                    viewModel.DeleteCargo();
                }
            }
        }

        private void NovoCargo_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CargoSelecionado = new Cargo();
            viewModel.CargoSelecionado.viewModel = viewModel;

            AppViewModel.Instance.Dialog.ShowDialog(new CadastroDialog(viewModel.CargoSelecionado));
        }

        // Eventos de candidatos
        private async void CandidatoDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var datagrid = sender as DataGrid;
            if (e.Key == Key.Delete && datagrid.SelectedIndex > -1)
            {
                var result = await Show.Window().For(new Confirmation("Deseja remover o candidato?", "REMOVER"));
                if (result.Action.ToString() == "positive")
                {
                    viewModel.DeleteCandidato();
                }
            }
        }

        private void CandidatoDatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.CandidatoSelecionado.viewModel = viewModel;
            AppViewModel.Instance.Dialog.ShowDialog(new CandidatoDialog(viewModel));
        }

        private void NovoCandidato_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CandidatoSelecionado = new Candidato();
            viewModel.CandidatoSelecionado.viewModel = viewModel;

            AppViewModel.Instance.Dialog.ShowDialog(new CandidatoDialog(viewModel));
        }

        private void Resultados_Click(object sender, RoutedEventArgs e)
        {
            if (eleicao_datagrid.SelectedIndex < 0)
            {
                Forge.Forms.Show.Window().For(new Alert("Nenhuma eleição selecionada!", "ERRO"));
                return;
            }

            AppViewModel.Instance.Dialog.ShowDialog(new ResultadoDialog(viewModel));
        }
    }
}

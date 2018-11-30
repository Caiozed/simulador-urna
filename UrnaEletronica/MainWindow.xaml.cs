using Forge.Forms;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using UrnaEletronica.Views;
using ForgeShow = Forge.Forms.Show;

namespace UrnaEletronica
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        AppViewModel viewModel;
        public MainWindow()
        {
            viewModel = new AppViewModel();

            InitializeComponent();
            DataContext = viewModel;

            AppViewModel.Instance = viewModel;

            viewModel.Frame = main_frame;
            viewModel.Dialog = Dialog;
            viewModel.Menu = Menu;

            this.KeyDown += HandleKeydown;

            main_frame.Navigate(new LoginView());
        }

        public void HandleKeydown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    if (Dialog.IsOpen)
                    {
                        Dialog.IsOpen = false;
                    }
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Dialog.ShowDialog(new LoginDialog());
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            main_frame.Navigate(new AdminView());
            Menu.IsOpen = false;
        }

        private void Eleicao_Click(object sender, RoutedEventArgs e)
        {
            main_frame.Navigate(new EleicaoView());
            Menu.IsOpen = false;
        }

        private async void Usuario_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsOpen = false;
            if (AppViewModel.Instance.Dialog.IsOpen) return;

            var result = await ForgeShow.Dialog().For(new Prompt<string> { Title = "SELECIONE O USUARIO", Value="INSIRA O CPF AQUI" });
            if (result.Action == null) return;

            if (result.Action.ToString() == "positive")
            {
                var usuarios = AppViewModel.Instance.Db.GetCollection<Usuario>("usuarios").FindAll();
                if (usuarios.Where(p => p.Cpf == result.Model.Value).Count() > 0)
                {
                    var usuario = usuarios.Where(p => p.Cpf == result.Model.Value).First();
                    if (!usuario.Bloqueado)
                    {
                        if (AppViewModel.Instance.EleicaoAtual.Votos.Any(u => u.Usuario.Cpf == usuario.Cpf))
                        {
                            await ForgeShow.Dialog().For(new Alert("Usuario selecionado já votou!", "ERRO"));
                            return;
                        }
                        else
                        {
                            AppViewModel.Instance.UsuarioAtual = usuario;
                            AppViewModel.Instance.Inicio = Visibility.Hidden;
                            AppViewModel.Instance.Finalizado = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        await ForgeShow.Dialog().For(new Alert("Usuario bloqueado!", "ERRO"));
                        return;
                    }
                }
                else
                {
                    await ForgeShow.Dialog().For(new Alert("Usuario não encontrado!", "ERRO"));
                }
            }
        }

        private async void Sair_Click(object sender, RoutedEventArgs e)
        {
            var result = await ForgeShow.Dialog().For(new Confirmation("Realmente deseja sair?", "SAIR"));
            if (result.Action.ToString() == "positive")
            {
                Process.Start(Application.ResourceAssembly.Location);
                Environment.Exit(0);
            }
        }
    }
}

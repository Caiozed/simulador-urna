using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    public partial class ResultadoDialog : UserControl
    {
        AdminViewModel viewModel;

        public ResultadoDialog(AdminViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();

            DataContext = this.viewModel;
        }

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();

            var headers = datagrid.Columns;
            sb.AppendLine(string.Join(" | ", headers.Select(column => column.Header).ToArray()));

            foreach (var voto in viewModel.EleicaoSelecionada.Votos)
            {
                sb.AppendLine(voto.Usuario.Nome + " | " + voto.Candidato.Nome + " | " + voto.Candidato.Cargo.Nome);
            }

            var dialog = new SaveFileDialog();
            dialog.DefaultExt = "txt";
            if ((bool)dialog.ShowDialog())
            {
                File.WriteAllText(dialog.FileName, sb.ToString(), UnicodeEncoding.UTF8);
            }
        }
    }
}

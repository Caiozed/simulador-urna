using Forge.Forms;
using Forge.Forms.Annotations;
using LiteDB;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.ViewModels;

namespace UrnaEletronica.Models
{
    [Action("cancel", "CANCELAR", IsReset = false, IsCancel = true)]
    [Action("register", "SALVAR", Validates = true, IsDefault = true)]
    public class Eleicao : BindableBase, IActionHandler
    {

        [BsonIgnore]
        public AdminViewModel viewModel;

        ObjectId _id;
        [FieldIgnore]
        public ObjectId Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        string _nome;
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); }
        }

        DateTime _dataInicial;
        [Time]
        public DateTime DataInicial
        {
            get { return _dataInicial; }
            set { SetProperty(ref _dataInicial, value); }
        }

        DateTime _dataTermino;
        [Time]
        public DateTime DataTermino
        {
            get { return _dataTermino; }
            set { SetProperty(ref _dataTermino, value); }
        }

        ObservableCollection<Status> _listaStatus;
        [BsonIgnore]
        public ObservableCollection<Status> ListaStatus
        {
            get { return _listaStatus; }
            set { SetProperty(ref _listaStatus, value); }
        }

        Status _statusAtual;
        public Status StatusAtual
        {
            get { return _statusAtual; }
            set { SetProperty(ref _statusAtual, value); }
        }

        ObservableCollection<Cargo> _cargos;
        public ObservableCollection<Cargo> Cargos
        {
            get { return _cargos; }
            set { SetProperty(ref _cargos, value); }
        }

        ObservableCollection<Voto> _votos;
        public ObservableCollection<Voto> Votos
        {
            get { return _votos; }
            set { SetProperty(ref _votos, value); }
        }

        ObservableCollection<Candidato> _candidatos;
        public ObservableCollection<Candidato> Candidatos
        {
            get { return _candidatos; }
            set { SetProperty(ref _candidatos, value); }
        }

        public Eleicao()
        {
            DataInicial = DateTime.Now;
            DataTermino = DateTime.Now;
            Votos = new ObservableCollection<Voto>();
            Candidatos = new ObservableCollection<Candidato>();
            Cargos = new ObservableCollection<Cargo>();
            ListaStatus = new ObservableCollection<Status>(Enum.GetValues(typeof(Status)).Cast<Status>().ToList());
        }

        public void HandleAction(IActionContext actionContext)
        {
            if (viewModel.EleicaoSelecionada == null)
                return;

            switch (actionContext.Action)
            {
                case "cancel":
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
                case "register":
                    if (Id != null)
                    {
                        viewModel.UpdateEleicao();
                    }
                    else
                    {
                        viewModel.AddEleicao();
                    }
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
            }
        }

        public enum Status
        {
            Ativa,
            Cancelada,
            Planejada,
            Finalizada
        }
    }
}

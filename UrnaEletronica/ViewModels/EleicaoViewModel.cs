using Forge.Forms;
using LiteDB;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UrnaEletronica.Models;
using Status = UrnaEletronica.Models.Eleicao.Status;

namespace UrnaEletronica.ViewModels
{
    public class EleicaoViewModel : BindableBase
    {
        public DelegateCommand<string> AddNumberCommand { get; set; }
        public DelegateCommand CorrigeCommand { get; set; }
        public DelegateCommand ConfirmaCommand { get; set; }


        private LiteCollection<Eleicao> _eleicaoDb;
        public LiteCollection<Eleicao> EleicaoDb
        {
            get { return _eleicaoDb; }
            set { SetProperty(ref _eleicaoDb, value); }
        }

        private Candidato _candidatoSelecionado;
        public Candidato CandidatoSelecionado
        {
            get { return _candidatoSelecionado; }
            set { SetProperty(ref _candidatoSelecionado, value); }
        }

        private Cargo _cargoSelecionado;
        public Cargo CargoSelecionado
        {
            get { return _cargoSelecionado; }
            set { SetProperty(ref _cargoSelecionado, value); }
        }

        private AppViewModel _appViewModel;
        public AppViewModel AppViewModel
        {
            get { return _appViewModel; }
            set { SetProperty(ref _appViewModel, value); }
        }

        private ObservableCollection<string> _numeroCandidato;
        public ObservableCollection<string> NumeroCandidato
        {
            get { return _numeroCandidato; }
            set { SetProperty(ref _numeroCandidato, value); }
        }

        private int _cargoIndex;
        public int CargoIndex
        {
            get { return _cargoIndex; }
            set { SetProperty(ref _cargoIndex, value); }
        }

        public EleicaoViewModel()
        {
            AppViewModel = AppViewModel.Instance;
            CargoIndex = 0;

            NumeroCandidato = new ObservableCollection<string>();
            AddNumberCommand = new DelegateCommand<string>(AddNumber);
            CorrigeCommand = new DelegateCommand(Corrige);
            ConfirmaCommand = new DelegateCommand(Confirma);

            EleicaoDb = AppViewModel.Instance.Db.GetCollection<Eleicao>("eleicoes");
            if (EleicaoDb.FindAll().Where(e => e.StatusAtual == Status.Ativa).Count() > 0)
            {
                AppViewModel.EleicaoAtual = EleicaoDb.FindAll().Where(e => e.StatusAtual == Status.Ativa).First();
                CargoSelecionado = AppViewModel.EleicaoAtual.Cargos[CargoIndex];
                CargoIndex++;
            }
            else
            {
                Show.Window().For(new Alert("Nenhuma eleição ativa encontrada!", "ERRO"));
            }
        }

        public void AddNumber(string number)
        {
            SoundPlayer player = new System.Media.SoundPlayer(@"Resources\click.wav");
            player.Play();
            if (NumeroCandidato.Count <= 3)
            {
                NumeroCandidato.Add(number);
            }

            var Numero = String.Join(String.Empty, NumeroCandidato.ToArray());
            var candidatos = AppViewModel.Instance.EleicaoAtual.Candidatos;

            if (candidatos.Where(e => e.Numero == Numero && e.Cargo.Id == CargoSelecionado.Id).Count() > 0)
            {
                CandidatoSelecionado = candidatos.Where(e => e.Numero == Numero).First();
            }
            else
            {
                CandidatoSelecionado = new Candidato { Nome = "VOTO NULO" };
            }
        }

        public void Corrige()
        {
            NumeroCandidato.Clear();
            CandidatoSelecionado = new Candidato();
        }

        public void Confirma()
        {
            if (CandidatoSelecionado.Id != ObjectId.Empty)
            {
                if (CargoIndex == AppViewModel.EleicaoAtual.Cargos.Count())
                {

                    AppViewModel.EleicaoAtual.Votos.Add(new Voto { Candidato = CandidatoSelecionado, Usuario = AppViewModel.Instance.UsuarioAtual });

                    EleicaoDb.Update(AppViewModel.EleicaoAtual);

                    AppViewModel.Finalizado = Visibility.Visible;

                    NumeroCandidato.Clear();
                    CandidatoSelecionado = new Candidato();
                    SoundPlayer player = new System.Media.SoundPlayer(@"Resources\confirma.wav");
                    player.Play();
                }
                else
                {
                    AppViewModel.EleicaoAtual.Votos.Add(new Voto { Candidato = CandidatoSelecionado, Usuario = AppViewModel.Instance.UsuarioAtual });
                    NumeroCandidato.Clear();
                    CandidatoSelecionado = new Candidato();
                    CargoSelecionado = AppViewModel.EleicaoAtual.Cargos[CargoIndex];
                    CargoIndex++;
                }

            }
        }
    }
}

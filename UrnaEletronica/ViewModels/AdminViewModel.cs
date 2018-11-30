using Forge.Forms;
using LiteDB;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Models;
using static UrnaEletronica.Models.Eleicao;

namespace UrnaEletronica.ViewModels
{
    public class AdminViewModel : BindableBase
    {
        public DelegateCommand<string> SaveEleicaoCommand { get; set; }
        public DelegateCommand<string> SaveCandidatoCommand { get; set; }


        LiteCollection<Usuario> _usuariosDB;
        public LiteCollection<Usuario> UsuariosDB
        {
            get { return _usuariosDB; }
            set { SetProperty(ref _usuariosDB, value); }
        }

        ObservableCollection<Usuario> _usuarios;
        public ObservableCollection<Usuario> Usuarios
        {
            get { return _usuarios; }
            set { SetProperty(ref _usuarios, value); }
        }

        Usuario _usuarioSelecionado;
        public Usuario UsuarioSelecionado
        {
            get { return _usuarioSelecionado; }
            set { SetProperty(ref _usuarioSelecionado, value); }
        }

        // ElEicao
        Eleicao _eleicaoSelecionada;
        public Eleicao EleicaoSelecionada
        {
            get { return _eleicaoSelecionada; }
            set { SetProperty(ref _eleicaoSelecionada, value); }
        }

        LiteCollection<Eleicao> _eleicoesDB;
        public LiteCollection<Eleicao> EleicoesDB
        {
            get { return _eleicoesDB; }
            set { SetProperty(ref _eleicoesDB, value); }
        }

        ObservableCollection<Eleicao> _eleicoes;
        public ObservableCollection<Eleicao> Eleicoes
        {
            get { return _eleicoes; }
            set { SetProperty(ref _eleicoes, value); }
        }

        // Dados de candidato
        Candidato _cadidatoSelecionado;
        public Candidato CandidatoSelecionado
        {
            get { return _cadidatoSelecionado; }
            set { SetProperty(ref _cadidatoSelecionado, value); }
        }

        LiteCollection<Candidato> _candidatoDB;
        public LiteCollection<Candidato> CandidatoDB
        {
            get { return _candidatoDB; }
            set { SetProperty(ref _candidatoDB, value); }
        }

        ObservableCollection<Candidato> _candidatos;
        public ObservableCollection<Candidato> Candidatos
        {
            get { return _candidatos; }
            set { SetProperty(ref _candidatos, value); }
        }

        // Dados de cargos
        Cargo _cargoSelecionado;
        public Cargo CargoSelecionado
        {
            get { return _cargoSelecionado; }
            set { SetProperty(ref _cargoSelecionado, value); }
        }

        LiteCollection<Cargo> _cargoDB;
        public LiteCollection<Cargo> CargoDB
        {
            get { return _cargoDB; }
            set { SetProperty(ref _cargoDB, value); }
        }

        ObservableCollection<Cargo> _cargos;
        public ObservableCollection<Cargo> Cargos
        {
            get { return _cargos; }
            set { SetProperty(ref _cargos, value); }
        }

        public AdminViewModel()
        {
            SaveEleicaoCommand = new DelegateCommand<string>(SaveEleicao);
            SaveCandidatoCommand = new DelegateCommand<string>(SaveCandidato);

            UsuariosDB = AppViewModel.Instance.Db.GetCollection<Usuario>("usuarios");
            Usuarios = new ObservableCollection<Usuario>(UsuariosDB.FindAll());
            UsuarioSelecionado = new Usuario();
            UsuarioSelecionado.viewModel = this;

            EleicoesDB = AppViewModel.Instance.Db.GetCollection<Eleicao>("eleicoes");
            Eleicoes = new ObservableCollection<Eleicao>(EleicoesDB.FindAll());
            EleicaoSelecionada = new Eleicao();
            EleicaoSelecionada.viewModel = this;

            CandidatoDB = AppViewModel.Instance.Db.GetCollection<Candidato>("candidatos");
            Candidatos = new ObservableCollection<Candidato>(CandidatoDB.FindAll());
            CandidatoSelecionado = new Candidato();
            CandidatoSelecionado.viewModel = this;

            CargoDB = AppViewModel.Instance.Db.GetCollection<Cargo>("cargos");
            Cargos = new ObservableCollection<Cargo>(CargoDB.FindAll());
            CargoSelecionado = new Cargo();
            CargoSelecionado.viewModel = this;
        }

        public void SaveEleicao(string action)
        {
            if (EleicaoSelecionada == null)
                return;


            if (EleicaoSelecionada.Candidatos.Count == 0 && action == "register")
            {
                Show.Window().For(new Alert("Selecione os candidatos!", "ERRO"));
                return;
            }


            if (EleicaoSelecionada.Cargos.Count == 0 && action == "register")
            {
                Show.Window().For(new Alert("Selecione os cargos!", "ERRO"));
                return;
            }


            if (Eleicoes.Any(s => s.StatusAtual == Status.Ativa && s.Id != EleicaoSelecionada.Id) && action == "register" && EleicaoSelecionada.StatusAtual == Status.Ativa)
            {
                Show.Window().For(new Alert("Já existe eleição ativa!", "ERRO"));
                return;
            }


            switch (action)
            {
                case "cancel":
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
                case "register":
                    if (EleicaoSelecionada.Id != null)
                    {
                        UpdateEleicao();
                    }
                    else
                    {
                        AddEleicao();
                    }
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
            }
        }

        public void SaveCandidato(string action)
        {
            if (CandidatoSelecionado == null)
                return;

            if (CandidatoSelecionado.Cargo == null && action == "register")
            {
                Show.Window().For(new Alert("Selecione um cargo!", "ERRO"));
                return;
            }


            switch (action)
            {
                case "cancel":
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
                case "register":
                    if (CandidatoSelecionado.Id != null)
                    {
                        UpdateCandidato();
                    }
                    else
                    {
                        AddCandidato();
                    }
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
            }
        }

        // User
        public void AddUser()
        {
            UsuariosDB.Insert(UsuarioSelecionado);
            Usuarios = new ObservableCollection<Usuario>(UsuariosDB.FindAll());
        }

        public void UpdateUser()
        {
            UsuariosDB.Update(UsuarioSelecionado);
            Usuarios = new ObservableCollection<Usuario>(UsuariosDB.FindAll());
        }

        public void DeleteUser()
        {
            UsuariosDB.Delete(UsuarioSelecionado.Id);
            Usuarios = new ObservableCollection<Usuario>(UsuariosDB.FindAll());
        }


        // Eleicao 
        public void AddEleicao()
        {
            EleicoesDB.Insert(EleicaoSelecionada);
            Eleicoes = new ObservableCollection<Eleicao>(EleicoesDB.FindAll());
        }

        public void UpdateEleicao()
        {
            EleicoesDB.Update(EleicaoSelecionada);
            Eleicoes = new ObservableCollection<Eleicao>(EleicoesDB.FindAll());
        }

        public void DeleteEleicao()
        {
            EleicoesDB.Delete(EleicaoSelecionada.Id);
            Eleicoes = new ObservableCollection<Eleicao>(EleicoesDB.FindAll());
        }

        // Candidatos 
        public void AddCandidato()
        {
            CandidatoDB.Insert(CandidatoSelecionado);
            Candidatos = new ObservableCollection<Candidato>(CandidatoDB.FindAll());
        }

        public void UpdateCandidato()
        {
            CandidatoDB.Update(CandidatoSelecionado);
            Candidatos = new ObservableCollection<Candidato>(CandidatoDB.FindAll());
        }

        public void DeleteCandidato()
        {
            CandidatoDB.Delete(CandidatoSelecionado.Id);
            Candidatos = new ObservableCollection<Candidato>(CandidatoDB.FindAll());
        }

        // Candidatos 
        public void AddCargo()
        {
            CargoDB.Insert(CargoSelecionado);
            Cargos = new ObservableCollection<Cargo>(CargoDB.FindAll());
        }

        public void UpdateCargo()
        {
            CargoDB.Update(CargoSelecionado);
            Cargos = new ObservableCollection<Cargo>(CargoDB.FindAll());
        }

        public void DeleteCargo()
        {
            CargoDB.Delete(CargoSelecionado.Id);
            Cargos = new ObservableCollection<Cargo>(CargoDB.FindAll());
        }
    }
}

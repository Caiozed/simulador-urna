using Forge.Forms;
using Forge.Forms.Annotations;
using LiteDB;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.ViewModels;

namespace UrnaEletronica.Models
{
    [Action("cancel", "CANCELAR", IsReset = false, IsCancel = true)]
    [Action("register", "SALVAR", Validates = true, IsDefault = true)]
    public class Usuario : BindableBase, IActionHandler
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

        string _cpf;
        public string Cpf
        {
            get { return _cpf; }
            set { SetProperty(ref _cpf, value); }
        }

        int _idInterno;
        public int IdInterno
        {
            get { return _idInterno; }
            set { SetProperty(ref _idInterno, value); }
        }

        string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        string _senha;
        public string Senha
        {
            get { return _senha; }
            set { SetProperty(ref _senha, value); }
        }

        bool _admin;
        public bool Admin
        {
            get { return _admin; }
            set { SetProperty(ref _admin, value); }
        }

        bool _bloqueado;
        public bool Bloqueado
        {
            get { return _bloqueado; }
            set { SetProperty(ref _bloqueado, value); }
        }

        public Usuario()
        {

        }

        public void HandleAction(IActionContext actionContext)
        {
            if (viewModel.UsuarioSelecionado == null)
                return;

            switch (actionContext.Action)
            {
                case "cancel":                    
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
                case "register":
                    if (Id != null)
                    {
                        viewModel.UpdateUser();
                    }
                    else
                    {
                        viewModel.AddUser();
                    }
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
            }
        }
    }
}

using Forge.Forms;
using Forge.Forms.Annotations;
using LiteDB;
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
    public class Candidato : BindableBase, IActionHandler
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

        string _numero;
        public string Numero
        {
            get { return _numero; }
            set { SetProperty(ref _numero, value); }
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

        string _foto;
        public string Foto
        {
            get { return _foto; }
            set { SetProperty(ref _foto, value); }
        }

        string _legenda;
        public string Legenda
        {
            get { return _legenda; }
            set { SetProperty(ref _legenda, value); }
        }

        Cargo _cargo;
        public Cargo Cargo
        {
            get { return _cargo; }
            set { SetProperty(ref _cargo, value); }
        }

        public void HandleAction(IActionContext actionContext)
        {
            if (viewModel.CandidatoSelecionado == null)
                return;

            switch (actionContext.Action)
            {
                case "cancel":
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
                case "register":
                    if (Id != null)
                    {
                        viewModel.UpdateCandidato();
                    }
                    else
                    {
                        viewModel.AddCandidato();
                    }
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
            }
        }
    }
}

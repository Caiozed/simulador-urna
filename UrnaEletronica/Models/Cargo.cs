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
    public class Cargo : BindableBase, IActionHandler
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

        public void HandleAction(IActionContext actionContext)
        {
            if (viewModel.CargoSelecionado == null)
                return;

            switch (actionContext.Action)
            {
                case "cancel":
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
                case "register":
                    if (Id != null)
                    {
                        viewModel.UpdateCargo();
                    }
                    else
                    {
                        viewModel.AddCargo();
                    }
                    AppViewModel.Instance.Dialog.IsOpen = false;
                    break;
            }
        }
    }
}

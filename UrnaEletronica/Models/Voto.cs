using LiteDB;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Models
{
    public class Voto : BindableBase
    {
        Candidato _candidato;
        public Candidato Candidato
        {
            get { return _candidato; }
            set { SetProperty(ref _candidato, value); }
        }

        Usuario _usuario;
        public Usuario Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }
    }
}

using LiteDB;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UrnaEletronica.Models;

namespace UrnaEletronica.ViewModels
{
    public class AppViewModel : BindableBase
    {
        public static AppViewModel Instance;
        public Frame Frame;
        public LiteDatabase Db;
        public DialogHost Dialog;
        public Flyout Menu;

        Visibility _menuVisibility;
        public Visibility MenuVisibility
        {
            get { return _menuVisibility; }
            set { SetProperty(ref _menuVisibility, value); }
        }

        private Visibility _inicio;
        public Visibility Inicio
        {
            get { return _inicio; }
            set { SetProperty(ref _inicio, value); }
        }

        private Visibility _finalizado;
        public Visibility Finalizado
        {
            get { return _finalizado; }
            set { SetProperty(ref _finalizado, value); }
        }

        private Usuario _usuarioAtual;
        public Usuario UsuarioAtual
        {
            get { return _usuarioAtual; }
            set { SetProperty(ref _usuarioAtual, value); }
        }

        private Eleicao _eleicaoAtual;
        public Eleicao EleicaoAtual
        {
            get { return _eleicaoAtual; }
            set { SetProperty(ref _eleicaoAtual, value); }
        }

        public AppViewModel()
        {
            Finalizado = MenuVisibility = Visibility.Hidden;
            Inicio = Visibility.Visible;
            Instance = this;
            Db = new LiteDatabase(@"data.db");

            if (Db.GetCollection<Usuario>("usuarios").FindAll().Count() == 0)
            {
                Db.GetCollection<Usuario>("usuarios").Insert(new Usuario { Nome = "admin", Admin = true, Cpf = "0000000000", IdInterno = 1, Senha = "admin" });
            }
        }
    }
}

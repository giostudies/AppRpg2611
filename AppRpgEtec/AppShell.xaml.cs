﻿using AppRpgEtec.Views.Personagens;

namespace AppRpgEtec
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            string login = Preferences.Get("Usuarioname", string.Empty);
            lblLogin.Text = login;

            Routing.RegisterRoute("cadPersonagemView", typeof(CadastroPersonagemView));
        }
    }
}
using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Disputas
{
    public class DisputaViewModel : BaseViewModel
    {

        private PersonagemService pService;
        public ObservableCollection<Personagem> PersonagensEncontrados { get; set; }
        public Personagem Atacante { get; set; }
        public Personagem Oponente { get; set; }
        public DisputaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            Atacante = new Personagem();
            Oponente = new Personagem();
            PersonagensEncontrados = new ObservableCollection<Personagem>();


            PesquisaPersonagemCommand =
                new Command<string>(async (string pesquisa) => { await PesquisaPersonagens(pesquisa); });

        }

        public ICommand PesquisaPersonagemCommand { get; set; }

        public async Task PesquisaPersonagens(string textoPesquisaPersonagem)
        {
            try
            {
                PersonagensEncontrados = await pService.GetByNomeAproximadoAsync(textoPesquisaPersonagem);
                OnPropertyChanged(nameof(PersonagensEncontrados));
            }

            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + "Detalhes:" + ex.InnerException, "OK");
            }
        }

        public string DescricaoPersonagemAtacante
        {
            get => Atacante.Nome;
        }

        public string DescricaoPersonagemOponente
        {
            get => Oponente.Nome;
        }

        public async void SelecionarPersonagem(Personagem p)
        {
            try
            {
                string tipoCombatente = await Application.Current.MainPage
                        .DisplayActionSheet("Atacante ou oponente?", "Cancelar", "", "Atacante", "Opononente");

                if (tipoCombatente == "Atacante")
                {
                    Atacante = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemAtacante));
                }
                else if (tipoCombatente == "Oponente")
                {
                    Oponente = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemOponente));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + "Detalhes" + ex.InnerException, "Ok");
            }
        }

        private Personagem personagemSelecionado;
        public Personagem PersonagemSelecionado
        {
            set
            {
                if (value != null)
                {
                    Personagem personagem = value;
                    SelecionarPersonagem(personagemSelecionado);
                    OnPropertyChanged();
                    PersonagensEncontrados.Clear();
                }
            }
        }

        public string textoBuscaDigitado = string.Empty;

        public string TextoBuscaDigitado
        {
            get { return textoBuscaDigitado; }
            set
            {
                if ((value != null && !string.IsNullOrEmpty(value) && value.Length > 0))
                {
                    textoBuscaDigitado = value;
                    _ = PesquisaPersonagens(textoBuscaDigitado);
                }
                else
                {
                    PersonagensEncontrados.Clear();
                }

            }
        }
    }
}

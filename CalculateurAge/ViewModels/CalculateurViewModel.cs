namespace CalculateurAge.ViewModels
{
    public class CalculateurViewModel : BaseViewModel
    {
        private string _nom = "";
        private DateTime _dateNaissance
            = DateTime.Today.AddYears(-20);
        private string _resultat = "";
        private bool _resultatVisible;
        private string _message = "";

        public string Nom
        {
            get => _nom;
            set { if (SetField(ref _nom, value)) 
                     CalculerCommand.Rafraichir(); }
        }

        public DateTime DateNaissance
        {
            get => _dateNaissance;
            set
            {
                if (SetField(ref _dateNaissance, value))
                {
                    OnPropertyChanged(nameof(DateFuture));
                    CalculerCommand.Rafraichir();
                }
            }
        }

        // Propriété calculée : pas de champ privé, la valeur se déduit de la date.
        public bool DateFuture => DateNaissance.Date > DateTime.Today;

        public string Resultat
        {
            get => _resultat;
            set => SetField(ref _resultat, value);
        }

        public string Message
        {
            get => _message;
            set => SetField(ref _message, value);
        }
        public bool ResultatVisible
        {
            get => _resultatVisible;
            set => SetField(ref _resultatVisible, value);
        }

        public RelayCommand CalculerCommand { get; }
        public RelayCommand EffacerCommand { get; }

        public CalculateurViewModel()
        {
            CalculerCommand = new RelayCommand(
                Calculer,
                () => !string.IsNullOrWhiteSpace(Nom) && !DateFuture);

            EffacerCommand = new RelayCommand(Effacer);
        }

        private void Calculer()
        {
            int age = DateTime.Today.Year - DateNaissance.Year;
            if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

            Resultat = $"{Nom}, vous avez {age} ans.";
            Message = age >= 18 ? "Majeur" : "Mineur";
            ResultatVisible = true;
        }

        private void Effacer()
        {
            Nom = "";
            DateNaissance = DateTime.Today.AddYears(-20);
            Resultat = "";
            Message = "";
            ResultatVisible = false;
        }
    }
}

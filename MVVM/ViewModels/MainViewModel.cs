using System.Collections.ObjectModel;
using System.Windows.Input;
using Converter.MVVM.Models;

namespace Converter.MVVM.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string category;
        public string Category
        {
            get => category;
            set { category = value; OnPropertyChanged(); }
        }

        private double inputValue;
        public double InputValue
        {
            get => inputValue;
            set { inputValue = value; OnPropertyChanged(); }
        }

        private string selectedFrom;
        public string SelectedFrom
        {
            get => selectedFrom;
            set { selectedFrom = value; OnPropertyChanged(); }
        }

        private string selectedTo;
        public string SelectedTo
        {
            get => selectedTo;
            set { selectedTo = value; OnPropertyChanged(); }
        }

        private double result;
        public double Result
        {
            get => result;
            set { result = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Units { get; set; } = new();

        public ICommand SelectCategoryCommand { get; }
        public ICommand ConvertCommand { get; }

        public MainViewModel()
        {
            SelectCategoryCommand = new Command<string>(OnCategorySelected);
            ConvertCommand = new Command(ConvertValue);
        }

        private async void OnCategorySelected(string category)
        {
            Category = category;

            Units.Clear();
            var newUnits = category switch
            {
                "Length" => new[] { "Meter", "Centimeter", "Kilometer", "Millimeter" },
                "Mass" => new[] { "Gram", "Kilogram", "Pound" },
                "Area" => new[] { "SquareMeter", "SquareKilometer", "SquareCentimeter" },
                "Volume" => new[] { "Liter", "Milliliter", "CubicMeter" },
                "Temperature" => new[] { "DegreeCelsius", "DegreeFahrenheit", "Kelvin" },
                _ => Array.Empty<string>()
            };

            foreach (var unit in newUnits)
                Units.Add(unit);

            if (Units.Any())
            {
                SelectedFrom = Units.First();
                SelectedTo = Units.Last();
            }

            await Shell.Current.GoToAsync($"ResultPage?category={Category}");
        }



        private void ConvertValue()
        {
            if (string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(SelectedFrom) ||
                string.IsNullOrWhiteSpace(SelectedTo))
                return;

            Result = UnitConverter.Convert(Category, InputValue, SelectedFrom, SelectedTo);
        }
    }
}

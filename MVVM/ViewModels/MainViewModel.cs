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

        private string inputValue;
        public string InputValue
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

        public ObservableCollection<string> Units { get; } = new();

        public ICommand SelectCategoryCommand { get; }
        public ICommand ConvertCommand { get; }

        public MainViewModel()
        {
            SelectCategoryCommand = new Command<string>(async category =>
            {
                await Shell.Current.GoToAsync($"ResultPage?category={category}");
            });

            ConvertCommand = new Command(ConvertValue);
        }

        public void LoadUnits(string category)
        {
            Category = category;
            Units.Clear();

            string[] newUnits = category switch
            {
                "Length" => new[] { "Meter", "Centimeter", "Kilometer", "Millimeter" },
                "Mass" => new[] { "Gram", "Kilogram", "Milligram", "Pound" },
                "Area" => new[] { "SquareMeter", "SquareKilometer", "SquareCentimeter", "Hectare" },
                "Volume" => new[] { "Liter", "Milliliter", "CubicMeter", "CubicCentimeter" },
                "Temperature" => new[] { "DegreeCelsius", "DegreeFahrenheit", "Kelvin", "DegreeRankine" },
                "Information" => new[] { "Byte", "Kilobyte", "Megabyte", "Gigabyte" },
                "Energy" => new[] { "Joule", "Kilojoule", "Calorie", "KilowattHour" },
                "Speed" => new[] { "MeterPerSecond", "KilometerPerHour", "MilePerHour", "FootPerSecond" },
                "Duration" => new[] { "Second", "Minute", "Hour", "Day" },
                _ => Array.Empty<string>()
            };

            foreach (var unit in newUnits)
                Units.Add(unit);

            SelectedFrom = Units.FirstOrDefault();
            SelectedTo = Units.LastOrDefault();
        }

        private void ConvertValue()
        {
            if (!double.TryParse(InputValue, out double value))
                return;

            try
            {
                Result = UnitConverter.Convert(Category, value, SelectedFrom, SelectedTo);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}

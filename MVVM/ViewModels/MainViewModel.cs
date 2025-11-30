using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Converter.MVVM.Models;

namespace Converter.MVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string n = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));

        public ObservableCollection<string> Units { get; private set; } = new ObservableCollection<string>();

        private string selectedCategory;
        private string fromUnit;
        private string toUnit;
        private double inputValue;
        private double resultValue;

        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
                LoadUnits(); // update dropdown
            }
        }

        public string FromUnit
        {
            get => fromUnit;
            set { fromUnit = value; OnPropertyChanged(); }
        }

        public string ToUnit
        {
            get => toUnit;
            set { toUnit = value; OnPropertyChanged(); }
        }

        public double InputValue
        {
            get => inputValue;
            set { inputValue = value; OnPropertyChanged(); }
        }

        public double ResultValue
        {
            get => resultValue;
            set { resultValue = value; OnPropertyChanged(); }
        }

        // COMMANDS
        public ICommand CategoryCommand { get; }
        public ICommand ConvertCommand { get; }

        public MainViewModel()
        {
            CategoryCommand = new Command<string>((category) =>
            {
                SelectedCategory = category;
            });

            ConvertCommand = new Command(() =>
            {
                if (string.IsNullOrEmpty(FromUnit) || string.IsNullOrEmpty(ToUnit)) return;
                ResultValue = UnitConverter.Convert(SelectedCategory, InputValue, FromUnit, ToUnit);
            });
        }

        // Load Units for the selected category
        private void LoadUnits()
        {
            Units.Clear();

            switch (SelectedCategory)
            {
                case "Length":
                    Units.Add("Meters");
                    Units.Add("Centimeters");
                    Units.Add("Kilometers");
                    Units.Add("Inches");
                    Units.Add("Feet");
                    break;
                case "Mass":
                    Units.Add("Kilograms");
                    Units.Add("Grams");
                    Units.Add("Pounds");
                    Units.Add("Ounces");
                    break;
                case "Temperature":
                    Units.Add("Celsius");
                    Units.Add("Fahrenheit");
                    Units.Add("Kelvin");
                    break;
                case "Area":
                    Units.Add("Square Meters");
                    Units.Add("Square Centimeters");
                    Units.Add("Square Kilometers");
                    Units.Add("Acres");
                    break;
                case "Volume":
                    Units.Add("Liters");
                    Units.Add("Milliliters");
                    Units.Add("Cubic Meters");
                    Units.Add("Gallons");
                    break;
                case "Current":
                    Units.Add("Amperes");
                    Units.Add("Milliamperes");
                    break;
                case "Intensity":
                    Units.Add("Candela");
                    break;
            }

            // Set default selected units
            FromUnit = Units.Count > 0 ? Units[0] : null;
            ToUnit = Units.Count > 1 ? Units[1] : Units.Count > 0 ? Units[0] : null;
        }
    }
}

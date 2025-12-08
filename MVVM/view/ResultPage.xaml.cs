using Converter.MVVM.ViewModels;

namespace Converter.MVVM.View
{
    [QueryProperty(nameof(Category), "category")]
    public partial class ResultPage : ContentPage
    {
        public string Category
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var vm = (MainViewModel)BindingContext;
                    vm.Category = value;

                    vm.Units = value switch
                    {
                        "Length" => new System.Collections.ObjectModel.ObservableCollection<string> { "Meter", "Centimeter", "Kilometer", "Millimeter" },
                        "Mass" => new System.Collections.ObjectModel.ObservableCollection<string> { "Gram", "Kilogram", "Pound" },
                        "Area" => new System.Collections.ObjectModel.ObservableCollection<string> { "SquareMeter", "SquareKilometer", "SquareCentimeter" },
                        "Volume" => new System.Collections.ObjectModel.ObservableCollection<string> { "Liter", "Milliliter", "CubicMeter" },
                        "Temperature" => new System.Collections.ObjectModel.ObservableCollection<string> { "DegreeCelsius", "DegreeFahrenheit", "Kelvin" },
                        _ => new System.Collections.ObjectModel.ObservableCollection<string>()
                    };

                    if (vm.Units.Count > 0)
                    {
                        vm.SelectedFrom = vm.Units[0];
                        vm.SelectedTo = vm.Units[^1]; // Last
                    }
                }
            }
        }
        public ResultPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}

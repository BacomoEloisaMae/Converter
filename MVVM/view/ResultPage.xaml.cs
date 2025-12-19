using Converter.MVVM.ViewModels;

namespace Converter.MVVM.View;

[QueryProperty(nameof(Category), "category")]
public partial class ResultPage : ContentPage
{
    public string Category
    {
        set
        {
            if (BindingContext is MainViewModel vm)
                vm.LoadUnits(value);
        }
    }

    public ResultPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }
}

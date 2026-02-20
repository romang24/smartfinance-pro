using SmartFinancePro.ViewModels;

namespace SmartFinancePro.Views;

public partial class TransactionsPage : ContentPage
{
    public TransactionsPage(TransactionsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is TransactionsViewModel vm)
        {
            vm.RefreshCommand.Execute(null);
        }
    }
}
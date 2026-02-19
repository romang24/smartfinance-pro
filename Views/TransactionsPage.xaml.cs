using SmartFinancePro.ViewModels;

namespace SmartFinancePro.Views;

public partial class TransactionsPage : ContentPage
{
    public TransactionsPage(TransactionsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}



using Microsoft.Extensions.DependencyInjection;
using SmartFinancePro.ViewModels;

namespace SmartFinancePro.Views;

public partial class EditTransactionPage : ContentPage
{
    public EditTransactionPage()
    {
        InitializeComponent();

        var sp = Application.Current?.Handler?.MauiContext?.Services;
        BindingContext = sp?.GetRequiredService<EditTransactionViewModel>();
    }
}
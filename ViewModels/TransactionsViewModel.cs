using System.Collections.ObjectModel;
using System.Windows.Input;
using SmartFinancePro.Models;

namespace SmartFinancePro.ViewModels;

public class TransactionsViewModel : BaseViewModel
{
    public ObservableCollection<Transaction> Transactions { get; } = new();

    public ICommand AddDummyCommand { get; }
    public ICommand DeleteCommand { get; }

    public TransactionsViewModel()
    {
        Title = "Transactions";

        Transactions.Add(new Transaction { Amount = 12.50m, Category = "Food", Date = new DateTime(2026, 2, 13) });
        Transactions.Add(new Transaction { Amount = 1200.00m, Category = "Salary", Date = new DateTime(2026, 2, 1) });
        Transactions.Add(new Transaction { Amount = 45.99m, Category = "Transport", Date = new DateTime(2026, 2, 10) });

        AddDummyCommand = new Command(() =>
            Transactions.Add(new Transaction
            {
                Amount = Random.Shared.Next(1, 200),
                Category = "Misc",
                Date = DateTime.Today
            })
        );

        DeleteCommand = new Command<Transaction>((item) =>
        {
            if (item is null) return;
            Transactions.Remove(item);
        });
    }
}

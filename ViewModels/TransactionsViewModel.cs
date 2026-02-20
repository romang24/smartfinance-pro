using System.Collections.ObjectModel;
using System.Windows.Input;
using SmartFinancePro.Interfaces;
using SmartFinancePro.Models;
using SmartFinancePro.Views;

namespace SmartFinancePro.ViewModels;

public class TransactionsViewModel : BaseViewModel
{
    private readonly IDatabaseService _database;

    public ObservableCollection<Transaction> Transactions { get; } = new();

    public ICommand AddDummyCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand EditCommand { get; }

    public TransactionsViewModel(IDatabaseService database)
    {
        _database = database;

        Title = "Transactions";

        AddDummyCommand = new Command(async () => await AddDummyAsync());
        DeleteCommand = new Command<Transaction>(async (item) => await DeleteAsync(item));
        RefreshCommand = new Command(async () => await LoadAsync());
        EditCommand = new Command<Transaction>(async (item) => await EditAsync(item));

        _ = InitializeAndLoadAsync();
    }

    private async Task InitializeAndLoadAsync()
    {
        await _database.InitializeAsync();
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Transactions.Clear();
            var items = await _database.GetTransactionsAsync();
            foreach (var t in items)
                Transactions.Add(t);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task AddDummyAsync()
    {
        var transaction = new Transaction
        {
            Amount = Random.Shared.Next(1, 200),
            Category = "Misc",
            Date = DateTime.Today,
            Note = ""
        };

        await _database.AddTransactionAsync(transaction);
        await LoadAsync();
    }

    private async Task DeleteAsync(Transaction? item)
    {
        if (item is null) return;

        await _database.DeleteTransactionAsync(item);
        Transactions.Remove(item);
    }

    private async Task EditAsync(Transaction? item)
    {
        if (item is null) return;

        var navParams = new Dictionary<string, object>
        {
            { "transaction", item }
        };

        await Shell.Current.GoToAsync(nameof(EditTransactionPage), navParams);
    }
        
}
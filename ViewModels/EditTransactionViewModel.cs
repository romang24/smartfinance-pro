using System.Windows.Input;
using SmartFinancePro.Interfaces;
using SmartFinancePro.Models;

namespace SmartFinancePro.ViewModels;

public class EditTransactionViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IDatabaseService _databaseService;

    private int _id;
    private decimal _amount;
    private string _category = string.Empty;
    private DateTime _date = DateTime.Today;
    private string _note = string.Empty;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public decimal Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }

    public string Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public string Note
    {
        get => _note;
        set => SetProperty(ref _note, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public EditTransactionViewModel(IDatabaseService databaseService)
    {
        _databaseService = databaseService;

        SaveCommand = new Command(async () => await SaveAsync(), () => !IsBusy);
        CancelCommand = new Command(async () => await CancelAsync(), () => !IsBusy);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("transaction", out var value) && value is Transaction t)
        {
            Id = t.Id;
            Amount = t.Amount;
            Category = t.Category ?? string.Empty;
            Date = t.Date;
            Note = t.Note ?? string.Empty;
        }
    }

    private bool Validate(out string error)
    {
        if (Amount <= 0)
        {
            error = "Amount must be greater than 0.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Category))
        {
            error = "Category is required.";
            return false;
        }

        error = string.Empty;
        return true;
    }

    private async Task SaveAsync()
    {
        if (IsBusy) return;

        IsBusy = true;
        ((Command)SaveCommand).ChangeCanExecute();
        ((Command)CancelCommand).ChangeCanExecute();

        try
        {
            if (!Validate(out var error))
            {
                await Shell.Current.DisplayAlertAsync("Validation", error, "OK");
                return;
            }

            var updated = new Transaction
            {
                Id = Id,
                Amount = Amount,
                Category = Category.Trim(),
                Date = Date,
                Note = Note?.Trim() ?? string.Empty
            };

            await _databaseService.UpdateTransactionAsync(updated);

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to update transaction: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            ((Command)SaveCommand).ChangeCanExecute();
            ((Command)CancelCommand).ChangeCanExecute();
        }
    }

    private async Task CancelAsync()
    {
        if (IsBusy) return;
        await Shell.Current.GoToAsync("..");
    }
}
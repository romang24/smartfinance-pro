using SmartFinancePro.Models;

namespace SmartFinancePro.Interfaces;

public interface IDatabaseService
{
    Task InitializeAsync();

    Task<List<Transaction>> GetTransactionsAsync();
    Task<int> AddTransactionAsync(Transaction transaction);
    Task<int> DeleteTransactionAsync(Transaction transaction);
    Task<int> UpdateTransactionAsync(Transaction transaction);
}
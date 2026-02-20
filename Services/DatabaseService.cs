using SQLite;
using SmartFinancePro.Interfaces;
using SmartFinancePro.Models;

namespace SmartFinancePro.Services;

public class DatabaseService : IDatabaseService
{
    private SQLiteAsyncConnection? _db;

    private async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (_db is not null)
            return _db;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "smartfinance.db3");
        _db = new SQLiteAsyncConnection(dbPath);

        // Create table on first use
        await _db.CreateTableAsync<Transaction>();

        return _db;
    }

    public async Task InitializeAsync()
    {
        // Force init + table creation
        _ = await GetConnectionAsync();
    }

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        var db = await GetConnectionAsync();
        return await db.Table<Transaction>()
                      .OrderByDescending(t => t.Date)
                      .ToListAsync();
    }

    public async Task<int> AddTransactionAsync(Transaction transaction)
    {
        var db = await GetConnectionAsync();
        return await db.InsertAsync(transaction);
    }

    public async Task<int> DeleteTransactionAsync(Transaction transaction)
    {
        var db = await GetConnectionAsync();
        return await db.DeleteAsync(transaction);
    }

    public async Task<int> UpdateTransactionAsync(Transaction transaction)
    {
        var db = await GetConnectionAsync();
        return await db.UpdateAsync(transaction);
    }
}
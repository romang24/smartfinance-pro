using SQLite;

namespace SmartFinancePro.Models;

public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public decimal Amount { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Note { get; set; } = string.Empty;

    [Ignore]
    public string Display => $"£{Amount:0.00} - {Category} - {Date:yyyy-MM-dd}";
}
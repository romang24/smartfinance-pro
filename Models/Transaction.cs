namespace SmartFinancePro.Models;

public class Transaction
{
    public int Id { get; set; }          // For SQLite later
    public decimal Amount { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Note { get; set; } = string.Empty;

    // Handy display text (temporary, for UI)
    public string Display => $"£{Amount:0.00} - {Category} - {Date:yyyy-MM-dd}";
}

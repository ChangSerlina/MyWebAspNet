using System.ComponentModel.DataAnnotations;

namespace MyWebAspNet.Models;

public class Bill : BaseEntity
{
    public int Id { get; set; }

    // FK
    public string UserId { get; set; } = "";
    public int PayAccountId { get; set; } = 0;
    public int BillCategoryId { get; set; } = 0;
    public int CurrencyId { get; set; } = 0;
    
    // Data
    [Required]
    public decimal Amount { get; set; } = 0;
    public string? Note { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    // Navigation
    public ApplicationUser? User { get; set; }
    public PayAccount? PayAccount { get; set; }
    public BillCategory? BillCategory { get; set; }
    public Currency? Currency { get; set; }
}
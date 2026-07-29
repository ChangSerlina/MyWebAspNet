using System.ComponentModel.DataAnnotations;

namespace MyWebAspNet.Models;

public class BillCategory : BaseEntity
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public TransactionType TransactionType { get; set; } = TransactionType.Unknown;
}

public enum TransactionType
{
    Unknown = 0,
    Income = 1,
    Expense = 2
}
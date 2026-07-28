using System.ComponentModel.DataAnnotations;

namespace MyWebAspNet.Models;

public class PayAccount : BaseEntity
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
}
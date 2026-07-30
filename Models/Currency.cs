using System.ComponentModel.DataAnnotations;

namespace MyWebAspNet.Models;

public class Currency : BaseEntity
{
    public int Id { get; set; }

    [Required]
    [StringLength(3)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Symbol { get; set; } = string.Empty;

    [Range(0, 4)]
    public byte DecimalPlaces { get; set; } = 2;

    public bool IsActive { get; set; } = true;
}
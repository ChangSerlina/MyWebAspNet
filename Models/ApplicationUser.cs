using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyWebAspNet.Models;

public class ApplicationUser : IdentityUser
{
    // 雙向關聯
    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
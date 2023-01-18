using System.ComponentModel.DataAnnotations;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities;

public class Client : BaseEntity
{
    [StringLength(256)]
    [Required]
    public string Name { get; set; }
    
    [StringLength(256)]
    [Required]
    public string Surname { get; set; }
    
    [StringLength(256)]
    [Required]
    public string Patronymic { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Phone]
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string[] CarNumber { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}

public enum Gender
{
    Male,
    Female
}

public enum DiscountSystem
{
    Percent,
    
}

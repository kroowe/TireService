using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.Client;

public class ClientUpdateInfo
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }
    
    [Required]
    public string Patronymic { get; set; }
    
    [Phone]
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string[] CarNumber { get; set; }
}

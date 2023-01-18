using System.ComponentModel.DataAnnotations;
using TireService.Dtos.Infos.Client;

namespace TireService.Dtos.Infos.Worker;

public class WorkerCreateInfo
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }
    
    [Required]
    public string Patronymic { get; set; }
    
    [Required]
    public GenderInfo? Gender { get; set; }
    
    [Phone]
    [Required]
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// Должность
    /// </summary>
    public string Position { get; set;}
}

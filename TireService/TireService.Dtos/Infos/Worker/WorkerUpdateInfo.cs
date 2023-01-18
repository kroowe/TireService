using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.Worker;

public class WorkerUpdateInfo
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
    
    /// <summary>
    /// Должность
    /// </summary>
    public string Position { get; set;}
}

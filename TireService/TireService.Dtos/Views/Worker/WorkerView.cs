using TireService.Dtos.Infos.Client;

namespace TireService.Dtos.Views.Worker;

public class WorkerView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public GenderInfo Gender { get; set; }
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// Должность
    /// </summary>
    public string Position { get; set;}
    public bool IsDismissed { get; set; }
}
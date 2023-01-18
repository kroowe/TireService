namespace TireService.Dtos.Infos.Worker;

public class GetAllWorkerByFilterInfo
{
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public bool? IsDismissed { get; set; }
    public IReadOnlyCollection<Guid> WorkerIds { get; set; }
    public IReadOnlyCollection<Guid> ExcludeWorkerIds { get; set; }
}
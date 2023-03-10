using TireService.Dtos.Infos.Client;

namespace TireService.Dtos.Views.Client;

public class ClientView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public GenderInfo Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string[] CarNumber { get; set; }
}
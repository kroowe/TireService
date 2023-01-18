namespace TireService.Dtos.Infos.Reports;

public class GetClientOrderHistoryReportInfo
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string PhoneNumber { get; set; }
    public string CarNumber { get; set; }
    public IReadOnlyCollection<Guid> ClientIds { get; set; }
}
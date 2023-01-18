using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.Reports;

public class GetSliceCatalogItemsReportInfo
{
    [Required]
    public DateTime? StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
}
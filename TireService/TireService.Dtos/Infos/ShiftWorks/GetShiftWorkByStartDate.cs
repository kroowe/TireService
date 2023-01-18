using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.ShiftWorks;

public class GetShiftWorkByStartDate
{
    [Required]
    public int? Year { get; set; }
    [Required]
    public int? Month { get; set; }
    [Required]
    public int? Day { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.ShiftWorks;

public class ShiftWorkCreateInfo
{
    [Required]
    public DateTime? CreatedDate { get; set; }
}
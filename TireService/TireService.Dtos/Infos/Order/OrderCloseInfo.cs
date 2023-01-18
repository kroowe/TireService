using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.Order;

public class OrderCloseInfo
{
    [Required]
    public Guid? OrderId { get; set; }
    [Required]
    public string CloseComment { get; set; }
}
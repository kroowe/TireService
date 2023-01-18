namespace TireService.Dtos.Views.ShiftWorkView;

public class ShiftWorkView
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? OpenedDate { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime? ClosedDate { get; set; }
    public bool IsOpen { get; set; }
}
namespace TireService.Dtos.Views.ShiftWorkView;

public class ShiftWorkWorkerView
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    
    /// <summary>
    /// Должность
    /// </summary>
    public string Position { get; set;}
}
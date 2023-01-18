namespace TireService.Dtos.Views.Reports;

public class IncomeStatementReportView
{
    public DateTime Day { get; set; }
    public decimal Expenses { get; set; }
    public decimal Revenue { get; set; }
    public int OrderCount { get; set; }
    
}
namespace DetectiveAgencyProject.Models;

public class Report
{
    public int ReportId { get; set; }
    public DateTime CreationDate { get; set; }
    public string Details { get; set; }
    public Detective? Detective { get; set; }
    public Case? Case { get; set; }
        
    // Ідентифікатори зовнішніх ключів
    public int DetectiveId { get; set; }
    public int CaseId { get; set; }
}
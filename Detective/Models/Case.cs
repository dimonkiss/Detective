namespace DetectiveAgencyProject.Models;

public class Case
{
    public int CaseId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    
    public int DetectiveId { get; set; }
    public int ClientId { get; set; }
    public Detective? Detective { get; set; }
    public Client? Client { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Report>? Reports { get; set; }
}
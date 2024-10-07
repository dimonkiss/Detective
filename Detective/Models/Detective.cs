namespace DetectiveAgencyProject.Models;

public class Detective
{
    public int DetectiveId { get; set; }
    public string Name { get; set; }
    public string Experience { get; set; }
    public string Specialization { get; set; }
    
    public int AgencyId { get; set; }
    public ICollection<Case>? Cases { get; set; }
    public ICollection<Order>? Orders { get; set; }
    
    public ICollection<Report>? Reports { get; set; }
}
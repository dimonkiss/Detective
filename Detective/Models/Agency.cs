namespace DetectiveAgencyProject.Models;

public class Agency
{
    public int AgencyId { get; set; }
    public string Name { get; set; }
    
    public ICollection<Detective>? Detectives { get; set; }
    public ICollection<Client>? Clients { get; set; }
}
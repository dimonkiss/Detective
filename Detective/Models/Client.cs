using Microsoft.AspNetCore.Identity;

namespace DetectiveAgencyProject.Models;

public class Client 
{
    public int ClientId { get; set; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public string RequestType { get; set; }
    
    public int AgencyId { get; set; }
    public ICollection<Case>? Cases { get; set; }
}
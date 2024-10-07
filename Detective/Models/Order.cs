namespace DetectiveAgencyProject.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Progress { get; set; }
        
        // Навігаційні властивості
        public Detective? Detective { get; set; }
        public Case? Case { get; set; }
        
        // Ідентифікатори зовнішніх ключів
        public int DetectiveId { get; set; }
        public int CaseId { get; set; }
    }
}
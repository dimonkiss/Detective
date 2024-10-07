using Microsoft.EntityFrameworkCore;

namespace DetectiveAgencyProject.Models
{
    public class DetectiveAgencyDbContext : DbContext
    {
        public DetectiveAgencyDbContext(DbContextOptions<DetectiveAgencyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Detective> Detectives { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Agency-Detective relationship
            modelBuilder.Entity<Detective>()
                .HasOne<Agency>()
                .WithMany(a => a.Detectives)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Agency-Client relationship
            modelBuilder.Entity<Client>()
                .HasOne<Agency>()
                .WithMany(a => a.Clients)
                .HasForeignKey(c => c.AgencyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Case-Detective relationship
            modelBuilder.Entity<Case>()
                .HasOne<Detective>()
                .WithMany(d => d.Cases)
                .HasForeignKey(c => c.DetectiveId)
                .OnDelete(DeleteBehavior.NoAction);

            // Case-Client relationship
            modelBuilder.Entity<Case>()
                .HasOne<Client>()
                .WithMany(cl => cl.Cases)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Order-Detective relationship
            modelBuilder.Entity<Order>()
                .HasOne<Detective>()
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DetectiveId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order-Case relationship
            modelBuilder.Entity<Order>()
                .HasOne<Case>()
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Report-Detective relationship
            modelBuilder.Entity<Report>()
                .HasOne<Detective>()
                .WithMany(d => d.Reports)
                .HasForeignKey(r => r.DetectiveId)
                .OnDelete(DeleteBehavior.Cascade);

            // Report-Case relationship
            modelBuilder.Entity<Report>()
                .HasOne<Case>()
                .WithMany(c => c.Reports)
                .HasForeignKey(r => r.CaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Kugel.StaffSearch.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kugel.StaffSearch.Database.SqlServer;

public class KugelStaffSearchContext : DbContext
{
    public KugelStaffSearchContext(DbContextOptions<KugelStaffSearchContext> options) : base(options)
    {
    }

    public DbSet<StaffMember>? StaffMember { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StaffMember>().HasData(new StaffMember
            {
                Id = Guid.NewGuid(),
                FirstName = "Petra",
                LastName = "Bauknecht",
                PersonalId = "2023-01-01-01"
            },
            new StaffMember
            {
                Id = Guid.NewGuid(),
                FirstName = "Philipp",
                LastName = "Bauknecht",
                PersonalId = "2023-01-01-02"
            },
            new StaffMember
            {
                Id = Guid.NewGuid(),
                FirstName = "Janek",
                LastName = "Fellien",
                PersonalId = "2023-01-01-03"
            });
    }
}
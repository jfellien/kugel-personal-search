using Kugel.StaffSearch.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kugel.StaffSearch.Database.SqlServer;

public class KugelStaffSearchContext : DbContext
{
    public KugelStaffSearchContext(DbContextOptions<KugelStaffSearchContext> options) : base(options) { }
    
    public DbSet<StaffMember>? StaffMember { get; set; }
}
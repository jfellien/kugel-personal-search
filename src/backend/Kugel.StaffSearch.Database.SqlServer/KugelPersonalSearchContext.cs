using Kugel.StaffSearch.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kugel.StaffSearch.Database.SqlServer;

public class KugelPersonalSearchContext : DbContext
{
    public KugelPersonalSearchContext(DbContextOptions<KugelPersonalSearchContext> options) : base(options) { }
    
    public DbSet<StaffMember>? StaffMember { get; set; }
}
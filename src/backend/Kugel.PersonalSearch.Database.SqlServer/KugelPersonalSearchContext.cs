using Kugel.PersonalSearch.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kugel.PersonalSearch.Database.SqlServer;

public class KugelPersonalSearchContext : DbContext
{
    public DbSet<Person>? People { get; set; } = null;
}
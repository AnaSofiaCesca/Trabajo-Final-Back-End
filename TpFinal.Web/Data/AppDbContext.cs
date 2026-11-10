using Microsoft.EntityFrameworkCore;

namespace TpFinal.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSets: se agregan cuando definan el tema
}

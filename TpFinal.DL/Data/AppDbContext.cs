using Microsoft.EntityFrameworkCore;

namespace TpFinal.DL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

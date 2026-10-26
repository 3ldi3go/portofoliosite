using Microsoft.EntityFrameworkCore;

class portofolioDB : DbContext
{
    public portofolioDB(DbContextOptions<portofolioDB> options) : base(options)
    {
    }

    public DbSet<Projecten> Projecten => Set<Projecten>();
    public DbSet<Posts> Posts => Set<Posts>();
    }
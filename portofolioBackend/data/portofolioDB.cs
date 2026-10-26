using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

class portofolioDB : IdentityDbContext<User>
{
    public portofolioDB(DbContextOptions<portofolioDB> options) : base(options)
    {
    }

    public DbSet<Projecten> Projecten => Set<Projecten>();
    public DbSet<Posts> Posts => Set<Posts>();
    }
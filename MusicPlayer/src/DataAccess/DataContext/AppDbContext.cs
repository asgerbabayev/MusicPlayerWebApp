using Data.Entities;
using Data.Entities.Base;
using Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataContext;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    private readonly IHttpContextAccessor _accessor;
    public AppDbContext(DbContextOptions<AppDbContext> options,
        IHttpContextAccessor accessor) : base(options)
    {
        _accessor = accessor;
    }
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<Artist> Artists => Set<Artist>();


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var list = ChangeTracker.Entries<BaseAuditableEntity>().ToList();
        foreach (var item in list)
        {
            if (item.State == EntityState.Added)
            {
                item.Entity.CreatedBy = _accessor.HttpContext.User.Identity.Name;
                item.Entity.CreatedDate = DateTime.UtcNow;
            }
            else if (item.State == EntityState.Modified)
            {
                item.Entity.UpdatedBy = _accessor.HttpContext.User.Identity.Name;
                item.Entity.UpdatedDate = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

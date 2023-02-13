using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataContext;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	public DbSet<Song> Songs => Set<Song>();
	public DbSet<Artist> Artists => Set<Artist>();
}

using Microsoft.EntityFrameworkCore;
using StaySphere.API.Models;

namespace StaySphere.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Homestay> Homestays { get; set; }
    public DbSet<User> Users { get; set; }
}
using Microsoft.EntityFrameworkCore;
using WebEntrevistaApi.Models;

namespace WebEntrevistaApi.Context;

public class EntrevistaDbContext : DbContext
{
    public EntrevistaDbContext(DbContextOptions<EntrevistaDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Empleado> Empleados { get; set; }
}
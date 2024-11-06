using DispatchService.Model.Entities;
using Microsoft.EntityFrameworkCore;


namespace DispatchService.Model.Context;

public class DispatchServiceDbContext(DbContextOptions<DispatchServiceDbContext> options) : DbContext(options)
{
    public DbSet<Driver> Driver { get; set; }
    public DbSet<Transport> Transport { get; set; }
    public DbSet<Route> Route { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasOne(r => r.AssignedTransport)
                .WithMany()
                .HasForeignKey("assigned_transport")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.AssignedDriver)
                .WithMany()
                .HasForeignKey("assigned_driver")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}


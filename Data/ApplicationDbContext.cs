using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebAspNet.Models;

namespace MyWebAspNet.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public override int SaveChanges()
    {
        SetTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Code)
            .IsUnique();
    }

    private void SetTimestamps()
    {
        var now = DateTime.UtcNow;  // 使用 UTC 時間，避免時區問題，要顯示本地時間可以在前端轉換
        var entries = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
            }
            else
            {
                // 避免建立時間被修改到
                entry.Property(x => x.CreatedAt).IsModified = false;
            }

            entry.Entity.UpdatedAt = now;
        }
    }

    public DbSet<PayAccount> PayAccount { get; set; } = default!;
    public DbSet<BillCategory> BillCategory { get; set; } = default!;
    public DbSet<Currency> Currency { get; set; } = default!;
}

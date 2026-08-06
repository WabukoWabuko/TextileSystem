using Microsoft.EntityFrameworkCore;
using TextileWarehouseERP.Models;

namespace TextileWarehouseERP.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<InboundTransaction> InboundTransactions => Set<InboundTransaction>();
    public DbSet<OutboundTransaction> OutboundTransactions => Set<OutboundTransaction>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Setting> Settings => Set<Setting>();
    public DbSet<LookupValue> LookupValues => Set<LookupValue>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Unique indexes
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Item>()
            .HasIndex(i => i.ItemCode)
            .IsUnique();

        modelBuilder.Entity<InboundTransaction>()
            .HasIndex(t => t.TransactionNo)
            .IsUnique();

        modelBuilder.Entity<OutboundTransaction>()
            .HasIndex(t => t.TransactionNo)
            .IsUnique();

        modelBuilder.Entity<Setting>()
            .HasIndex(s => s.Key)
            .IsUnique();

        // Decimal precision
        modelBuilder.Entity<Item>()
            .Property(i => i.CurrentStock)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Item>()
            .Property(i => i.ReorderLevel)
            .HasPrecision(18, 2);

        modelBuilder.Entity<InboundTransaction>()
            .Property(t => t.QuantityReceived)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OutboundTransaction>()
            .Property(t => t.QuantityIssued)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OutboundTransaction>()
            .Property(t => t.RemainingStock)
            .HasPrecision(18, 2);
    }
}
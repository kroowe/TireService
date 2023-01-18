using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure.Entities;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Infrastructure;

public sealed class PostgresContext : DbContext
{
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PaymentRule> PaymentRules { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<WorkerBalance> WorkerBalances { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<WarehouseNomenclature> WarehouseNomenclatures { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<TaskOrder> TaskOrders { get; set; }
    public DbSet<ShiftWork> ShiftWorks { get; set; }
    public DbSet<ShiftWorkWorker> ShiftWorkWorkers { get; set; }
    public DbSet<AppSettingConstant> AppSettingConstants { get; set; }
    public DbSet<SalaryPaymentsToWorker> SalaryPaymentsToWorkers { get; set; }
    
    
    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {
        modelBuilder.HasPostgresExtension("ltree");
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppSettingConstant>().HasKey(x => x.Key);
        modelBuilder
            .Entity<ShiftWork>()
            .HasMany(x => x.Workers)
            .WithMany(x => x.ShiftWorks)
            .UsingEntity<ShiftWorkWorker>(
                entityTypeBuilder => entityTypeBuilder.HasOne(shiftWorkWorker => shiftWorkWorker.Worker)
                    .WithMany(worker => worker.ShiftWorkWorkers)
                    .HasForeignKey(shiftWorkWorker => shiftWorkWorker.WorkerId),
                entityTypeBuilder => entityTypeBuilder.HasOne(shiftWorkWorker => shiftWorkWorker.ShiftWork)
                    .WithMany(shiftWork => shiftWork.ShiftWorkWorkers)
                    .HasForeignKey(shiftWorkWorker => shiftWorkWorker.ShiftWorkId),
                entityTypeBuilder => entityTypeBuilder.HasIndex(x => new { x.WorkerId, x.ShiftWorkId }).IsUnique()
            );

        //modelBuilder.Entity<Category>().HasQueryFilter(x => x.IsDeleted == false);
        //modelBuilder.Entity<CatalogItem>().HasQueryFilter(x => x.IsDeleted == false);
    }
}
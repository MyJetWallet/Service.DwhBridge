using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Service.DwhBridge.Database.Models;
using Service.HighYieldEngine.Domain.Models.Dtos;
using Service.HighYieldEngine.Domain.Models.NoSql;

namespace Service.DwhBridge.Database;

public class DwhContext : DbContext
{
    public const string Schema = "bridge";
    private const string EarnDashboardAssetTableName = "EarnDashboardAssets";
    private const string EarnDashboardTotalTableName = "EarnDashboardTotal";
    private const string EarnDashboardAssetByDayTableName = "EarnDashboardAssetsByDay";
    private const string EarnDashboardTotalByDayTableName = "EarnDashboardTotalByDay";
    
    public static ILoggerFactory LoggerFactory { get; set; }

    public DbSet<EarnDashboardAssetEntity> AssetEntities { get; set; }
    public DbSet<EarnDashboardAssetEntityByDay> AssetEntitiesByDay { get; set; }
    public DbSet<EarnDashboardTotalEntity> TotalEntities { get; set; }
    public DbSet<EarnDashboardTotalEntityByDay> TotalEntitiesByDay { get; set; }

    public DwhContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (LoggerFactory != null)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory).EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.Entity<EarnDashboardAssetEntity>().ToTable(EarnDashboardAssetTableName);
        modelBuilder.Entity<EarnDashboardAssetEntity>().HasKey(e=>e.Id);
        modelBuilder.Entity<EarnDashboardAssetEntity>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<EarnDashboardAssetEntity>().OwnsOne(e => e.ClientInfo);
        modelBuilder.Entity<EarnDashboardAssetEntity>().OwnsOne(e => e.NetInfo);
        modelBuilder.Entity<EarnDashboardAssetEntity>().OwnsOne(e => e.SimpleInfo);

        modelBuilder.Entity<EarnDashboardTotalEntity>().ToTable(EarnDashboardTotalTableName);
        modelBuilder.Entity<EarnDashboardTotalEntity>().HasKey(e=>e.Id);
        modelBuilder.Entity<EarnDashboardTotalEntity>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<EarnDashboardTotalEntity>().OwnsOne(e => e.ClientInfo);
        modelBuilder.Entity<EarnDashboardTotalEntity>().OwnsOne(e => e.NetInfo);
        modelBuilder.Entity<EarnDashboardTotalEntity>().OwnsOne(e => e.SimpleInfo);

        modelBuilder.Entity<EarnDashboardAssetEntityByDay>().ToTable(EarnDashboardAssetByDayTableName);
        modelBuilder.Entity<EarnDashboardAssetEntityByDay>().Property(e => e.TimeStamp).HasColumnType("datetime");
        modelBuilder.Entity<EarnDashboardAssetEntityByDay>().HasKey(e => new {e.TimeStamp, e.AssetSymbol});
        modelBuilder.Entity<EarnDashboardAssetEntityByDay>().OwnsOne(e => e.ClientInfo);
        modelBuilder.Entity<EarnDashboardAssetEntityByDay>().OwnsOne(e => e.NetInfo);
        modelBuilder.Entity<EarnDashboardAssetEntityByDay>().OwnsOne(e => e.SimpleInfo);

        modelBuilder.Entity<EarnDashboardTotalEntityByDay>().ToTable(EarnDashboardTotalByDayTableName);
        modelBuilder.Entity<EarnDashboardTotalEntityByDay>().Property(e => e.TimeStamp).HasColumnType("datetime");
        modelBuilder.Entity<EarnDashboardTotalEntityByDay>().HasKey(e => e.TimeStamp);
        modelBuilder.Entity<EarnDashboardTotalEntityByDay>().OwnsOne(e => e.ClientInfo);
        modelBuilder.Entity<EarnDashboardTotalEntityByDay>().OwnsOne(e => e.NetInfo);
        modelBuilder.Entity<EarnDashboardTotalEntityByDay>().OwnsOne(e => e.SimpleInfo);
    }
    
}
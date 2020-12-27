using HaloBiz.Model_;
using Microsoft.EntityFrameworkCore;

namespace HaloBiz.Data
{
    public class DataContext : DbContext
    {

    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    public DbSet<State> States { get; set; }
    public DbSet<LGA> LGAs { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Division> Divisions { get; set; }
    public DbSet<OperatingEntity> OperatingEntities { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<ServiceGroup> ServiceGroups { get; set; }
    public DbSet<Services> Services { get; set; }
    public DbSet<StrategicBusinessUnit> StrategicBusinessUnits { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<ModificationHistory> ModificationHistories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<State>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<State>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<LGA>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<LGA>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<Office>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<Office>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<Branch>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<Branch>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<Division>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<Division>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<OperatingEntity>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<OperatingEntity>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<ServiceCategory>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<ServiceCategory>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
                
            builder.Entity<ServiceGroup>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<ServiceGroup>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<Services>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<Services>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<StrategicBusinessUnit>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<StrategicBusinessUnit>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<UserProfile>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<UserProfile>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<ModificationHistory>()
               .Property(p => p.UpdatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<ModificationHistory>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
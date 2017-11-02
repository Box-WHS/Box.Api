using Box.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Box.Api.Data.DataContexts
{
    public class BoxApiDataContext : DbContext
    {
        public BoxApiDataContext(IConfiguration configuration, DbContextOptions<BoxApiDataContext> options) : base(
            options)
        {
            Configuration = configuration;
        }

        // ReSharper disable once UnassignedGetOnlyAutoProperty // Why?
        public DbSet<Core.Data.Box> Boxes { get; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Tray> Trays { get; set; }

        public DbSet<User> Users { get; set; }

        private IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(u => u.Id);
            builder.Entity<User>()
                .Property( u => u.Username )
                .IsRequired();
            builder.Entity<User>()
                .Property( u => u.Email )
                .IsRequired();

            builder.Entity<Core.Data.Box>()
                .HasKey(b => b.Id);
            builder.Entity<Core.Data.Box>()
                .HasIndex(b => b.Id);
            builder.Entity<Core.Data.Box>()
                .HasMany(b => b.Trays);
            builder.Entity<Core.Data.Box>()
                .Property( b => b.ConcurrencyToken )
                .IsConcurrencyToken();
            builder.Entity<Core.Data.Box>()
                .HasOne( b => b.User )
                .WithMany( b => b.Boxes )
                .OnDelete( DeleteBehavior.Cascade );

            builder.Entity<Tray>()
                .HasKey(t => t.Id);
            builder.Entity<Tray>()
                .HasIndex(t => t.Id);
            builder.Entity<Tray>()
                .HasOne(t => t.Box)
                .WithMany(b => b.Trays)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Tray>()
                .HasMany(t => t.Cards);

            builder.Entity<Card>()
                .HasKey(c => c.Id);
            builder.Entity<Card>()
                .HasIndex(c => c.Id);
            builder.Entity<Card>()
                .HasOne(c => c.Tray)
                .WithMany(t => t.Cards)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Configuration.GetConnectionString("BoxApiDatabase"));
        }


        /// <summary>
        ///     Saves all changes made in this context to the database.
        /// </summary>
        /// <remarks>
        ///     This method will automatically call
        ///     <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///     changes to entity instances before saving to the underlying database. This can be disabled via
        ///     <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </remarks>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
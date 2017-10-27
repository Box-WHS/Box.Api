using Box.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Box.Api.Data.DataContexts
{
    public class BoxApiDataContext : DbContext
    {
        public DbSet<Core.Data.Box> Boxes { get; }

        public DbSet<Card> Cards { get; set; }
        
        public DbSet<Tray> Trays { get; set; }
        
        public BoxApiDataContext( IConfiguration configuration, DbContextOptions<BoxApiDataContext> options ) : base( options )
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseMySql( Configuration.GetConnectionString( "BoxApiDatabase" ) );
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
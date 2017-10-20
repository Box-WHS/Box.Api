using System;
using System.Linq;
using Box.Api.Types;
using Microsoft.EntityFrameworkCore;

namespace Box.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        
        public DbSet<Types.Box> Boxes { get; set; }
        public DbSet<Fold> Folds { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Parametrize  User-table
            builder.Entity<Types.Box>()
                .HasKey(box => box.IdBox);
            builder.Entity<Types.Box>()
                .HasMany(box => box.Folds);
            
            //Parametrize Folds-Table
            builder.Entity<Fold>()
                .HasKey(fold => fold.IdFold);
            builder.Entity<Fold>()
                .HasOne(fold => fold.Box);
            builder.Entity<Fold>()
                .HasMany(fold => fold.Cards);
            
            //Parametrize Cards-Table
            builder.Entity<Card>()
                .HasKey(card => card.IdCard);
            builder.Entity<Card>()
                .HasOne(card => card.Fold);
        }
        
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            return base.SaveChanges();
        }
    }
}
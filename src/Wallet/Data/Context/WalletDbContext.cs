using System.Data.Entity;
using WalletService.Business.Entities;


namespace WalletService.Data.Context
{
    public class WalletDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }

        public WalletDbContext() : base("name=WalletDb")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<WalletDbContext, Api.Migrations.Configuration>());

            //if (!Database.Exists())
            //{
            //    Database.Initialize(true);
            //}
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasKey(w => w.Id);
            modelBuilder.Entity<Wallet>().Property(w => w.CustomerId).IsRequired();
            modelBuilder.Entity<Wallet>().Property(w => w.Currency).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Wallet>().Property(w => w.Balance).IsRequired().HasPrecision(18, 2);

            modelBuilder.Entity<WalletTransaction>().HasKey(t => t.Id);
            modelBuilder.Entity<WalletTransaction>().Property(t => t.Amount).IsRequired().HasPrecision(18, 2);
            modelBuilder.Entity<WalletTransaction>().Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar");

            base.OnModelCreating(modelBuilder);
        }
    }
}
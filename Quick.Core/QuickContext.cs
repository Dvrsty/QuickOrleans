using Quick.Interface;
using Quick.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Quick.Core
{
    public class QuickContext : DbContext, IQuickContext
    {
        public QuickContext()
        {
            
        }

        public QuickContext(DbContextOptions<QuickContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            return base.SaveChanges(true);
        }

        public virtual DbSet<HelloModel> HelloModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HelloModel>().ToTable("Hello").HasKey(s => s.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!!!options.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot config = builder.Build();
                options.UseMySql(config.GetConnectionString("DbConnection"));
            }
        }
    }
}

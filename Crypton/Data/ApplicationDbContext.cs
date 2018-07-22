using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Crypton.Models;
using Crypton.Models.PriceViewModels;
using Crypton.Models.CurrencyViewModels;
using Crypton.Models.AlertViewModels;

namespace Crypton.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }

        public DbSet<Crypton.Models.CurrencyViewModels.Provider> Provider { get; set; }

        public DbSet<Crypton.Models.PriceViewModels.Variation> Variation { get; set; }

        public DbSet<Crypton.Models.AlertViewModels.AlertType> AlertType { get; set; }

        public DbSet<Crypton.Models.CurrencyViewModels.Currency> Currency { get; set; }

        public DbSet<Crypton.Models.PriceViewModels.CurrencyPrice> CurrencyPrice { get; set; }

        public DbSet<Crypton.Models.PriceViewModels.ConversionRate> ConversionRate { get; set; }

        public DbSet<Crypton.Models.AlertViewModels.Log> Log { get; set; }

        public DbSet<Crypton.Models.AlertViewModels.Alert> Alert { get; set; }

    }
}

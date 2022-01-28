using System;
using System.Linq;
using System.Reflection;
using eCore.Entities;
using eCore.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using eCore.Models;
using eCore.Services.Models;

namespace eCore.Context
{
    public partial class SecureOMDb : DbContext
    {
        public IConfiguration Configuration { get; }

        public SecureOMDb(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public SecureOMDb(DbContextOptions<SecureOMDb> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }


        public virtual DbSet<ApicoreCompraSeguro> ApicoreCompraSeguro { get; set; }
        public virtual DbSet<WsBrokerCompania> WsBrokerCompania { get; set; }
        public virtual DbSet<WsBrokerFamilia> WsBrokerFamilia { get; set; }
        public virtual DbSet<WsBrokerCiaCoberturaDetalle> WsBrokerCiaCoberturaDetalle { get; set; }
        public virtual DbSet<WsBrokerCiaFamilia> WsBrokerCiaFamilia { get; set; }
        public virtual DbSet<ReporteCobertura> ReporteCobertura { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SecureOMConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");


            modelBuilder.ApplyConfiguration(new ApicoreCompraSeguroConfiguration());
            modelBuilder.ApplyConfiguration(new WsBrokerCompaniaConfiguration());
            modelBuilder.ApplyConfiguration(new WsBrokerFamiliaConfiguration());
            modelBuilder.ApplyConfiguration(new WsBrokerCiaCoberturaDetalleConfiguration());
            modelBuilder.ApplyConfiguration(new WsBrokerCiaFamiliaConfiguration());
            modelBuilder.ApplyConfiguration(new ReporteCoberturaConfiguration());

        }
    }
}

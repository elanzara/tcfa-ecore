using System;
using System.Linq;
using System.Reflection;
using eCore.Entities;
using eCore.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using eCore.Models;

namespace eCore.Context
{
    public partial class SecureDbContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public SecureDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public SecureDbContext(DbContextOptions<SecureDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }


        public virtual DbSet<AllianzCarteraEnc> AllianzCarteraEnc { get; set; }
        public virtual DbSet<AllianzCarteraDet> AllianzCarteraDet { get; set; }
        public virtual DbSet<AllianzComisionesEnc> AllianzComisionesEnc { get; set; }
        public virtual DbSet<AllianzComisionesDet> AllianzComisionesDet { get; set; }
        public virtual DbSet<TrfNovedades> TrfNovedades { get; set; }
        public virtual DbSet<TrfDetallePremio> TrfDetallePremio { get; set; }
        public virtual DbSet<TrfRama> TrfRama { get; set; }
        public virtual DbSet<TrfSdtcomision> TrfSdtcomision { get; set; }
        public virtual DbSet<TrfSdtcuota> TrfSdtcuota { get; set; }
        public virtual DbSet<TrfVehiculoDatos> TrfVehiculoDatos { get; set; }
        public virtual DbSet<CajaCarteraEnc> CajaCarteraEnc { get; set; }
        public virtual DbSet<CajaCarteraDet> CajaCarteraDet { get; set; }
        public virtual DbSet<CajaCarteraAccesorio> CajaCarteraAccesorio { get; set; }
        public virtual DbSet<CajaCarteraAuto> CajaCarteraAuto { get; set; }
        public virtual DbSet<CajaCarteraCliente> CajaCarteraCliente { get; set; }
        public virtual DbSet<CajaCarteraCuota> CajaCarteraCuota { get; set; }
        public virtual DbSet<CajaCarteraCuotas> CajaCarteraCuotas { get; set; }
        public virtual DbSet<CajaCarteraDomicilio> CajaCarteraDomicilio { get; set; }
        public virtual DbSet<CajaCarteraDomicilioCorresp> CajaCarteraDomicilioCorresp { get; set; }

        public virtual DbSet<MapNovedades> MapNovedades { get; set; }
        public virtual DbSet<MapCuotas> MapCuotas { get; set; }
        public virtual DbSet<MapCoberturas> MapCoberturas { get; set; }
        public virtual DbSet<MapDatosVariables> MapDatosVariables { get; set; }
        public virtual DbSet<MapDatosRiesgo> MapDatosRiesgo { get; set; }
        public virtual DbSet<MapAsegurados> MapAsegurados { get; set; }
        public virtual DbSet<MapImpuestos> MapImpuestos { get; set; }

        public virtual DbSet<ScriMovimientos> ScriMovimientos { get; set; }
        public virtual DbSet<ScriListJobSummary> ScriListJobSummary { get; set; }
        public virtual DbSet<ScriOffering> ScriOffering { get; set; }
        public virtual DbSet<ScriPolicyType> ScriPolicyType { get; set; }
        public virtual DbSet<ScriProduct> ScriProduct { get; set; }
        public virtual DbSet<ScriMessages> ScriMessages { get; set; }
        public virtual DbSet<ScriCodigoProductor> ScriCodigoProductor { get; set; }
        public virtual DbSet<ScriTaxId> ScriTaxId { get; set; }
        public virtual DbSet<ScriMovimientosCobranzas> ScriMovimientosCobranzas { get; set; }
        public virtual DbSet<SomDetalleCoberturas> SomDetalleCoberturas { get; set; }
        public virtual DbSet<MapCodigoConexion> MapCodigoConexion { get; set; }
        public virtual DbSet<CodigoProductor> CodigoProductor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SecureConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");


            modelBuilder.ApplyConfiguration(new AllianzCarteraEncConfiguration());
            modelBuilder.ApplyConfiguration(new AllianzCarteraDetConfiguration());
            modelBuilder.ApplyConfiguration(new AllianzComisionesEncConfiguration());
            modelBuilder.ApplyConfiguration(new AllianzComisionesDetConfiguration());
            modelBuilder.ApplyConfiguration(new TrfNovedadesConfiguration());
            modelBuilder.ApplyConfiguration(new TrfDetallePremioConfiguration());
            modelBuilder.ApplyConfiguration(new TrfRamaConfiguration());
            modelBuilder.ApplyConfiguration(new TrfSdtcomisionConfiguration());
            modelBuilder.ApplyConfiguration(new TrfSdtcuotaConfiguration());
            modelBuilder.ApplyConfiguration(new TrfVehiculoDatosConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraEncConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraDetConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraAccesorioConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraAutoConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraClienteConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraCuotaConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraCuotasConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraDomicilioConfiguration());
            modelBuilder.ApplyConfiguration(new CajaCarteraDomicilioCorrespConfiguration());

            modelBuilder.ApplyConfiguration(new MapNovedadesConfiguration());
            modelBuilder.ApplyConfiguration(new MapCuotasConfiguration());
            modelBuilder.ApplyConfiguration(new MapCoberturasConfiguration());
            modelBuilder.ApplyConfiguration(new MapDatosVariablesConfiguration());
            modelBuilder.ApplyConfiguration(new MapDatosRiesgoConfiguration());
            modelBuilder.ApplyConfiguration(new MapAseguradosConfiguration());
            modelBuilder.ApplyConfiguration(new MapImpuestosConfiguration());

            modelBuilder.ApplyConfiguration(new ScriMovimientosConfiguration());
            modelBuilder.ApplyConfiguration(new ScriListJobSummaryConfiguration());
            modelBuilder.ApplyConfiguration(new ScriOfferingConfiguration());
            modelBuilder.ApplyConfiguration(new ScriPolicyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriProductConfiguration());
            modelBuilder.ApplyConfiguration(new ScriMessagesConfiguration());
            modelBuilder.ApplyConfiguration(new ScriCodigoProductorConfiguration());

            modelBuilder.ApplyConfiguration(new ScriMovimientosCobranzasConfiguration());
            modelBuilder.ApplyConfiguration(new SomDetalleCoberturasConfiguration());
            modelBuilder.ApplyConfiguration(new MapCodigoConexionConfiguration());
            modelBuilder.ApplyConfiguration(new CodigoProductorConfiguration());

        }
    }
}

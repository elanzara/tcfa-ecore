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
        public virtual DbSet<ScriPolizab2b> ScriPolizab2b { get; set; }
        public virtual DbSet<ScriPolicy> ScriPolicy { get; set; }
        public virtual DbSet<ScriAffinityGroupSub> ScriAffinityGroupSub { get; set; }
        public virtual DbSet<ScriAffinityGroupType> ScriAffinityGroupType { get; set; }
        public virtual DbSet<ScriSubtype> ScriSubtype { get; set; }
        public virtual DbSet<ScriContact> ScriContact { get; set; }
        public virtual DbSet<ScriAddress> ScriAddress { get; set; }
        public virtual DbSet<ScriAddressType> ScriAddressType { get; set; }
        public virtual DbSet<ScriAddressCountry> ScriAddressCountry { get; set; }
        public virtual DbSet<ScriState> ScriState { get; set; }
        public virtual DbSet<ScriPhone> ScriPhone { get; set; }
        public virtual DbSet<ScriPhoneType> ScriPhoneType { get; set; }
        public virtual DbSet<ScriContactType> ScriContactType { get; set; }
        public virtual DbSet<ScriGender> ScriGender { get; set; }
        public virtual DbSet<ScriMaritalStatus> ScriMaritalStatus { get; set; }
        public virtual DbSet<ScriNationality> ScriNationality { get; set; }
        public virtual DbSet<ScriOccupation> ScriOccupation { get; set; }
        public virtual DbSet<ScriOfficialIDType> ScriOfficialIDType { get; set; }
        public virtual DbSet<ScriPreferredSettlementCurrency> ScriPreferredSettlementCurrency { get; set; }
        public virtual DbSet<ScriSchoolLevel> ScriSchoolLevel { get; set; }
        public virtual DbSet<ScriCountry> ScriCountry { get; set; }
        public virtual DbSet<ScriMaillingAddress> ScriMaillingAddress { get; set; }
        public virtual DbSet<ScriTaxStatus> ScriTaxStatus { get; set; }
        public virtual DbSet<ScriEnrollementStatus> ScriEnrollementStatus { get; set; }
        public virtual DbSet<ScriRetentionAgent> ScriRetentionAgent { get; set; }
        public virtual DbSet<ScriMaillingAddressAddressType> ScriMaillingAddressAddressType { get; set; }
        public virtual DbSet<ScriMaillingAddressCountry> ScriMaillingAddressCountry { get; set; }
        public virtual DbSet<ScriMaillingAddressState> ScriMaillingAddressState { get; set; }
        public virtual DbSet<ScriExt_RamoSSN> ScriExt_RamoSSN { get; set; }
        public virtual DbSet<ScriExt_PolicyType> ScriExt_PolicyType { get; set; }
        public virtual DbSet<ScriProducerCode> ScriProducerCode { get; set; }
        public virtual DbSet<ScriPaymentMethod> ScriPaymentMethod { get; set; }
        public virtual DbSet<ScriPolicyTerm> ScriPolicyTerm { get; set; }
        public virtual DbSet<ScriCurrency> ScriCurrency { get; set; }
        public virtual DbSet<ScriProducerAgent> ScriProducerAgent { get; set; }
        public virtual DbSet<ScriProducerOfService> ScriProducerOfService { get; set; }
        public virtual DbSet<ScriServiceOrganizer> ScriServiceOrganizer { get; set; }
        public virtual DbSet<ScriChannelEntry> ScriChannelEntry { get; set; }
        public virtual DbSet<ScriStatus> ScriStatus { get; set; }
        public virtual DbSet<ScriVehicle> ScriVehicle { get; set; }
        public virtual DbSet<ScriAutomaticAdjust> ScriAutomaticAdjust { get; set; }
        public virtual DbSet<ScriCategory> ScriCategory { get; set; }
        public virtual DbSet<ScriColor> ScriColor { get; set; }
        public virtual DbSet<ScriFuelType> ScriFuelType { get; set; }
        public virtual DbSet<ScriJurisdiction> ScriJurisdiction { get; set; }
        public virtual DbSet<ScriOriginCountry> ScriOriginCountry { get; set; }
        public virtual DbSet<ScriProductOffering> ScriProductOffering { get; set; }
        public virtual DbSet<ScriRiskLocation> ScriRiskLocation { get; set; }
        public virtual DbSet<ScriUsage> ScriUsage { get; set; }
        public virtual DbSet<ScriReasonCancelDTO> ScriReasonCancelDTO { get; set; }
        

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
            modelBuilder.ApplyConfiguration(new ScriPolizab2bConfiguration());
            modelBuilder.ApplyConfiguration(new ScriPolicyConfiguration());
            modelBuilder.ApplyConfiguration(new ScriAffinityGroupSubConfiguration());
            modelBuilder.ApplyConfiguration(new ScriAffinityGroupTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriSubtypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriContactConfiguration());
            modelBuilder.ApplyConfiguration(new ScriAddressConfiguration());
            modelBuilder.ApplyConfiguration(new ScriAddressTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriAddressCountryConfiguration());
            modelBuilder.ApplyConfiguration(new ScriStateConfiguration());
            modelBuilder.ApplyConfiguration(new ScriPhoneConfiguration());
            modelBuilder.ApplyConfiguration(new ScriPhoneTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriContactTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriGenderConfiguration());
            modelBuilder.ApplyConfiguration(new ScriMaritalStatusConfiguration());
            modelBuilder.ApplyConfiguration(new ScriNationalityConfiguration());
            modelBuilder.ApplyConfiguration(new ScriOccupationConfiguration());
            modelBuilder.ApplyConfiguration(new ScriOfficialIDTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriPreferredSettlementCurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new ScriSchoolLevelConfiguration());
            modelBuilder.ApplyConfiguration(new ScriCountryConfiguration());
            modelBuilder.ApplyConfiguration(new ScriMaillingAddressConfiguration());
            modelBuilder.ApplyConfiguration(new ScriTaxStatusConfiguration());
            modelBuilder.ApplyConfiguration(new ScriEnrollementStatusConfiguration());
            modelBuilder.ApplyConfiguration(new ScriRetentionAgentConfiguration());
            modelBuilder.ApplyConfiguration(new ScriMaillingAddressAddressTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriMaillingAddressCountryConfiguration());
            modelBuilder.ApplyConfiguration(new ScriMaillingAddressStateConfiguration());
            modelBuilder.ApplyConfiguration(new ScriExt_RamoSSNConfiguration());
            modelBuilder.ApplyConfiguration(new ScriExt_PolicyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriProducerCodeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriPaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new ScriPolicyTermConfiguration());
            modelBuilder.ApplyConfiguration(new ScriCurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new ScriProducerAgentConfiguration());
            modelBuilder.ApplyConfiguration(new ScriProducerOfServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ScriServiceOrganizerConfiguration());
            modelBuilder.ApplyConfiguration(new ScriChannelEntryConfiguration());
            modelBuilder.ApplyConfiguration(new ScriStatusConfiguration());
            modelBuilder.ApplyConfiguration(new ScriVehicleConfiguration());
            modelBuilder.ApplyConfiguration(new ScriAutomaticAdjustConfiguration());
            modelBuilder.ApplyConfiguration(new ScriCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ScriColorConfiguration());
            modelBuilder.ApplyConfiguration(new ScriFuelTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ScriJurisdictionConfiguration());
            modelBuilder.ApplyConfiguration(new ScriOriginCountryConfiguration());
            modelBuilder.ApplyConfiguration(new ScriProductOfferingConfiguration());
            modelBuilder.ApplyConfiguration(new ScriRiskLocationConfiguration());
            modelBuilder.ApplyConfiguration(new ScriUsageConfiguration());
            modelBuilder.ApplyConfiguration(new ScriReasonCancelDTOConfiguration());
            
        }
    }
}

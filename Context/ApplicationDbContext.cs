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
    public partial class ApplicationDbContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<AccEventos> AccEventos { get; set; }
        public virtual DbSet<AccGrupos> AccGrupos { get; set; }
        public virtual DbSet<AccGruposXPerfil> AccGruposXPerfil { get; set; }
        public virtual DbSet<AccModulos> AccModulos { get; set; }
        public virtual DbSet<AccPerfiles> AccPerfiles { get; set; }
        public virtual DbSet<LAccPerfiles> LAccPerfiles { get; set; }
        public virtual DbSet<MAccPerfiles> MAccPerfiles { get; set; }
        public virtual DbSet<NAccPerfiles> NAccPerfiles { get; set; }
        public virtual DbSet<AccProgramas> AccProgramas { get; set; }
        public virtual DbSet<AccProgramasAcciones> AccProgramasAcciones { get; set; }
        public virtual DbSet<AccProgramasAccionesXGrupo> AccProgramasAccionesXGrupo { get; set; }
        public virtual DbSet<AccProgramasXModulos> AccProgramasXModulos { get; set; }
        public virtual DbSet<AccTiposEventos> AccTiposEventos { get; set; }
        public virtual DbSet<AccUsuarios> AccUsuarios { get; set; }
        public virtual DbSet<MAccGrupos> MAccGrupos { get; set; }
        public virtual DbSet<LAccGrupos> LAccGrupos { get; set; }
        public virtual DbSet<NAccGrupos> NAccGrupos { get; set; }
        public virtual DbSet<AccProgramasXUsuario> AccProgramasXUsuario { get; set; }
        public virtual DbSet<AccUsuariosSesiones> AccUsuariosSesiones { get; set; }
        public virtual DbSet<AccProgramasFavoritosXUsuario> AccProgramasFavoritosXUsuario { get; set; }
        public virtual DbSet<AccProgramasRecientesXUsuario> AccProgramasRecientesXUsuario { get; set; }
        public virtual DbSet<AccAcciones> AccAcciones { get; set; }
        public virtual DbSet<LAccModulos> LAccModulos { get; set; }
        public virtual DbSet<MAccModulos> MAccModulos { get; set; }
        public virtual DbSet<NAccModulos> NAccModulos { get; set; }
        public virtual DbSet<LAccProgramas> LAccProgramas { get; set; }
        public virtual DbSet<MAccProgramas> MAccProgramas { get; set; }
        public virtual DbSet<NAccProgramas> NAccProgramas { get; set; }
        public virtual DbSet<LAccUsuarios> LAccUsuarios { get; set; }
        public virtual DbSet<MAccUsuarios> MAccUsuarios { get; set; }
        public virtual DbSet<NAccUsuarios> NAccUsuarios { get; set; }
        public virtual DbSet<LAccGruposXPerfil> LAccGruposXPerfil { get; set; }
        public virtual DbSet<MAccGruposXPerfil> MAccGruposXPerfil { get; set; }
        public virtual DbSet<NAccGruposXPerfil> NAccGruposXPerfil { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.ApplyConfiguration(new AccEventosConfiguration());
            modelBuilder.ApplyConfiguration(new AccGruposConfiguration());
            modelBuilder.ApplyConfiguration(new AccGruposXPerfilConfiguration());
            modelBuilder.ApplyConfiguration(new AccModulosConfiguration());
            modelBuilder.ApplyConfiguration(new AccProgramasConfiguration());
            modelBuilder.ApplyConfiguration(new AccProgramasAccionesConfiguration());
            modelBuilder.ApplyConfiguration(new AccProgramasAccionesXGrupoConfiguration());
            modelBuilder.ApplyConfiguration(new AccProgramasXModulosConfiguration());
            modelBuilder.ApplyConfiguration(new AccTiposEventosConfiguration());
            modelBuilder.ApplyConfiguration(new AccUsuariosConfiguration());
            modelBuilder.ApplyConfiguration(new MAccGruposConfiguration());
            modelBuilder.ApplyConfiguration(new LAccGruposConfiguration());
            modelBuilder.ApplyConfiguration(new NAccGruposConfiguration());
            modelBuilder.ApplyConfiguration(new AccProgramasXUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new AccUsuariosSesionesConfiguration());
            modelBuilder.ApplyConfiguration(new AccProgramasFavoritosXUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new AccProgramasRecientesXUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new AccAccionesConfiguration());
            modelBuilder.ApplyConfiguration(new AccPerfilesConfiguration());
            modelBuilder.ApplyConfiguration(new LAccPerfilesConfiguration());
            modelBuilder.ApplyConfiguration(new MAccPerfilesConfiguration());
            modelBuilder.ApplyConfiguration(new NAccPerfilesConfiguration());
            modelBuilder.ApplyConfiguration(new LAccModulosConfiguration());
            modelBuilder.ApplyConfiguration(new MAccModulosConfiguration());
            modelBuilder.ApplyConfiguration(new NAccModulosConfiguration());
            modelBuilder.ApplyConfiguration(new LAccProgramasConfiguration());
            modelBuilder.ApplyConfiguration(new MAccProgramasConfiguration());
            modelBuilder.ApplyConfiguration(new NAccProgramasConfiguration());
            modelBuilder.ApplyConfiguration(new LAccUsuariosConfiguration());
            modelBuilder.ApplyConfiguration(new MAccUsuariosConfiguration());
            modelBuilder.ApplyConfiguration(new NAccUsuariosConfiguration());
            modelBuilder.ApplyConfiguration(new LAccGruposXPerfilConfiguration());
            modelBuilder.ApplyConfiguration(new MAccGruposXPerfilConfiguration());
            modelBuilder.ApplyConfiguration(new NAccGruposXPerfilConfiguration());

        }
    }
}

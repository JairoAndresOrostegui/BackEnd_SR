using CESDE.DataAdapter.models;

using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter
{
    public class CESDE_Context : DbContext
    {
        public CESDE_Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relación de Usuario
            modelBuilder.Entity<UsuarioModel>()
                  .HasOne(fr => fr.ForKeyPersona).WithOne(fr => fr.ForKeyUsuario)
                  .HasForeignKey<UsuarioModel>(key => key.id_persona);

            modelBuilder.Entity<UsuarioModel>()
                .HasOne(fr => fr.ForKeyRol_Usuario).WithMany(fr => fr.ForKeyUsuario_Rol)
                .HasForeignKey(key => key.id_rol);

            modelBuilder.Entity<UsuarioModel>()
                  .HasOne(fr => fr.ForKeyUnidad_Usuario).WithMany(fr => fr.ForKeyUsuario_Unidad)
                  .HasForeignKey(key => key.id_unidad_organizacional);

            //Relaciones de Municipio
            modelBuilder.Entity<MunicipioModel>()
                .HasOne(fr => fr.ForKeyDepartamentoMuni).WithMany(fr => fr.ForKeyMuniDepartamento)
                .HasForeignKey(key => key.id_departamento);

            //Relaciones de Unidad Organizacional
            modelBuilder.Entity<UnidadOrganizacionalModel>()
                  .HasOne(fr => fr.ForKeyMunicipio_Unidad).WithMany(fr => fr.ForKeyUnidad_Municipio)
                  .HasForeignKey(key => key.id_municipio);

            modelBuilder.Entity<UnidadOrganizacionalModel>()
                  .HasOne(fr => fr.ForKeyTipoEspacioUnidad).WithMany(fr => fr.ForKeyUnidadOrganiTipoEspacio)
                  .HasForeignKey(key => key.id_tipo_espacio);

            //Relaciones de Reserva
            modelBuilder.Entity<ReservaModel>()
                  .HasOne(fr => fr.ForKeyUnidadOrg_Reserva).WithMany(fr => fr.ForKeyReserva_UnidadOrg)
                  .HasForeignKey(key => key.id_unidad_organizacional);

            //Relaciones de Unidad Organizacional - Caracteristica
            modelBuilder.Entity<UnidadOrganizacionalCaracteristicaModel>()
                  .HasOne(fr => fr.ForKeyUnidadOrgani_UOC).WithMany(fr => fr.ForKeyUOC_UnidadOrgani)
                  .HasForeignKey(key => key.id_unidad_organizacional);

            modelBuilder.Entity<UnidadOrganizacionalCaracteristicaModel>()
                  .HasOne(fr => fr.ForKeyCaracteristica_UOC).WithMany(fr => fr.ForKeyUOC_Caracteristica)
                  .HasForeignKey(key => key.id_caracteristica);

            //Relación de Funcionalidad
            modelBuilder.Entity<FuncionalidadModel>()
                  .HasOne(fr => fr.ForKeyCompo_Func).WithMany(fr => fr.ForKeyFunc_Compo)
                  .HasForeignKey(key => key.id_funcionalidad);

            //Relación de Permiso
            modelBuilder.Entity<PermisosRolModel>()
                  .HasOne(fr => fr.ForKeyRol_Permisos).WithMany(fr => fr.ForKeyPermisos_Rol)
                  .HasForeignKey(key => key.id_rol);

            modelBuilder.Entity<PermisosRolModel>()
                  .HasOne(fr => fr.ForKeyFunc_Permisos).WithMany(fr => fr.ForKeyPermisos_Func)
                  .HasForeignKey(key => key.id_funcionalidad);

            //Relaciones de Reserva
            modelBuilder.Entity<ReservaModel>()
                  .HasOne(fr => fr.ForKeyUnidadOrg_Reserva).WithMany(fr => fr.ForKeyReserva_UnidadOrg)
                  .HasForeignKey(key => key.id_unidad_organizacional);

            //Relaciones de Reserva Dia
            modelBuilder.Entity<ReservaDiaModel>()
                  .HasOne(fr => fr.ForKeyReserva_ReservaDia).WithMany(fr => fr.ForKeyReservaDia_Reserva)
                  .HasForeignKey(key => key.id_reserva);

            //Relaciones de Rol Espacio
            modelBuilder.Entity<RolEspacioModel>()
                  .HasOne(fr => fr.ForKeyRol_RolEspacio).WithMany(fr => fr.ForKeyRolEspacio_Rol)
                  .HasForeignKey(key => key.id_rol);

            modelBuilder.Entity<RolEspacioModel>()
                  .HasOne(fr => fr.ForKeyTipoEspacio_RolEspacio).WithMany(fr => fr.ForKeyRolEspacio_TipoEspacio)
                  .HasForeignKey(key => key.id_tipo_espacio);

            //Relaciones de Rol Espacio
            modelBuilder.Entity<UnidadRolModel>()
                  .HasOne(fr => fr.ForKeyUnidadOrgani_UnidadRol).WithMany(fr => fr.ForKeyUnidadRol_UnidadOrgani)
                  .HasForeignKey(key => key.id_unidad_organizacional);

            modelBuilder.Entity<UnidadRolModel>()
                  .HasOne(fr => fr.ForKeyRol_UnidadRol).WithMany(fr => fr.ForKeyUnidadRol_Rol)
                  .HasForeignKey(key => key.id_rol);
        }

        public DbSet<DepartamentoModel> DepartamentoModels { get; set; }
        public DbSet<UsuarioModel> UsuarioModels { get; set; }
        public DbSet<MunicipioModel> MunicipioModels { get; set; }
        public DbSet<TipoEspacioModel> TipoEspacioModels { get; set; }
        public DbSet<PermisosRolModel> PermisosRolModels { get; set; }
        public DbSet<RolModel> RolModels { get; set; }
        public DbSet<ReservaModel> ReservaModels { get; set; }
        public DbSet<ReservaDiaModel> ReservaDiaModels { get; set; }
        public DbSet<RolEspacioModel> RolEspacioModels { get; set; }
        public DbSet<UnidadRolModel> UnidadRolModels { get; set; }

        public DbSet<UnidadOrganizacionalModel> UnidadOrganizacionalModels { get; set; }
        public DbSet<ComponenteModel> ComponenteModels { get; set; }
        public DbSet<FuncionalidadModel> FuncionalidadModels { get; set; }
        public DbSet<CaracteristicaModel> CaracteristicaModels { get; set; }
        public DbSet<UnidadOrganizacionalCaracteristicaModel> UnidadOrganizacionalCaracteristicaModels { get; set; }
    }
}
using CESDE.DataAdapter.models;
using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter
{
    public class CESDE_ReservasContext : DbContext
    {
        public CESDE_ReservasContext(DbContextOptions<CESDE_ReservasContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relaciones de Reserva
            modelBuilder.Entity<ReservaModel>()
                  .HasOne(fr => fr.ForKeyUnidadOrg_Reserva).WithMany(fr => fr.ForKeyReserva_UnidadOrg)
                  .HasForeignKey(key => key.id_unidad_organizacional);

            modelBuilder.Entity<ReservaDiaModel>()
                  .HasOne(fr => fr.ForKeyReserva_ReservaDia).WithMany(fr => fr.ForKeyReservaDia_Reserva)
                  .HasForeignKey(key => key.id_reserva);
        }
        public DbSet<ReservaModel> ReservaModels { get; set; }
        public DbSet<ReservaDiaModel> ReservaDiaModels { get; set; }

    }
}

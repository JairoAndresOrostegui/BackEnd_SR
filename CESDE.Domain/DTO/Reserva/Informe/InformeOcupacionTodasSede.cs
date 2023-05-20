namespace CESDE.Domain.DTO.Reserva.Informe
{
    public class InformeOcupacionTodasSede
    {
        public string nombre_sede { get; set; }

        public int cantidad_tipoespacio { get; set; }
        public int cantidad_reserva { get; set; }

        public int jornada1 { get; set; }
        public int jornada2 { get; set; }
        public int jornada3 { get; set; }
        public int jornada4 { get; set; }
        public int jornada5 { get; set; }
    }
}

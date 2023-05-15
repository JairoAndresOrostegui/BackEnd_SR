namespace CESDE.Domain.DTO.Reserva
{
      public class ReservaDiaDTO
      {
            public long reserva_dia_id { get; set; }
            public long id_reserva { get; set; }
            public string reserva_dia_dia { get; set; }
            public int reserva_dia_hora_inicio { get; set; }
            //public int reserva_dia_hora_fin { get; set; }

            public string jornada { get; set; }
      }
}
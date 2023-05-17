using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.DTO.Reserva.Filtros;
using CESDE.Domain.DTO.Reserva.Informe;
using CESDE.Domain.DTO.Reserva.Reporte;
using CESDE.Domain.Models;


namespace CESDE.Application.Ports
{
      public interface IReservaRepositoryPort
      {
            Task SaveReserva(InsertarReservaDTO reserva);

            Task<Reserva> GetById(long id_reserva);

            Task<List<ReservaDTO>> GetAll();

            Task<List<ReservaDTO>> GetBySearch(string type, string search);

            Task UpdateReserva(InsertarReservaDTO reserva);

            Task DeleteReserva(long id_reserva);

            Task<List<FiltroReservaDTO>> GetProgramaByEscuela(string id_submodulo);

            Task<ReporteHorario> GetReporteDocenteEstudiante(string grupo, int nivel, string codigo_programa);

            Task<List<ReporteDocente>> GetReporteDocente(long id_colaborador);

            Task<List<ComboDTO>> GetComboUsuarioColaborador();

            Task<InformeOcupacionSede> GetContarOcupacionAulas(long id_sede);

            Task<List<InformeOcupacionTodasSede>> GetContarOcupacionTodosEspacios();

            Task<InformeUnidadesReservadas> GetUnidadesReservadas(long id_unidad_organizacional);

            Task<List<ReservaDTO>> GetfiltrarUsuariosPorRol(long nivel_rol, string area_rol);
      }
}

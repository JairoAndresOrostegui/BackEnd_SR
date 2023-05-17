using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.DTO.Reserva.Filtros;
using CESDE.Domain.DTO.Reserva.Informe;
using CESDE.Domain.DTO.Reserva.Reporte;
using CESDE.Domain.Models;

namespace CESDE.Application.UseCases
{
      public class ReservaUseCase
      {
            private readonly IReservaRepositoryPort _reservaRepository;

            public ReservaUseCase(IReservaRepositoryPort reservaRepository)
            {
                  _reservaRepository = reservaRepository;
            }

            public async Task SaveReserva(InsertarReservaDTO reserva) => await _reservaRepository.SaveReserva(reserva);

            public async Task<Reserva> GetById(long id_reserva) => await _reservaRepository.GetById(id_reserva);

            public async Task<List<ReservaDTO>> GetAll() => await _reservaRepository.GetAll();

            public async Task<List<ReservaDTO>> GetBySearch(string type, string search) => await _reservaRepository.GetBySearch(type, search);

            public async Task UpdateReserva(InsertarReservaDTO reserva) => await _reservaRepository.UpdateReserva(reserva);

            public async Task DeleteReserva(long id_reserva) => await _reservaRepository.DeleteReserva(id_reserva);

            public async Task<List<ReporteDocente>> GetReporteDocente(long id_colaborador) => await _reservaRepository.GetReporteDocente(id_colaborador);

            public async Task<List<ComboDTO>> GetComboUsuarioColaborador() => await _reservaRepository.GetComboUsuarioColaborador();

            public async Task<List<FiltroReservaDTO>> GetProgramaByEscuela(string id_submodulo) => await _reservaRepository.GetProgramaByEscuela(id_submodulo);

            public async Task<ReporteHorario> GetReporteDocenteEstudiante(string grupo, int nivel, string codigo_programa) =>
                      await _reservaRepository.GetReporteDocenteEstudiante(grupo, nivel, codigo_programa);

            public async Task<InformeOcupacionSede> GetContarOcupacionAulas(long id_sede) => await _reservaRepository.GetContarOcupacionAulas(id_sede);

            public async Task<List<InformeOcupacionTodasSede>> GetContarOcupacionTodosEspacios() =>
                  await _reservaRepository.GetContarOcupacionTodosEspacios();

            public async Task<InformeUnidadesReservadas> GetUnidadesReservadas(long id_unidad_organizacional) =>
                  await _reservaRepository.GetUnidadesReservadas(id_unidad_organizacional);

            public async Task<List<ReservaDTO>> GetfiltrarUsuariosPorRol(long nivel_rol, string area_rol) =>
                await _reservaRepository.GetfiltrarUsuariosPorRol(nivel_rol, area_rol);
    }
}
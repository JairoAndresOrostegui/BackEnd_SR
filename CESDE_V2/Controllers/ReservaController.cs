using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.DTO.Reserva.Reporte;
using CESDE.Domain.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
      //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      [Route("api/reserva")]
      [ApiController]
      public class ReservaController : ControllerBase
      {
            private readonly IReservaRepositoryPort _reservaRepositoryPort;

            public ReservaController(IReservaRepositoryPort reservaRepositoryPort)
            {
                  _reservaRepositoryPort = reservaRepositoryPort;
            }

            [HttpGet("{id_reserva:long}")]
            public async Task<IActionResult> GetById(long id_reserva)
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetById(id_reserva);
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetAll();
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return new NotFoundObjectResult(ex.Message);
                  }
            }

          

        [HttpGet("buscar")]
            public async Task<IActionResult> GetBySearch(string type, string search)
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetBySearch(type, search);
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("escuela")]
            public async Task<IActionResult> GetProgramaByEscuela(string id_submodulo)
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetProgramaByEscuela(id_submodulo);
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("combo-colaborador")]
            public async Task<IActionResult> GetComboUsuarioColaborador()
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetComboUsuarioColaborador();
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("reporte-docente")]
            public async Task<IActionResult> GetReporteDocente(long id_colaborador)
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetReporteDocente(id_colaborador);
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("reporte-docente-estudiante")]
            public async Task<IActionResult> GetReporteDocenteEstudiante(string grupo, int nivel, string codigo_programa)
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetReporteDocenteEstudiante(grupo, nivel, codigo_programa);
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }
        
            [HttpGet("informe-sede")]
            public async Task<IActionResult> GetContarOcupacionAulas(long id_sede)
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetContarOcupacionAulas(id_sede);
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("informe-allsede")]
            public async Task<IActionResult> GetContarOcupacionTodosEspacios()
            {
                  try
                  {
                        var reserva = await _reservaRepositoryPort.GetContarOcupacionTodosEspacios();
                        return Ok(reserva);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }
   

            [HttpPost]
            public async Task<IActionResult> SaveReserva([FromBody] InsertarReservaDTO reserva)
            {
                  try
                  {
                        await _reservaRepositoryPort.SaveReserva(reserva);
                        return Ok(new { message = Enums.MessageSave });
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpPut]
            public async Task<IActionResult> UpdateReserva([FromBody] InsertarReservaDTO reserva)
            {
                  try
                  {
                        await _reservaRepositoryPort.UpdateReserva(reserva);
                        return Ok(new { message = Enums.MessageUpdate });
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpDelete("{id_reserva:long}")]
            public async Task<IActionResult> DeleteReserva(long id_reserva)
            {
                  try
                  {
                        await _reservaRepositoryPort.DeleteReserva(id_reserva);
                        return Ok(new { message = Enums.MessageDelete });
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("informe-unidades-reservadas")]
            public async Task<IActionResult> GetUnidadesReservadas(long id_unidad_organizacional)
            {
                  try
                  {
                        var unidades = await _reservaRepositoryPort.GetUnidadesReservadas(id_unidad_organizacional);
                        return Ok(unidades);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("filtrar-rol-usuario")]
            public async Task<IActionResult> GetFiltrarUsuarioRol(long nivel_rol, string area_rol)
            {
                  try
                  {
                        var unidades = await _reservaRepositoryPort.GetfiltrarUsuariosPorRol(nivel_rol, area_rol);
                        return Ok(unidades);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }
      }
}

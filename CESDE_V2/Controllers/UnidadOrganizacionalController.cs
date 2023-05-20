using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/unidadorganizacional")]
    [ApiController]
    public class UnidadOrganizacionalController : ControllerBase
    {
        private readonly IUnidadOrganizacionalRepositoryPort _unidadOrganizacionalRepositoryPort;

        public UnidadOrganizacionalController(IUnidadOrganizacionalRepositoryPort unidadOrganizacionalRepositoryPort)
        {
            _unidadOrganizacionalRepositoryPort = unidadOrganizacionalRepositoryPort;
        }


        [HttpGet("{id_unidad_organizacional:long}")]
        public async Task<IActionResult> GetById(long id_unidad_organizacional)
        {
            try
            {
                var unidadOrganizacional = await _unidadOrganizacionalRepositoryPort.GetById(id_unidad_organizacional);
                return Ok(unidadOrganizacional);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("combo")]
        public async Task<IActionResult> GetAllCombo()
        {
            try
            {
                var unidadOrganizacional = await _unidadOrganizacionalRepositoryPort.GetAllCombo();
                return Ok(unidadOrganizacional);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpPost("reserva")]
        public async Task<IActionResult> GetByPadreAndTipoEspacio(ParametroReservaDTO parametroReservaDTO)
        {
            try
            {
                var unidadOrganizacional = await _unidadOrganizacionalRepositoryPort.GetByPadreAndTipoEspacio(parametroReservaDTO);
                return Ok(unidadOrganizacional);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("combo/{id_tipo_espacio:long}")]
        public async Task<IActionResult> GetByTipoEspacioCombo(int id_tipo_espacio)
        {
            try
            {
                var unidadOrganizacional = await _unidadOrganizacionalRepositoryPort.GetByTipoEspacioCombo(id_tipo_espacio);
                return Ok(unidadOrganizacional);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("validatename")]
        public async Task<IActionResult> ValidateNameUnidadOrganizacional(string nombre_unidad_organizacional, long id_sede)
        {
            try
            {
                var unidadOrganizacional = await _unidadOrganizacionalRepositoryPort
                      .ValidateNameUnidadOrganizacional(nombre_unidad_organizacional, id_sede);
                return Ok(unidadOrganizacional);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(long id_sede)
        {
            try
            {
                var unidadOrganizacional = await _unidadOrganizacionalRepositoryPort.GetAll(id_sede);
                return Ok(unidadOrganizacional);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> GetBySearch(string type, string search, long id_sede)
        {
            try
            {
                var unidadOrganizacional = await _unidadOrganizacionalRepositoryPort.GetBySearch(type, search, id_sede);
                return Ok(unidadOrganizacional);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveUnidadOrganizacional([FromBody] UnidadOrganizacional unidadOrganizacional)
        {
            try
            {
                await _unidadOrganizacionalRepositoryPort.SaveUnidadOrganizacional(unidadOrganizacional);
                return Ok(new { message = Enums.MessageSave });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUnidadOrganizacional([FromBody] UnidadOrganizacional unidadOrganizacional)
        {
            try
            {
                await _unidadOrganizacionalRepositoryPort.UpdateUnidadOrganizacional(unidadOrganizacional);
                return Ok(new { message = Enums.MessageUpdate });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpDelete("{id_unidad_organizacional:long}")]
        public async Task<IActionResult> DeleteUnidadOrganizacional(long id_unidad_organizacional)
        {
            try
            {
                await _unidadOrganizacionalRepositoryPort.DeleteUnidadOrganizacional(id_unidad_organizacional);
                return Ok(new { message = Enums.MessageDelete });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }
    }
}
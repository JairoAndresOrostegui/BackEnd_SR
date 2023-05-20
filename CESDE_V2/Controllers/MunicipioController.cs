using CESDE.Application.Ports;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/municipio")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        private readonly IMunicipioRepositoryPort _municipioRepositoryPort;

        public MunicipioController(IMunicipioRepositoryPort municipioRepositoryPort)
        {
            _municipioRepositoryPort = municipioRepositoryPort;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var datos = await _municipioRepositoryPort.GetAll();
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("{id_departamento:long}")]
        public async Task<IActionResult> GetAll(long id_departamento)
        {
            try
            {
                var datos = await _municipioRepositoryPort.GetAllByDepartamento(id_departamento);
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("getByIdMunicipio/{id_municipio:long}")]
        public async Task<IActionResult> GetByIdMunicipio(long id_municipio)
        {
            try
            {
                var datos = await _municipioRepositoryPort.GetByIdMunicipio(id_municipio);
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }
    }
}
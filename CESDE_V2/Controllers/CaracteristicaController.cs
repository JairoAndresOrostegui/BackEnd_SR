using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/caracteristica")]
    [ApiController]
    public class CaracteristicaController : ControllerBase
    {
        private readonly ICaracteristicaRepositoryPort _caracteristicaRepositoryPort;

        public CaracteristicaController(ICaracteristicaRepositoryPort caracteristicaRepositoryPort)
        {
            _caracteristicaRepositoryPort = caracteristicaRepositoryPort;
        }

        [HttpGet("{id_caracteristica:long}")]
        public async Task<IActionResult> GetById(long id_caracteristica)
        {
            try
            {
                var caracteristica = await _caracteristicaRepositoryPort.GetById(id_caracteristica);
                return Ok(caracteristica);
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
                var caracteristica = await _caracteristicaRepositoryPort.GetAll();
                return Ok(caracteristica);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> GetBySearch(string type, string search)
        {
            try
            {
                var caracteristica = await _caracteristicaRepositoryPort.GetBySearch(type, search);
                return Ok(caracteristica);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("validatename/{nombre_tipo_espacio}")]
        public async Task<IActionResult> ValidateName(string nombre_tipo_espacio)
        {
            try
            {
                var caracteristica = await _caracteristicaRepositoryPort.ValidateName(nombre_tipo_espacio);
                return Ok(caracteristica);
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
                var caracteristica = await _caracteristicaRepositoryPort.GetAllCombo();
                return Ok(caracteristica);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveCaracteristica([FromBody] Caracteristica caracteristica)
        {
            try
            {
                await _caracteristicaRepositoryPort.SaveCaracteristica(caracteristica);
                return Ok(new { message = Enums.MessageSave });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCaracteristica([FromBody] Caracteristica caracteristica)
        {
            try
            {
                await _caracteristicaRepositoryPort.UpdateCaracteristica(caracteristica);
                return Ok(new { message = Enums.MessageUpdate });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpDelete("{id_caracteristica:long}")]
        public async Task<IActionResult> DeleteCaracteristica(long id_caracteristica)
        {
            try
            {
                await _caracteristicaRepositoryPort.DeleteCaracteristica(id_caracteristica);
                return Ok(new { message = Enums.MessageDelete });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }
    }
}
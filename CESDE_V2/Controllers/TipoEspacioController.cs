using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.Domain.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      [Route("api/tipoespacio")]
      [ApiController]
      public class TipoEspacioController : ControllerBase
      {
            private readonly ITipoEspacioRepositoryPort _tipoEspacioRepositoryPort;

            public TipoEspacioController(ITipoEspacioRepositoryPort tipoEspacioRepositoryPort)
            {
                  _tipoEspacioRepositoryPort = tipoEspacioRepositoryPort;
            }

            [HttpGet("{id_tipo_espacio:long}")]
            public async Task<IActionResult> GetById(long id_tipo_espacio)
            {
                  try
                  {
                        var tipoEspacio = await _tipoEspacioRepositoryPort.GetById(id_tipo_espacio);
                        return Ok(tipoEspacio);
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
                        var tipoEspacio = await _tipoEspacioRepositoryPort.GetAll();
                        return Ok(tipoEspacio);
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
                        var tipoEspacio = await _tipoEspacioRepositoryPort.GetBySearch(type, search);
                        return Ok(tipoEspacio);
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
                        var tipoEspacio = await _tipoEspacioRepositoryPort.ValidateName(nombre_tipo_espacio);
                        return Ok(tipoEspacio);
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
                        var tipoEspacio = await _tipoEspacioRepositoryPort.GetAllCombo();
                        return Ok(tipoEspacio);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpPost]
            public async Task<IActionResult> SaveTipoEspacio([FromBody] TipoEspacio tipoEspacio)
            {
                  try
                  {
                        await _tipoEspacioRepositoryPort.SaveTipoEspacio(tipoEspacio);
                        return Ok(new { message = Enums.MessageSave });
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpPut]
            public async Task<IActionResult> UpdateTipoEspacio([FromBody] TipoEspacio tipoEspacio)
            {
                  try
                  {
                        await _tipoEspacioRepositoryPort.UpdateTipoEspacio(tipoEspacio);
                        return Ok(new { message = Enums.MessageUpdate });
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpDelete("{id_tipo_espacio:long}")]
            public async Task<IActionResult> DeleteTipoEspacio(long id_tipo_espacio)
            {
                  try
                  {
                        await _tipoEspacioRepositoryPort.DeleteTipoEspacio(id_tipo_espacio);
                        return Ok(new { message = Enums.MessageDelete });
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }
      }
}
using CESDE.Application.Ports;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      [Route("api/departamento")]
      [ApiController]
      public class DepartamentoController : ControllerBase
      {
            private readonly IDepartamentoRepositoryPort _departamentoRepositoryPort;

            public DepartamentoController(IDepartamentoRepositoryPort departamentoRepositoryPort)
            {
                  _departamentoRepositoryPort = departamentoRepositoryPort;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                  try
                  {
                        var datos = await _departamentoRepositoryPort.GetAll();
                        return Ok(datos);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("{id_pais:long}")]
            public async Task<IActionResult> GetAll(long id_pais)
            {
                  try
                  {
                        var datos = await _departamentoRepositoryPort.GetAllByPais(id_pais);
                        return Ok(datos);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }
      }
}
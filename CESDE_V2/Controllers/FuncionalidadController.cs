using CESDE.Application.Ports;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      [Route("api/funcionalidad")]
      [ApiController]
      public class FuncionalidadController : ControllerBase
      {
            private readonly IFuncionalidadRepositoryPort _funcionalidadRepositoryPort;

            public FuncionalidadController(IFuncionalidadRepositoryPort funcionalidadRepositoryPort)
            {
                  _funcionalidadRepositoryPort = funcionalidadRepositoryPort;
            }

            [HttpGet("{id_componente:long}")]
            public async Task<IActionResult> GetAllByIdComponente(long id_componente)
            {
                  try
                  {
                        var funcionalidad = await _funcionalidadRepositoryPort.GetAllByIdComponente(id_componente);
                        return Ok(funcionalidad);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }

            [HttpGet("returnIdComponente/{id_funcionalidad:long}")]
            public async Task<IActionResult> ReturnIdComponente(long id_funcionalidad)
            {
                  try
                  {
                        var rol = await _funcionalidadRepositoryPort.ReturnIdComponente(id_funcionalidad);
                        return Ok(rol);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }
      }
}
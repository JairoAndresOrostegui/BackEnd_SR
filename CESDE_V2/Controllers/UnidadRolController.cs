using CESDE.Application.Ports;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      [Route("api/unidadrol")]
      [ApiController]
      public class UnidadRolController : ControllerBase
      {
            private readonly IUnidadRolRepositoryPort _unidadRolRepositoryPort;

            public UnidadRolController(IUnidadRolRepositoryPort unidadRolRepositoryPort)
            {
                  _unidadRolRepositoryPort = unidadRolRepositoryPort;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllByUnidadRol(long id_rol)
            {
                  try
                  {
                        var datos = await _unidadRolRepositoryPort.GetAllByUnidadRol(id_rol);
                        return Ok(datos);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }
      }
}
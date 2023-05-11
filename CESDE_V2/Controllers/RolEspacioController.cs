using CESDE.Application.Ports;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      [Route("api/rolespacio")]
      [ApiController]
      public class RolEspacioController : ControllerBase
      {
            private readonly IRolEspacioRepositoryPort _rolEspacioRepositoryPort;

            public RolEspacioController(IRolEspacioRepositoryPort rolEspacioRepositoryPort)
            {
                  _rolEspacioRepositoryPort = rolEspacioRepositoryPort;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllByRolEspacio(long id_rol)
            {
                  try
                  {
                        var datos = await _rolEspacioRepositoryPort.GetAllByRolEspacio(id_rol);
                        return Ok(datos);
                  }
                  catch (Exception ex)
                  {
                        return Ok(new { message = ex.Message });
                  }
            }
      }
}
using CESDE.Application.Ports;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/componente")]
    [ApiController]
    public class ComponenteController : ControllerBase
    {
        private readonly IComponenteRepositoryPort _componenteRepositoryPort;

        public ComponenteController(IComponenteRepositoryPort componenteRepositoryPort)
        {
            _componenteRepositoryPort = componenteRepositoryPort;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCombo()
        {
            try
            {
                var componente = await _componenteRepositoryPort.GetAllCombo();
                return Ok(componente);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }
    }
}
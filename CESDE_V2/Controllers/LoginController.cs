using CESDE.Application.Ports;
using CESDE.DataAdapter;
using CESDE.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CESDE_API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepositoryPort _loginRepositoryPort;
        private readonly IConfiguration _config;

        public LoginController(ILoginRepositoryPort loginRepositoryPort, IConfiguration config)
        {
            _loginRepositoryPort = loginRepositoryPort;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login login)
        {
            try
            {
                var user = await _loginRepositoryPort.ValidateUser(login);
                //var mensaje = await _loginRepositoryPort.ValidateStateDataUser(login.login_usuario);
                string tokenString = ConfigureJWT.GetToken(user.id_usuario, _config);

                return Ok(new { token = tokenString, user /*message = mensaje*/ });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Login login)
        //{
        //      try
        //      {
        //            var user = await _loginRepositoryPort.ValidateUser(login);
        //            var mensaje = await _loginRepositoryPort.ValidateStateDataUser(login.login_usuario);
        //            string tokenString = ConfigureJWT.GetToken(user.id_usuario, _config);

        //            return Ok(new { token = tokenString, user, message = mensaje });
        //      }
        //      catch (Exception ex)
        //      {
        //            return Ok(new { message = ex.Message });
        //      }
        //}

        //[HttpPost("changepassword")]
        //public async Task<IActionResult> PostChangePassword([FromBody] Login login)
        //{
        //      try
        //      {
        //            var result = await _loginRepositoryPort.ChangePassword(login);

        //            if (result)
        //                  return Ok(new { message = Enums.MessageChangePassword });
        //            else
        //                  return Ok(new { message = Enums.MessageErrorChangePassword });
        //      }
        //      catch (Exception ex)
        //      {
        //            return Ok(new { message = ex.Message });
        //      }
        //}

        //[HttpGet("validate/{login_usuario}")]
        //public async Task<IActionResult> ValidateNameUser(string login_usuario)
        //{
        //      try
        //      {
        //            var result = await _loginRepositoryPort.ValidateNameUser(login_usuario);
        //            return Ok(result);
        //      }
        //      catch (Exception ex)
        //      {
        //            return Ok(new { message = ex.Message });
        //      }
        //}
    }
}
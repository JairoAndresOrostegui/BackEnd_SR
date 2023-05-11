using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.DTOForUsuario;
using CESDE.Domain.DTO.Usuario;
using CESDE.Domain.Models;

namespace CESDE.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly ILoginRepositoryPort _loginRepository;

        public LoginUseCase(ILoginRepositoryPort loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<SistemaAutenticacionDTO> ValidateUser(Login login) => await _loginRepository.ValidateUser(login);

        //public async Task<string> ValidateStateDataUser(string login_usuario) => await _loginRepository.ValidateStateDataUser(login_usuario);

        //public async Task<bool> ChangePassword(Login login) => await _loginRepository.ChangePassword(login);

        //public async Task<bool> ValidateNameUser(string login_usuario) => await _loginRepository.ValidateNameUser(login_usuario);

        public async Task<SistemaAutenticacionDTO> GetSistemaAutenticacion(long id_usuario, long id_rol) =>
              await _loginRepository.GetSistemaAutenticacion(id_usuario, id_rol);
    }
}
using CESDE.Domain.DTO.DTOForUsuario;
using CESDE.Domain.DTO.Usuario;
using CESDE.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.Ports
{
      public interface ILoginRepositoryPort
      {
        Task<SistemaAutenticacionDTO> ValidateUser(Login login);

        //Task<bool> ChangePassword(Login login);

        //Task<bool> ValidateNameUser(string login_usuario);

        //Task<string> ValidateStateDataUser(string login_usuario);

        Task<SistemaAutenticacionDTO> GetSistemaAutenticacion(long id_usuario, long id_rol);
    }
}
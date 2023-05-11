using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Domain.DTO.Combo;

namespace CESDE.Application.Ports
{
      public interface IUnidadRolRepositoryPort
      {
            Task<List<ComboDTO>> GetAllByUnidadRol(long id_rol);
      }
}
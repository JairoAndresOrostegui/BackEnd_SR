using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Domain.DTO.Combo;

namespace CESDE.Application.Ports
{
      public interface IFuncionalidadRepositoryPort
      {
            Task<List<ComboDTO>> GetAllByIdComponente(long id_componente);

            Task<long> ReturnIdComponente(long id_funcionalidad);
      }
}
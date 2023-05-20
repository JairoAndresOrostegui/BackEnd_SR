using CESDE.Domain.DTO.Combo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.Ports
{
    public interface IFuncionalidadRepositoryPort
    {
        Task<List<ComboDTO>> GetAllByIdComponente(long id_componente);

        Task<long> ReturnIdComponente(long id_funcionalidad);
    }
}
using CESDE.Domain.DTO.Combo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.Ports
{
    public interface IDepartamentoRepositoryPort
    {
        Task<List<ComboDTO>> GetAllByPais(long id_pais);

        Task<List<ComboDTO>> GetAll();
    }
}
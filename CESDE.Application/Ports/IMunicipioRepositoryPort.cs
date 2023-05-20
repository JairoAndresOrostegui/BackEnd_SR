using CESDE.Domain.DTO.Combo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.Ports
{
    public interface IMunicipioRepositoryPort
    {
        Task<List<ComboDTO>> GetAllByDepartamento(long id_departamento);

        Task<List<ComboDTO>> GetAll();

        Task<long> GetByIdMunicipio(long id_municipio);
    }
}
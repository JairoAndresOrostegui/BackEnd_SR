using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.UseCases
{
    public class FuncionalidadUseCase
    {
        private readonly IFuncionalidadRepositoryPort _funcionalidadRepository;

        public FuncionalidadUseCase(IFuncionalidadRepositoryPort funcionalidadRepository)
        {
            _funcionalidadRepository = funcionalidadRepository;
        }

        public async Task<List<ComboDTO>> GetAllByIdComponente(long id_componente) =>
              await _funcionalidadRepository.GetAllByIdComponente(id_componente);

        public async Task<long> ReturnIdComponente(long id_funcionalidad) =>
              await _funcionalidadRepository.ReturnIdComponente(id_funcionalidad);
    }
}
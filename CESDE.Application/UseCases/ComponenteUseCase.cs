using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.UseCases
{
      public class ComponenteUseCase
      {
            private readonly IComponenteRepositoryPort _componenteRepository;

            public ComponenteUseCase(IComponenteRepositoryPort componenteRepository)
            {
                  _componenteRepository = componenteRepository;
            }

            public async Task<List<ComboDTO>> GetAllCombo() => await _componenteRepository.GetAllCombo();
      }
}
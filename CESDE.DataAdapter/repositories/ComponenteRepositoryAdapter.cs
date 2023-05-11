using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter.repositories
{
      public class ComponenteRepositoryAdapter : IComponenteRepositoryPort
      {

            private readonly CESDE_Context _context;

            public ComponenteRepositoryAdapter(CESDE_Context context)
            {
                  _context = context;
            }

            public async Task<List<ComboDTO>> GetAllCombo()
            {
                  return await _context.ComponenteModels.Where(vali => vali.estado_componente == true)
                        .Select(enti => new ComboDTO()
                        {
                              value = enti.id_componente,
                              label = enti.nombre_componente.ToUpper()
                        }).OrderBy(ord => ord.label).ToListAsync();
            }
      }
}
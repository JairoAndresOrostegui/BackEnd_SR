using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter.repositories
{
      public class FuncionalidadRepositoryAdapter : IFuncionalidadRepositoryPort
      {

            private readonly CESDE_Context _context;

            public FuncionalidadRepositoryAdapter(CESDE_Context context)
            {
                  _context = context;
            }

            public async Task<List<ComboDTO>> GetAllByIdComponente(long id_componente)
            {
                  return await _context.FuncionalidadModels
                        .Where(vali => vali.id_componente == id_componente && vali.estado_funcionalidad == true)
                        .Select(enti => new ComboDTO()
                        {
                              value = enti.id_funcionalidad,
                              label = enti.nombre_funcionalidad.ToUpper()
                        }).OrderBy(ord => ord.label).ToListAsync();
            }

            public async Task<long> ReturnIdComponente(long id_funcionalidad)
            {
                  return await _context.FuncionalidadModels
                        .Where(vali => vali.id_funcionalidad == id_funcionalidad)
                        .Select(x => x.id_componente).FirstOrDefaultAsync();
            }
      }
}
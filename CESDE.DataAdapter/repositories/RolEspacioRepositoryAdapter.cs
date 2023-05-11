using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter.repositories
{
      public class RolEspacioRepositoryAdapter : IRolEspacioRepositoryPort
      {

            private readonly CESDE_Context _context;

            public RolEspacioRepositoryAdapter(CESDE_Context context)
            {
                  _context = context;
            }

            public async Task<List<ComboDTO>> GetAllByRolEspacio(long id_rol)
            {
                  var lsRolEspacio = await _context.RolEspacioModels.Include(r => r.ForKeyRol_RolEspacio)
                        .Include(te => te.ForKeyTipoEspacio_RolEspacio).Where(rol => rol.id_rol == id_rol)
                        .Select(item => new ComboDTO()
                        {
                              value = item.id_tipo_espacio,
                              label = item.ForKeyTipoEspacio_RolEspacio.nombre_tipo_espacio
                        }).ToListAsync();

                  return lsRolEspacio;
            }
      }
}
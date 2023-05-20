using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CESDE.DataAdapter.repositories
{
    public class UnidadRolRepositoryAdapter : IUnidadRolRepositoryPort
    {

        private readonly CESDE_Context _context;

        public UnidadRolRepositoryAdapter(CESDE_Context context)
        {
            _context = context;
        }

        public async Task<List<ComboDTO>> GetAllByUnidadRol(long id_rol)
        {
            var lsUnidadRol = await _context.UnidadRolModels
                  .OrderBy(x => x.ForKeyUnidadOrgani_UnidadRol.nombre_unidad_organizacional)
                  .Include(r => r.ForKeyRol_UnidadRol)
                  .Include(te => te.ForKeyUnidadOrgani_UnidadRol).Where(rol => rol.id_rol == id_rol)
                  .Select(item => new ComboDTO()
                  {
                      value = item.id_unidad_organizacional,
                      label = item.ForKeyUnidadOrgani_UnidadRol.nombre_unidad_organizacional.ToUpper()
                  }).ToListAsync();

            return lsUnidadRol;
        }
    }
}
using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.Domain.DTO.Combo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CESDE.DataAdapter.repositories
{
    public class DepartamentoRepositoryAdapter : IDepartamentoRepositoryPort
    {

        private readonly CESDE_Context _context;

        public DepartamentoRepositoryAdapter(CESDE_Context context)
        {
            _context = context;
        }

        public async Task<List<ComboDTO>> GetAll()
        {
            return await _context.DepartamentoModels.OrderBy(x => x.nombre_departamento)
                  .Where(status => status.estado_departamento.Equals(Enums.StateAsset))
                  .Select(enti => new ComboDTO()
                  {
                      value = enti.id_departamento,
                      label = enti.nombre_departamento.ToUpper()
                  }).ToListAsync();
        }

        public async Task<List<ComboDTO>> GetAllByPais(long id_pais)
        {
            return await _context.DepartamentoModels.OrderBy(x => x.nombre_departamento)
                  .Where(sear => sear.id_pais == id_pais)
                  .Select(enti => new ComboDTO()
                  {
                      value = enti.id_departamento,
                      label = enti.nombre_departamento.ToUpper()
                  }).ToListAsync();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.Domain.DTO.Combo;

using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter.repositories
{
      public class MunicipioRepositoryAdapter : IMunicipioRepositoryPort
      {

            private readonly CESDE_Context _context;

            public MunicipioRepositoryAdapter(CESDE_Context context)
            {
                  _context = context;
            }

            public async Task<List<ComboDTO>> GetAll()
            {
                  return await _context.MunicipioModels
                        .Where(status => status.estado_municipio.Equals(Enums.StateAsset))
                        .Select(enti => new ComboDTO()
                        {
                              value = enti.id_municipio,
                              label = enti.nombre_municipio.ToUpper()
                        }).OrderBy(ord => ord.value).ToListAsync();
            }

            public async Task<List<ComboDTO>> GetAllByDepartamento(long id_departamento)
            {
                  return await _context.MunicipioModels
                        .Where(sear => sear.id_departamento == id_departamento && sear.estado_municipio.Equals(Enums.StateAsset))
                        .Select(enti => new ComboDTO()
                        {
                              value = enti.id_municipio,
                              label = enti.nombre_municipio.ToUpper()
                        }).OrderBy(ord => ord.value).ToListAsync();
            }

            public async Task<long> GetByIdMunicipio(long id_municipio)
            {
                  return await _context.MunicipioModels.Where(sear => sear.id_municipio == id_municipio)
                        .Select(x => x.id_departamento).FirstOrDefaultAsync();
            }
      }
}
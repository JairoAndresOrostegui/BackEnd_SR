using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.DataAdapter.models;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter.repositories
{
      public class TipoEspacioRepositoryAdapter : ITipoEspacioRepositoryPort
      {

            private readonly CESDE_Context _context;

            public TipoEspacioRepositoryAdapter(CESDE_Context context)
            {
                  _context = context;
            }

            public async Task DeleteTipoEspacio(long id_tipo_espacio)
            {
                  try
                  {
                        var validate = await _context.TipoEspacioModels.AnyAsync(sear => sear.id_tipo_espacio == id_tipo_espacio);
                        if (!validate)
                              throw new Exception(Enums.MessageDoesNotExist);

                        _context.TipoEspacioModels.Remove(new TipoEspacioModel() { id_tipo_espacio = id_tipo_espacio });
                        await _context.SaveChangesAsync();
                  }
                  catch (Exception)
                  {

                        throw new Exception(Enums.MessageReferenceTable);
                  }
            }

            public async Task<List<TipoEspacio>> GetAll()
            {
                  var entidades = await _context.TipoEspacioModels
                        .OrderBy(x => x.nombre_tipo_espacio)
                        .Select(enti => new TipoEspacio()
                        {
                              id_tipo_espacio = enti.id_tipo_espacio,
                              nombre_tipo_espacio = enti.nombre_tipo_espacio.UpperFirstChar(),
                              estado_tipo_espacio = enti.estado_tipo_espacio.UpperFirstChar()
                        }).ToListAsync();

                  if (entidades.Count() == 0)
                        throw new Exception(Enums.MessageNoRecord);

                  return entidades;
            }

            public async Task<List<ComboDTO>> GetAllCombo()
            {
                  return await _context.TipoEspacioModels
                        .OrderBy(x => x.nombre_tipo_espacio)
                        .Where(status => status.estado_tipo_espacio.Equals(Enums.StateAsset))
                        .Select(enti => new ComboDTO()
                        {
                              value = enti.id_tipo_espacio,
                              label = enti.nombre_tipo_espacio.ToUpper()
                        }).ToListAsync();
            }

            public async Task<TipoEspacio> GetById(long id_tipo_espacio)
            {
                  var validate = await _context.TipoEspacioModels.AnyAsync(sear => sear.id_tipo_espacio == id_tipo_espacio);
                  if (!validate)
                        throw new Exception(Enums.MessageNoRecord);

                  var entidadMap = await _context.TipoEspacioModels
                        .Where(user => user.id_tipo_espacio == id_tipo_espacio)
                        .Select(enti => new TipoEspacio()
                        {
                              id_tipo_espacio = enti.id_tipo_espacio,
                              nombre_tipo_espacio = enti.nombre_tipo_espacio.UpperFirstChar(),
                              estado_tipo_espacio = enti.estado_tipo_espacio.ToLower()
                        }).FirstOrDefaultAsync();

                  return entidadMap;
            }

            public async Task<List<TipoEspacio>> GetBySearch(string type, string search)
            {
                  search = search.ToUpper();
                  List<TipoEspacioModel> lsTipoEspacioModel = new List<TipoEspacioModel>();
                  List<TipoEspacio> lsTipoEspacio = new List<TipoEspacio>();

                  if (type == "nombre")
                        lsTipoEspacioModel = await _context.TipoEspacioModels.OrderBy(x => x.nombre_tipo_espacio)
                              .Where(sear => sear.nombre_tipo_espacio.ToUpper().Contains(search))
                              .ToListAsync();

                  if (type == "estado")
                        lsTipoEspacioModel = await _context.TipoEspacioModels.OrderBy(x => x.nombre_tipo_espacio)
                              .Where(sear => sear.estado_tipo_espacio.ToUpper().Equals(search))
                              .ToListAsync();

                  foreach (var tipoEspacio in lsTipoEspacioModel)
                  {
                        lsTipoEspacio.Add(new TipoEspacio()
                        {
                              id_tipo_espacio = tipoEspacio.id_tipo_espacio,
                              nombre_tipo_espacio = tipoEspacio.nombre_tipo_espacio.UpperFirstChar(),
                              estado_tipo_espacio = tipoEspacio.estado_tipo_espacio.UpperFirstChar()
                        });
                  }

                  if (lsTipoEspacio.Count() == 0)
                        throw new Exception(Enums.MessageNoRecord);

                  return lsTipoEspacio;
            }

            public async Task SaveTipoEspacio(TipoEspacio tipoEspacio)
            {
                  try
                  {
                        var entidadMap = new TipoEspacioModel()
                        {
                              id_tipo_espacio = tipoEspacio.id_tipo_espacio,
                              nombre_tipo_espacio = tipoEspacio.nombre_tipo_espacio.ToLower(),
                              estado_tipo_espacio = tipoEspacio.estado_tipo_espacio.ToLower()
                        };

                        _context.Add(entidadMap);
                        await _context.SaveChangesAsync();
                  }
                  catch (Exception)
                  {
                        throw new Exception(Enums.MessageErrorInsert);
                  }
            }

            public async Task UpdateTipoEspacio(TipoEspacio tipoEspacio)
            {
                  try
                  {
                        var validate = await _context.TipoEspacioModels
                        .AnyAsync(sear => sear.id_tipo_espacio == tipoEspacio.id_tipo_espacio);
                        if (!validate)
                              throw new Exception(Enums.MessageDoesNotExist);

                        var entidadMap = new TipoEspacioModel()
                        {
                              id_tipo_espacio = tipoEspacio.id_tipo_espacio,
                              nombre_tipo_espacio = tipoEspacio.nombre_tipo_espacio.ToLower(),
                              estado_tipo_espacio = tipoEspacio.estado_tipo_espacio.ToLower()
                        };

                        _context.Update(entidadMap);
                        await _context.SaveChangesAsync();
                  }
                  catch (Exception)
                  {
                        throw new Exception(Enums.MessageErrorUpdate);
                  }
            }

            public async Task<bool> ValidateName(string nombre_tipo_espacio)
            {
                  return await _context.TipoEspacioModels
                        .AnyAsync(sear => sear.nombre_tipo_espacio == nombre_tipo_espacio);
            }
      }
}
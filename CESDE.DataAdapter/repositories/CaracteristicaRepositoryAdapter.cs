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
      public class CaracteristicaRepositoryAdapter : ICaracteristicaRepositoryPort
      {

            private readonly CESDE_Context _context;

            public CaracteristicaRepositoryAdapter(CESDE_Context context)
            {
                  _context = context;
            }

            public async Task DeleteCaracteristica(long id_caracteristica)
            {
                  try
                  {
                        var validate = await _context.CaracteristicaModels.AnyAsync(sear => sear.id_caracteristica == id_caracteristica);
                        if (!validate)
                              throw new Exception(Enums.MessageDoesNotExist);

                        _context.CaracteristicaModels.Remove(new CaracteristicaModel() { id_caracteristica = id_caracteristica });
                        await _context.SaveChangesAsync();
                  }
                  catch (Exception)
                  {
                        throw new Exception(Enums.MessageReferenceTable);
                  }
            }

            public async Task<List<Caracteristica>> GetAll()
            {
                  var entidades = await _context.CaracteristicaModels.OrderBy(x => x.nombre_caracteristica)
                        .Select(enti => new Caracteristica()
                        {
                              id_caracteristica = enti.id_caracteristica,
                              nombre_caracteristica = enti.nombre_caracteristica.UpperFirstChar(),
                              estado_caracteristica = enti.estado_caracteristica.UpperFirstChar()
                        }).ToListAsync();

                  if (entidades.Count() == 0)
                        throw new Exception(Enums.MessageNoRecord);

                  return entidades;
            }

            public async Task<List<ComboDTO>> GetAllCombo()
            {
                  return await _context.CaracteristicaModels.OrderBy(x => x.nombre_caracteristica)
                        .Where(status => status.estado_caracteristica.Equals(Enums.StateAsset))
                        .Select(enti => new ComboDTO()
                        {
                              value = enti.id_caracteristica,
                              label = enti.nombre_caracteristica.ToUpper()
                        }).ToListAsync();
            }

            public async Task<Caracteristica> GetById(long id_caracteristica)
            {
                  var validate = await _context.CaracteristicaModels.AnyAsync(sear => sear.id_caracteristica == id_caracteristica);

                  if (!validate)
                        throw new Exception(Enums.MessageDoesNotExist);

                  var entidadMap = await _context.CaracteristicaModels
                        .Where(user => user.id_caracteristica == id_caracteristica)
                        .Select(enti => new Caracteristica()
                        {
                              id_caracteristica = enti.id_caracteristica,
                              nombre_caracteristica = enti.nombre_caracteristica.UpperFirstChar(),
                              estado_caracteristica = enti.estado_caracteristica.ToLower()
                        }).FirstOrDefaultAsync();

                  return entidadMap;
            }

            public async Task<List<Caracteristica>> GetBySearch(string type, string search)
            {
                  search = search.ToUpper();
                  List<CaracteristicaModel> lsCaracteristicaModel = new List<CaracteristicaModel>();
                  List<Caracteristica> lsCaracteristica = new List<Caracteristica>();

                  if (type == "nombre")
                        lsCaracteristicaModel = await _context.CaracteristicaModels.OrderBy(x => x.nombre_caracteristica)
                              .Where(sear => sear.nombre_caracteristica.ToUpper().Contains(search))
                              .ToListAsync();

                  if (type == "estado")
                        lsCaracteristicaModel = await _context.CaracteristicaModels.OrderBy(x => x.nombre_caracteristica)
                              .Where(sear => sear.estado_caracteristica.ToUpper().Equals(search))
                              .ToListAsync();

                  foreach (var caracteristica in lsCaracteristicaModel)
                  {
                        lsCaracteristica.Add(new Caracteristica()
                        {
                              id_caracteristica = caracteristica.id_caracteristica,
                              nombre_caracteristica = caracteristica.nombre_caracteristica.UpperFirstChar(),
                              estado_caracteristica = caracteristica.estado_caracteristica.UpperFirstChar()
                        });
                  }

                  if (lsCaracteristica.Count() == 0)
                        throw new Exception(Enums.MessageNoRecord);

                  return lsCaracteristica;
            }

            public async Task SaveCaracteristica(Caracteristica caracteristica)
            {
                  var entidadMap = new CaracteristicaModel()
                  {
                        id_caracteristica = caracteristica.id_caracteristica,
                        nombre_caracteristica = caracteristica.nombre_caracteristica.ToLower(),
                        estado_caracteristica = caracteristica.estado_caracteristica.ToLower()
                  };

                  _context.Add(entidadMap);
                  await _context.SaveChangesAsync();
            }

            public async Task UpdateCaracteristica(Caracteristica caracteristica)
            {
                  var validate = await _context.CaracteristicaModels
                        .AnyAsync(sear => sear.id_caracteristica == caracteristica.id_caracteristica);
                  if (!validate)
                        throw new Exception(Enums.MessageDoesNotExist);

                  var entidadMap = new CaracteristicaModel()
                  {
                        id_caracteristica = caracteristica.id_caracteristica,
                        nombre_caracteristica = caracteristica.nombre_caracteristica.ToLower(),
                        estado_caracteristica = caracteristica.estado_caracteristica.ToLower(),
                  };

                  _context.Update(entidadMap);
                  await _context.SaveChangesAsync();
            }

            public async Task<bool> ValidateName(string nombre_tipo_espacio)
            {
                  return await _context.CaracteristicaModels
                        .AnyAsync(sear => sear.nombre_caracteristica.ToUpper().Equals(nombre_tipo_espacio.ToUpper()));
            }
      }
}
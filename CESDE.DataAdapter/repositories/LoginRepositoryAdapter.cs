using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.DataAdapter.models;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.DTOForUsuario;
using CESDE.Domain.DTO.Usuario;
using CESDE.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter.repositories
{
    public class LoginRepositoryAdapter : ILoginRepositoryPort
    {

        private readonly CESDE_Context _context;

        public LoginRepositoryAdapter(CESDE_Context context)
        {
            _context = context;
        }

        public async Task<SistemaAutenticacionDTO> ValidateUser(Login login)
        {
            //var result = await ValidationLogin(login);
            UsuarioModel usuario = new UsuarioModel();
            //if (result)
            usuario = await _context.UsuarioModels.Include(rol => rol.ForKeyRol_Usuario)
                  .Where(sear => sear.login_usuario.Equals(login.login_usuario)).FirstOrDefaultAsync();

            if (usuario == null)
            {
                throw new Exception(Enums.MessageLogin);
            }
            else
            {
                if (login.clave_usuario == usuario.clave_usuario)
                {
                    var auth = await GetSistemaAutenticacion(usuario.id_usuario, usuario.ForKeyRol_Usuario.id_rol);
                    return auth;
                }
                else
                {
                    throw new Exception(Enums.MessageLogin);
                }
            }
        }

        //public async Task<bool> ValidationLogin(Login usuario)
        //{
        //    var user = await _context.UsuarioModels.Where(sear => sear.login_usuario == usuario.login_usuario)
        //          .FirstOrDefaultAsync();

        //    if (user == null)
        //        throw new Exception(Enums.MessageLogin);

        //    #region Validación de Fechas
        //    if (DateTime.Now < user.fecha_inicio_autenticacion_usuario)
        //        throw new Exception(Enums.MessageTimeStart);

        //    if (DateTime.Now >= user.fecha_fin_autenticacion_usuario)
        //        throw new Exception(Enums.MessageTimeEnd);
        //    #endregion

        //    #region Validación de Estado Usuario
        //    if (user.estado_usuario.ToUpper().Equals(Enums.StateInactive.ToUpper()))
        //        throw new Exception($"El usuario se encuentra {user.estado_usuario}");

        //    if (user.estado_usuario.ToUpper().Equals(Enums.StateBlocked.ToUpper()))
        //        throw new Exception($"El usuario se encuentra {user.estado_usuario}");

        //    if (!user.estado_usuario.ToUpper().Equals(Enums.StateAsset.ToUpper()) &&
        //          !user.estado_usuario.ToUpper().Equals(Enums.StateInactive.ToUpper()) &&
        //          !user.estado_usuario.ToUpper().Equals(Enums.StateBlocked.ToUpper()))
        //        throw new Exception(Enums.MessageErrorData);
        //    #endregion

        //    #region Validación de Estado Clave
        //    if (user.estado_clave_usuario.ToUpper().Equals(Enums.StatePassBlocked.ToUpper()))
        //        throw new Exception($"La contraseña se encuentra {user.estado_clave_usuario}");

        //    if (user.estado_clave_usuario.ToUpper().Equals(Enums.StatePassOutdated.ToUpper()))
        //        throw new Exception($"La contraseña se encuentra {user.estado_clave_usuario}");

        //    if (!user.estado_clave_usuario.ToUpper().Equals(Enums.StatePassUpdate.ToUpper()) &&
        //          !user.estado_clave_usuario.ToUpper().Equals(Enums.StatePassOutdated.ToUpper()) &&
        //          !user.estado_clave_usuario.ToUpper().Equals(Enums.StatePassBlocked.ToUpper()))
        //        throw new Exception(Enums.MessageErrorData);
        //    #endregion

        //    #region Validación de Numero de Intentos
        //    int numeroIntentosRol = await _context.RolModels.Where(sear => sear.id_rol == user.id_rol)
        //                .Select(numer => numer.maximo_autenticaciones_fallidas_rol).FirstOrDefaultAsync();

        //    if (!user.clave_usuario.Equals(usuario.clave_usuario))
        //    {
        //        user.numero_intentos_autenticacion_usuario += 1;
        //        _context.Entry(user).Property(value => value.numero_intentos_autenticacion_usuario).IsModified = true;
        //        await _context.SaveChangesAsync();

        //        if (numeroIntentosRol <= user.numero_intentos_autenticacion_usuario)
        //        {
        //            user.estado_clave_usuario = Enums.StatePassBlocked;
        //            _context.Entry(user).Property(value => value.estado_clave_usuario).IsModified = true;
        //            await _context.SaveChangesAsync();
        //        }

        //        throw new Exception(Enums.MessageLogin);
        //    }
        //    //else if (user.clave_usuario.ToUpper().Equals(usuario.clave_usuario.ToUpper()))
        //    //{
        //    //      if (numeroIntentosRol >= user.numero_intentos_autenticacion_usuario)
        //    //      {
        //    //            user.estado_clave_usuario = Enums.StatePassBlocked;
        //    //            _context.Entry(user).Property(value => value.estado_clave_usuario).IsModified = true;
        //    //            await _context.SaveChangesAsync();

        //    //            throw new Exception($"La contraseña se encuentra {user.estado_clave_usuario}");
        //    //      }
        //    //}
        //    #endregion

        //    if (user.estado_datos_usuario.ToUpper().Equals(Enums.StateDataBlocked.ToUpper()))
        //        throw new Exception($"Los datos se encuentran {user.estado_datos_usuario}");

        //    return true;
        //}

        public async Task<SistemaAutenticacionDTO> GetSistemaAutenticacion(long id_usuario, long id_rol)
        {
            List<ComponenteDTO> lsComponenteDTO = new List<ComponenteDTO>();

            var lsComponente = await (from permi in _context.PermisosRolModels
                                      join funci in _context.FuncionalidadModels
                                      on permi.id_funcionalidad equals funci.id_funcionalidad
                                      join compo in _context.ComponenteModels
                                      on funci.id_componente equals compo.id_componente
                                      where permi.id_rol == id_rol
                                      group compo by new
                                      {
                                          compo.id_componente,
                                          compo.estado_componente,
                                          compo.nombre_componente
                                      } into componente

                                      select new Componente()
                                      {
                                          id_componente = componente.Key.id_componente,
                                          estado_componente = componente.Key.estado_componente,
                                          nombre_componente = componente.Key.nombre_componente
                                      }).ToListAsync();


            foreach (var compo in lsComponente)
            {
                lsComponenteDTO.Add(new ComponenteDTO()
                {
                    nombre_componente = compo.nombre_componente,
                    funcionalidad = await GetListFuncionalidad(compo.id_componente, id_rol),
                });
            }

            var usuarioLoginDTO = await GetDataUser(id_usuario);

            var resultado = new SistemaAutenticacionDTO()
            {
                id_usuario = usuarioLoginDTO.id_usuario,
                id_rol = usuarioLoginDTO.id_rol,
                nombre_rol = usuarioLoginDTO.nombre_rol,
                id_persona = usuarioLoginDTO.id_persona,
                primer_nombre_persona = usuarioLoginDTO.primer_nombre_persona,
                primer_apellido_persona = usuarioLoginDTO.primer_apellido_persona,
                id_unidad_organizacional = usuarioLoginDTO.id_unidad_organizacional,
                nombre_unidad_organizacional = usuarioLoginDTO.nombre_unidad_organizacional,

                nivel_rol = usuarioLoginDTO.nivel_rol,
                rol_espacio = await _context.RolEspacioModels.Include(r => r.ForKeyRol_RolEspacio)
                    .Include(te => te.ForKeyTipoEspacio_RolEspacio)
                    .Where(rol => rol.id_rol == id_rol).Select(item => new ComboDTO()
                    {
                        value = item.id_tipo_espacio,
                        label = item.ForKeyTipoEspacio_RolEspacio.nombre_tipo_espacio.ToUpper()
                    }).ToListAsync(),

                unidad_rol = await _context.UnidadRolModels
                    .OrderBy(x => x.ForKeyUnidadOrgani_UnidadRol.nombre_unidad_organizacional)
                    .Include(r => r.ForKeyRol_UnidadRol)
                    .Include(te => te.ForKeyUnidadOrgani_UnidadRol).Where(rol => rol.id_rol == id_rol)
                    .Select(item => new ComboDTO()
                    {
                        value = item.id_unidad_organizacional,
                        label = item.ForKeyUnidadOrgani_UnidadRol.nombre_unidad_organizacional.ToUpper()
                    }).ToListAsync(),

                componente = lsComponenteDTO,
            };

            return resultado;
        }

        public async Task<UsuarioLoginDTO> GetDataUser(long id_usuario)
        {
            var user = await _context.UsuarioModels
                  .Include(per => per.ForKeyPersona)
                  .Include(unidad => unidad.ForKeyUnidad_Usuario)
                  .Include(rol => rol.ForKeyRol_Usuario)
                  .Where(user => user.id_usuario == id_usuario)
                  .Select(user => new UsuarioLoginDTO()
                  {
                      id_usuario = user.id_usuario,
                      login_usuario = user.login_usuario,
                      id_persona = user.id_persona,
                      primer_nombre_persona = user.ForKeyPersona.primer_nombre_persona,
                      primer_apellido_persona = user.ForKeyPersona.primer_apellido_persona,
                      id_unidad_organizacional = user.id_unidad_organizacional,
                      nombre_unidad_organizacional = user.ForKeyUnidad_Usuario.nombre_unidad_organizacional,
                      nombre_rol = user.ForKeyRol_Usuario.nombre_rol,
                      id_rol = user.ForKeyRol_Usuario.id_rol,
                      nivel_rol = user.ForKeyRol_Usuario.nivel_rol
                  }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<List<FuncionalidadDTO>> GetListFuncionalidad(long id_componente, long id_rol)
        {
            List<Funcionalidad> lsFuncionalidad = new List<Funcionalidad>();

            var lsFuncionalidadDTO = await (from permi in _context.PermisosRolModels
                                            join funci in _context.FuncionalidadModels
                                            on permi.id_funcionalidad equals funci.id_funcionalidad
                                            join compo in _context.ComponenteModels
                                            on funci.id_componente equals compo.id_componente
                                            where permi.id_rol == id_rol && compo.id_componente == id_componente
                                            select new FuncionalidadDTO()
                                            {
                                                nombre_funcionalidad = funci.nombre_funcionalidad,
                                                url_funcionalidad = funci.url_funcionalidad,
                                                permisosRol = (
                                                  from per in _context.PermisosRolModels
                                                  where per.id_funcionalidad == funci.id_funcionalidad && per.id_rol == id_rol
                                                  select new PermisosRolDTO()
                                                  {
                                                      agregar = per.agregar,
                                                      consultar = per.consultar,
                                                      eliminar = per.eliminar,
                                                      modificar = per.modificar,
                                                  }).FirstOrDefault(),
                                            }).ToListAsync();

            return lsFuncionalidadDTO;
        }

        //public async Task<string> ValidateStateDataUser(string login_usuario)
        //{
        //    var statusData = await _context.UsuarioModels.Where(sear => sear.login_usuario.Equals(login_usuario))
        //          .FirstOrDefaultAsync();

        //    if (statusData != null)
        //    {
        //        if (statusData.estado_datos_usuario.ToUpper().Equals(Enums.StateDataOutdated.ToUpper()))
        //        {
        //            statusData.numero_intentos_autenticacion_usuario = 0;
        //            _context.Entry(statusData).Property(value => value.estado_clave_usuario).IsModified = true;
        //            await _context.SaveChangesAsync();

        //            return $"Los datos se encuentran {statusData.estado_datos_usuario}";
        //        }

        //        if (!statusData.estado_datos_usuario.ToUpper().Equals(Enums.StateDataUpdate.ToUpper()) &&
        //              !statusData.estado_datos_usuario.ToUpper().Equals(Enums.StateDataOutdated.ToUpper()) &&
        //              !statusData.estado_datos_usuario.ToUpper().Equals(Enums.StateDataBlocked.ToUpper()))
        //            throw new Exception(Enums.MessageErrorData);
        //    }
        //    else throw new Exception(Enums.MessageLogin);

        //    statusData.numero_intentos_autenticacion_usuario = 0;
        //    _context.Entry(statusData).Property(value => value.estado_clave_usuario).IsModified = true;
        //    await _context.SaveChangesAsync();

        //    return Enums.MessageLoginExitoso;
        //}

        //public async Task<bool> ChangePassword(Login login)
        //{
        //    var usuario = await _context.UsuarioModels.Where(sear => sear.login_usuario.Equals(login.login_usuario))
        //          .FirstOrDefaultAsync();

        //    if (usuario != null)
        //    {
        //        usuario.clave_usuario = login.clave_usuario;
        //        usuario.estado_clave_usuario = Enums.StatePassUpdate;
        //        usuario.numero_intentos_autenticacion_usuario = 0;

        //        _context.Entry(usuario).Property(value => value.numero_intentos_autenticacion_usuario)
        //              .IsModified = true;
        //        _context.Entry(usuario).Property(value => value.clave_usuario).IsModified = true;
        //        _context.Entry(usuario).Property(value => value.estado_clave_usuario).IsModified = true;

        //        await _context.SaveChangesAsync();
        //    }
        //    else return false;

        //    return true;
        //}

        //public async Task<bool> ValidateNameUser(string login_usuario)
        //{
        //    var result = await _context.UsuarioModels
        //          .AnyAsync(sear => sear.login_usuario.Equals(login_usuario));

        //    return result;
        //}
    }
}
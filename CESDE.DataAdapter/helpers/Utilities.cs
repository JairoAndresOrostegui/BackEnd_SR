using CESDE.DataAdapter.models;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.DTO.UnidadOrganizacional;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CESDE.DataAdapter.helpers
{
    public static class Utilities
    {
        public static string UpperFirstChar(this string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            input.ToLower();

            char[] chars = input.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return new string(chars);
        }

        public static async Task<string> ObtenerUsuario(CESDE_Context _context, long id_usuario_reserva)
        {
            var user = await _context.UsuarioModels.Where(x => x.id_usuario == id_usuario_reserva)
                .Select(x => x.login_usuario).FirstAsync();

            return user;
        }

        public static async Task<List<UnidadOrganizacionalModel>> ObtenerUnidades(CESDE_Context _context, long id_tipo_espacio, long id_sede, long capacidad)
        {
            var por_tipo = await _context.UnidadOrganizacionalModels.Where(
                    x => x.id_tipo_espacio == id_tipo_espacio && x.id_unidad_organizacional_padre == id_sede && x.capacidad_unidad_organizacional >= capacidad && x.estado_unidad_organizacional == "activo"
                )
                .ToListAsync();

            return por_tipo;

        }

        public static async Task<List<UnidadOrganizacionalModel>> ObtenerUnidadesPorCaracteristica(CESDE_Context _context, long id_tipo_espacio, long id_sede, long capacidad, long id_caracteristica)
        {
            var por_caracteristica = new List<UnidadOrganizacionalModel>();
            var existe_caracteristica = _context.UnidadOrganizacionalCaracteristicaModels.Where(x => x.id_caracteristica == id_caracteristica).Any();

            por_caracteristica = await _context.UnidadOrganizacionalModels
                .Include(carac => carac.ForKeyUOC_UnidadOrgani)
                .Where(
                    x => x.id_tipo_espacio == id_tipo_espacio &&
                    x.id_unidad_organizacional_padre == id_sede &&
                    x.capacidad_unidad_organizacional >= capacidad &&
                    x.estado_unidad_organizacional == "activo" &&
                    x.ForKeyUOC_UnidadOrgani.Any(x => x.id_caracteristica == id_caracteristica)
                )
                .ToListAsync();

            return por_caracteristica;

        }

        public static async Task FiltrarFecha(CESDE_Context _context, List<ComboDTO> unidades_fijas, List<ComboReservaDTO> posibles_no, List<ReservaFechaDTO> reservas_fecha, ParametroReserva2DTO parametros)
        {
            var p_fecha_inicio = DateTime.Parse(parametros.fecha_inicio_reserva.ToString("yyyy-MM-dd"));
            var p_fecha_fin = DateTime.Parse(parametros.fecha_fin_reserva.ToString("yyyy-MM-dd"));

            foreach (var reserva in reservas_fecha)
            {
                var fecha_inicio = reserva.fecha_inicio_reserva;
                var fecha_fin = reserva.fecha_fin_reserva;

                if (
                    p_fecha_inicio >= fecha_inicio && p_fecha_inicio <= fecha_fin ||
                    p_fecha_fin >= fecha_inicio && p_fecha_fin <= fecha_fin ||
                    p_fecha_inicio < fecha_inicio && p_fecha_fin > fecha_fin
                   )
                {
                    posibles_no.Add(new ComboReservaDTO
                    {
                        id_reserva = reserva.id_reserva,
                        unidad_organizacional = reserva.unidad_organizacional
                    });
                }
            }
        }

        public static async Task FiltrarDiaHora(CESDE_Context _context, List<ComboReservaDTO> no_posibles, List<ComboReservaDTO> lista_no, ParametroReserva2DTO parametros)
        {
            var reservas_dia = new List<ReservaDiaModel>();
            foreach(var reserva in no_posibles)
            {
                var dias = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva.id_reserva)
                    .Include(x => x.ForKeyReserva_ReservaDia)
                    .ToListAsync();

                reservas_dia.AddRange(dias);
            }

            foreach(var reserva_dia in reservas_dia)
            {
                if (!lista_no.Any(x => x.id_reserva == reserva_dia.id_reserva))
                {
                    if (parametros.reserva_dia_dia.Contains(reserva_dia.reserva_dia_dia) && parametros.reserva_dia_hora_inicio.Contains(reserva_dia.reserva_dia_hora_inicio))
                    {
                        lista_no.Add(new ComboReservaDTO
                        {
                            id_reserva = reserva_dia.id_reserva,
                            unidad_organizacional = reserva_dia.ForKeyReserva_ReservaDia.id_unidad_organizacional
                        });
                    }
                }
            }
        }

        public static async Task<List<ReservaFechaDTO>> ObtenerReservas(CESDE_Context _context, List<UnidadOrganizacionalModel> unidades)
        {
            var reservas = await _context.ReservaModels
                .Include(dia => dia.ForKeyReservaDia_Reserva)
                .Where(x => unidades.Select(x => x.id_unidad_organizacional).Contains(x.id_unidad_organizacional) && x.estado_reserva == "activo")
                .Select(x => new ReservaFechaDTO
                {
                    id_reserva = x.id_reserva,
                    unidad_organizacional = x.id_unidad_organizacional,
                    fecha_inicio_reserva = x.fecha_inicio_reserva,
                    fecha_fin_reserva = x.fecha_fin_reserva
                })
                .ToListAsync();

            return reservas;
        }

        public static async Task FiltrarReservasVacias(CESDE_Context _context, List<long> id_reservas, List<ComboDTO> unidades_fijas, List<ComboDTO> posibles_no)
        {
            var reservas = await _context.ReservaModels.Where(x => id_reservas.Contains(x.id_reserva)).ToListAsync();
            
            foreach(var reserva in reservas)
            {
                var reservas_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva.id_reserva).ToListAsync();
                if (reservas_dia.Count == 0)
                {
                    var unidad = await _context.UnidadOrganizacionalModels.Where(x => x.id_unidad_organizacional == reserva.id_unidad_organizacional)
                        .Select(x => new ComboDTO
                        {
                            label = x.nombre_unidad_organizacional,
                            value = x.id_unidad_organizacional
                        }).FirstAsync();

                    unidades_fijas.Add(unidad);
                } else
                {
                    var unidad = await _context.UnidadOrganizacionalModels.Where(x => x.id_unidad_organizacional == reserva.id_unidad_organizacional)
                        .Select(x => new ComboDTO
                        {
                            label = x.nombre_unidad_organizacional,
                            value = x.id_unidad_organizacional
                        }).FirstAsync();
                    posibles_no.Add(unidad);
                }
            }
        }
    }
}
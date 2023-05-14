using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.DataAdapter.helpers
{
    internal class InformesHelper
    {
        public static void ResolverJornadas(string jornada_actual, int reserva_dias_contador, int[] jornadas)
        {
            string[] jornadas_enums = { Enums.jornada1, Enums.jornada2, Enums.jornada3, Enums.jornada4, Enums.jornada5 };

            for (int i = 0; i < jornadas.Length; i++)
            {
                foreach(string jornada_enum in jornadas_enums)
                {
                    if (jornada_enum == jornada_actual)
                        jornadas[i] = reserva_dias_contador;
                }
            }
        }
    }
}

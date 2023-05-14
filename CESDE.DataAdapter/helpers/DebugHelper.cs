using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.DataAdapter.helpers
{
    internal class DebugHelper
    {
        public static void Log(string content)
        {
            string filePath = "log.txt";

            using (StreamWriter sw = new (filePath))
            {
                sw.Write(content);
            }
        }
    }
}

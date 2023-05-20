using System.IO;

namespace CESDE.DataAdapter.helpers
{
    internal class DebugHelper
    {
        public static void Log(string content)
        {
            string filePath = "log.txt";

            using (StreamWriter sw = new(filePath))
            {
                sw.Write(content);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sliv
{
    public class Levels
    {
        public string Path { get; private set; }
        public string FilePath { get; private set; }

        public Levels(string path, string fileName)
        {
            Path = path;
            FilePath = path + "/" + fileName;
        }
        public void CreateLog()
        {
            Directory.CreateDirectory(Path);
            using (File.Create(FilePath)) ;
        }

        public void UpdateLog(string data)
        {
            string logData = data;
            File.WriteAllText(FilePath, logData);
        }

        public int ReadLog()
        {
            int res;
            if (File.ReadAllText(FilePath) != string.Empty)
                res = Convert.ToInt32(File.ReadAllText(FilePath));
            else
                res = 0;
            return res;
        }

        public void ClearLog()
        {
            string logData = string.Empty;
            File.WriteAllText(FilePath, logData);
        }
    }
}

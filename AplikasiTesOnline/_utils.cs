using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace AplikasiTesOnline
{
    class _utils
    {
        
        public void IniFile()
        {
            try
            {

                string name = "Configuration.ini";
                if (!File.Exists(Path.Combine(Application.StartupPath, name)))
                {
                    FileStream fs = new FileStream(Path.Combine(Application.StartupPath, name), FileMode.Create, FileAccess.Write, FileShare.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("[Database]");
                    sw.WriteLine("host=localhost");
                    sw.WriteLine("user=root");
                    sw.WriteLine("pass=");
                    sw.WriteLine("schema=tes_online");
                    sw.Flush();
                    fs.Close();
                }

            } catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            
        }

        public bool Logging(string logString, string logFilename)
        {
            FileStream fs = new FileStream(Path.Combine(Application.StartupPath, logFilename), FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(logString);
            sw.Flush();
            fs.Close();

            return true;
        }
    }
}

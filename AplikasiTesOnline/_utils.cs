using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace AplikasiTesOnline
{
    class _utils
    {
        FileIniDataParser parser = new FileIniDataParser();
        
        public void get()
        {
            IniData data = parser.ReadFile("Configuration.ini");
            //return data["Database"];
        }
    }
}

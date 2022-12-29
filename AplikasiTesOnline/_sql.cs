using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using IniParser;
using IniParser.Model;
using System.Data;

namespace AplikasiTesOnline
{
    class _sql
    {
        //string connection ;

        public string conn()
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Configuration.ini");

            return "server="+data["Database"]["host"]+";database=" + data["Database"]["schema"] + ";uid=" + data["Database"]["user"] + ";pwd=" + data["Database"]["pass"] + ";";
        }
        
        public DataTable getData(string query)
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection connection = new MySqlConnection(conn());
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                data.Load(reader);
                return data;

            } catch (MySqlException e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}

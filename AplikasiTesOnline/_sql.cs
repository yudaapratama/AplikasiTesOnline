using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using IniParser;
using IniParser.Model;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace AplikasiTesOnline
{
    class _sql
    {
        _utils utilities = new _utils();

        public string Conn()
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Configuration.ini");

            return "server="+data["Database"]["host"]+";database=" + data["Database"]["schema"] + ";uid=" + data["Database"]["user"] + ";pwd=" + data["Database"]["pass"] + ";";
        }
        
        public DataTable GetData(string query)
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection connection = new MySqlConnection(Conn());
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                data.Load(reader);

                utilities.Logging("GET DATA FROM MYSQL SERVER : " + query + Environment.NewLine, "log-" + DateTime.Now.ToString("yyyyMMdd"));

                return data;

            } catch (MySqlException e)
            {
                utilities.Logging("ERROR GET DATA FROM MYSQL SERVER : " + query + "[ "+ e.ToString() +" ]" + Environment.NewLine, "log-" + DateTime.Now.ToString("yyyyMMdd"));
                //throw new Exception(e.ToString());
                return data;
            }
        }

        public bool ExecuteData(string query)
        {
            bool flag = false;
            try
            {
                MySqlConnection connection = new MySqlConnection(Conn());
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                flag = true;

                utilities.Logging("EXECUTE QUERY FROM MYSQL SERVER : " + query + Environment.NewLine, "log-" + DateTime.Now.ToString("yyyyMMdd"));

                return flag;

            } catch (MySqlException e)
            {
                utilities.Logging("ERROR EXECUTE QUERY FROM MYSQL SERVER : " + query + "[ " + e.ToString() + " ]" + Environment.NewLine, "log-" + DateTime.Now.ToString("yyyyMMdd"));

                return flag;
            }
        }

        public bool CheckServer()
        {
            bool flag = false;
            try
            {
                MySqlConnection connection = new MySqlConnection(Conn());
                connection.Open();
                flag = true;
                return flag;
            } catch (MySqlException e)
            {
                return flag;
                //throw new Exception(e.ToString());
            }
        }
    }
}

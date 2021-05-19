using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace FCT
{
    class CRUD
    {

        private static string getConnectionString()
        {

            string host = "server=localhost;";
            string port = "port=3307;";
            string db = "Database=proy3tex;";
            string user = "user=root;";
            string pass = "password=";

            string conString = string.Format("{0}{1}{2}{3}{4}", host, port, db, user, pass);

            return conString;

        }

        public static MySqlConnection con = new MySqlConnection(getConnectionString());
        public static MySqlCommand cmd = default(MySqlCommand);
        public static string sql = string.Empty;
        public static DataSet dsEmpresa = new DataSet();
        public static DataSet dsAlumno = new DataSet();
        public static DataSet dsTutor = new DataSet();
        public static MySqlDataAdapter adaptador = new MySqlDataAdapter();

    }
}


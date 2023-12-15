using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoC_.Data
{
    public class Conexao
    {
        private string connectionString;
        private MySqlConnection connection;
        private string server = "localhost";
        private string user = "root";
        private string password = "12345678";
        private string database = "poo_project";


        public Conexao()
        {
            connectionString = $"Server={server};Database={database};User={user};Password={password};";
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection Abrir()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            return connection;
        }

        public void Fechar()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

    }
}

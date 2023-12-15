using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoC_.Data
{
    public class ClienteData
    {
        private Conexao conexao;

        public ClienteData(Conexao conexao)
        {
            this.conexao = conexao;
        }

        public List<Cliente> ConsultaTodos()
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                List <Cliente> clientes = new List<Cliente>();
                string query = "SELECT * FROM tb_cliente;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                             clientes.Add(new Cliente(Convert.ToInt32(reader["idTB_CLIENTE"]), reader["NOME_CLIENTE"].ToString()!, reader["CPF_CLIENTE"].ToString()!, reader["EMAIL_CLIENTE"].ToString()!, reader["ENDERECO_CLIENTE"].ToString()!));
                        }
                        return clientes;
                    }
                }
            }
        }
        public void AdicionarCliente(Cliente cliente)
            
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query = "INSERT INTO `poo_project`.`TB_CLIENTE` (`NOME_CLIENTE`, `CPF_CLIENTE`, `EMAIL_CLIENTE`, `ENDERECO_CLIENTE`)" +
                $"VALUES('{cliente.Nome}', '{cliente.CPF}', '{cliente.Email}', '{cliente.Endereco}');";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                        command.ExecuteNonQuery();
                    
                }
            }
        }
        public void DeletarCliente(int clienteId)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query = $"DELETE FROM `poo_project`.`TB_CLIENTE` WHERE `idTB_CLIENTE` = {clienteId};";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AlterarCliente(Cliente cliente)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query = $"UPDATE `poo_project`.`TB_CLIENTE` SET `NOME_CLIENTE` = '{cliente.Nome}', `CPF_CLIENTE` = '{cliente.CPF}', " +
                               $"`EMAIL_CLIENTE` = '{cliente.Email}', `ENDERECO_CLIENTE` = '{cliente.Endereco}' WHERE `idTB_CLIENTE` = {cliente.Id};";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
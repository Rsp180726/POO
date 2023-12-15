using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ProjetoC_.Data
{
    public class FuncionarioData
    {
        private Conexao conexao;

        public FuncionarioData(Conexao conexao)
        {
            this.conexao = conexao;
        }

        public List<Funcionario> ConsultaTodos()
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                List<Funcionario> funcionarios = new List<Funcionario>();
                string query = "SELECT * FROM tb_funcionario;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            funcionarios.Add(new Funcionario
                            {
                                Nome = reader["NOME_FUNCIONARIO"].ToString()!,
                                Id = Convert.ToInt32(reader["idTB_FUNCIONARIO"])
                            });
                        }
                        return funcionarios;
                    }
                }
            }
        }

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query = $"INSERT INTO `poo_project`.`TB_FUNCIONARIO` (`NOME_FUNCIONARIO`) VALUES ('{funcionario.Nome}');";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletarFuncionario(int funcionarioId)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query = $"DELETE FROM `poo_project`.`TB_FUNCIONARIO` WHERE `idTB_FUNCIONARIO` = {funcionarioId};";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

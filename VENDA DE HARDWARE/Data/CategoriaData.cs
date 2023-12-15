using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ProjetoC_.Data
{
    public class CategoriaData
    {
        private Conexao conexao;

        public CategoriaData(Conexao conexao)
        {
            this.conexao = conexao;
        }

        public List<Categoria> ConsultaTodos()
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                List<Categoria> categorias = new List<Categoria>();
                string query = "SELECT * FROM tb_categoria;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorias.Add(new Categoria
                            {
                                Nome = reader["NOME_CAT"].ToString()!,
                                Descricao = reader["DESCRICAO_CAT"].ToString()!,
                                Id = Convert.ToInt32(reader["idTB_CATEGORIA"])
                            });
                        }
                        return categorias;
                    }
                }
            }
        }

        public void AdicionarCategoria(Categoria categoria)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query = $"INSERT INTO poo_project.TB_CATEGORIA (NOME_CAT, DESCRICAO_CAT) VALUES ('{categoria.Nome}', '{categoria.Descricao}');";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

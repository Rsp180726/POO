using MySql.Data.MySqlClient;
using ProjetoC_.Models;
using System;
using System.Collections.Generic;

namespace ProjetoC_.Data
{
    public class ProdutoData
    {
        private Conexao conexao;

        public ProdutoData(Conexao conexao)
        {
            this.conexao = conexao;
        }

        public List<Produto> ConsultaTodos()
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                List<Produto> produtos = new List<Produto>();
                string query = "SELECT * FROM tb_produto INNER JOIN TB_CATEGORIA ON tb_produto.TB_CATEGORIA_idTB_CATEGORIA = TB_CATEGORIA.idTB_CATEGORIA;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {   

                            produtos.Add(new Produto
                            {
                                Id = Convert.ToInt32(reader["idTB_PRODUTO"]),
                                Nome = reader["NOME_PRODUTO"].ToString()!,
                                Categoria = new Categoria
                                {
                                    Nome = reader["NOME_CAT"].ToString()!,
                                    Descricao = reader["DESCRICAO_CAT"].ToString()!,
                                    Id = Convert.ToInt32(reader["idTB_CATEGORIA"])
                                },
                                Preco = Convert.ToDouble(reader["PRECO_PRODUTO"]),
                                Estoque = Convert.ToInt32(reader["ESTOQUE_PRODUTO"])
                            });
                        }
                        return produtos;
                    }
                }
            }
        }

        public List<Produto> ConsultaPorCategoria(Categoria categoria)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                List<Produto> produtos = new List<Produto>();
                string query = $"SELECT * FROM tb_produto INNER JOIN TB_CATEGORIA ON tb_produto.TB_CATEGORIA_idTB_CATEGORIA = TB_CATEGORIA.idTB_CATEGORIA WHERE TB_CATEGORIA_idTB_CATEGORIA = {categoria.Id};";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produtos.Add(new Produto
                            {
                                Id = Convert.ToInt32(reader["idTB_PRODUTO"]),
                                Nome = reader["NOME_PRODUTO"].ToString()!,
                                Categoria = new Categoria
                                {
                                    Nome = reader["NOME_CAT"].ToString()!,
                                    Descricao = reader["DESCRICAO_CAT"].ToString()!,
                                    Id = Convert.ToInt32(reader["idTB_CATEGORIA"])
                                },
                                Preco = Convert.ToDouble(reader["PRECO_PRODUTO"]),
                                Estoque = Convert.ToInt32(reader["ESTOQUE_PRODUTO"])
                            });
                        }
                        return produtos;
                    }
                }
            }
        }

        public void AdicionarProduto(Produto produto)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query = $"INSERT INTO `poo_project`.`TB_PRODUTO` (`NOME_PRODUTO`, `PRECO_PRODUTO`, `ESTOQUE_PRODUTO`, `TB_CATEGORIA_idTB_CATEGORIA`) " +
                               $"VALUES ('{produto.Nome}', {produto.Preco}, {produto.Estoque}, '{produto.Categoria.Id}');";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletarProduto(int produtoId)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query = $"DELETE FROM `poo_project`.`TB_PRODUTO` WHERE `idTB_PRODUTO` = {produtoId};";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

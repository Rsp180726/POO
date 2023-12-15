using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProjetoC_.Data
{
    public class VendaProdutoData
    {
        private Conexao conexao;

        public VendaProdutoData(Conexao conexao)
        {
            this.conexao = conexao;
        }

        public List<Produto> ConsultaPorVenda(int idVenda)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                List<Produto> produtos = new List<Produto>();
                string query = $"SELECT * FROM tb_venda_has_tb_produto INNER JOIN TB_PRODUTO ON tb_venda_has_tb_produto.TB_PRODUTO_idTB_PRODUTO = TB_PRODUTO.idTB_PRODUTO WHERE tb_venda_has_tb_produto.TB_VENDA_idTB_VENDA = {idVenda};";
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
    }
}
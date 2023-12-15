using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ProjetoC_.Data
{
    public class VendaData
    {
        private Conexao conexao;

        public VendaData(Conexao conexao)
        {
            this.conexao = conexao;
        }

        public List<Venda> ConsultaTodas()
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                List<Venda> vendas = new List<Venda>();
                string query = @"
                    SELECT
                        v.idTB_VENDA,
                        v.DATA_VENDA,
                        v.QUANTIDADE,
                        v.VALORTOTAL_VENDA,
                        p.NOME_PRODUTO,
                        p.idTB_Produto,
                        ca.idTB_CATEGORIA,
                        ca.NOME_CAT,
                        ca.DESCRICAO_CAT,
                        p.ESTOQUE_PRODUTO,
                        p.PRECO_PRODUTO

                    FROM
                        TB_VENDA v
                    INNER JOIN
                        TB_PRODUTO p ON v.TB_PRODUTO_idTB_PRODUTO = p.idTB_PRODUTO
                    INNER JOIN
                        TB_CATEGORIA ca ON p.TB_CATEGORIA_idTB_CATEGORIA = ca.idTB_CATEGORIA;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vendas.Add(new Venda
                            {
                                Id = Convert.ToInt32(reader["idTB_VENDA"]),
                                Data = Convert.ToDateTime(reader["DATA_VENDA"]),
                                produto = new Produto
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
                                },
                                Quantidade = Convert.ToInt32(reader["QUANTIDADE"]),
                                ValorTotal = Convert.ToDouble(reader["VALORTOTAL_VENDA"])
                            });
                        }
                        return vendas;
                    }
                }
            }
        }

        public void AdicionarVenda(Venda venda)
        {
            using (MySqlConnection connection = conexao.Abrir())
            {
                string query;
                if (venda.cliente == null)
                {
                    query = $"INSERT INTO `poo_project`.`TB_VENDA` (`DATA_VENDA`, `TB_PRODUTO_idTB_PRODUTO`, `QUANTIDADE`, `VALORTOTAL_VENDA`, `TB_FUNCIONARIO_idTB_FUNCIONARIO`) " +
                               $"VALUES ('{venda.Data.ToString("yyyy-MM-dd HH:mm:ss")}', {venda.produto.Id}, {venda.Quantidade}, {venda.ValorTotal}, {venda.funcionario!.Id});";
                }
                else if (venda.funcionario == null)
                {
                    query = $"INSERT INTO `poo_project`.`TB_VENDA` (`DATA_VENDA`, `TB_PRODUTO_idTB_PRODUTO`, `QUANTIDADE`, `VALORTOTAL_VENDA`, `TB_CLIENTE_idTB_CLIENTE`) " +
                               $"VALUES ('{venda.Data.ToString("yyyy-MM-dd HH:mm:ss")}', {venda.produto.Id}, {venda.Quantidade}, {venda.ValorTotal}, {venda.cliente!.Id});";

                }
                else
                {
                    query = $"INSERT INTO `poo_project`.`TB_VENDA` (`DATA_VENDA`,`TB_PRODUTO_idTB_PRODUTO`, `QUANTIDADE`, `VALORTOTAL_VENDA`) " +
                               $"VALUES ('{venda.Data.ToString("yyyy-MM-dd HH:mm:ss")}', {venda.produto.Id}, {venda.Quantidade}, {venda.ValorTotal});";
                }
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}


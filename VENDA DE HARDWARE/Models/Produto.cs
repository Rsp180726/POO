using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoC_
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public double Preco { get; set; }
        public int Estoque { get; set; }

        public static implicit operator List<object>(Produto v)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoC_
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public double ValorTotal { get; set; }
        public int Quantidade { get; set; }
        public Produto produto { get; set; }
        public Cliente? cliente { get; set; }
        public Funcionario? funcionario { get; set; }
    }
}

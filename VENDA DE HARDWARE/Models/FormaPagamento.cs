using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoC_.Models
{
    public class FormaPagamento
    {
        public string? Tipo { get; set; }

        public void ProcessarPagamento(double valorTotal)
        {
            Console.WriteLine($"Compra realizada com sucesso!\nTotal: R${valorTotal:F2}\nPagamento: {Tipo}");
        }
    }
}
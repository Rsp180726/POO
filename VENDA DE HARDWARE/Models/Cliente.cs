using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoC_;

namespace ProjetoC_
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public int Id { get; set; }

        public Cliente()
        {
        }
        public Cliente(int id,string nome, string CPF, string email, string endereco)
        {
            this.Nome = nome;
            this.CPF = CPF;
            this.Email = email;
            this.Endereco = endereco;
            this.Id = id;
        }
    }
    
}

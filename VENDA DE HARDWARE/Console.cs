using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjetoC_
{
    public class FrontConsole
    {
        public static void MostrarMenuCliente()
        {
            Console.WriteLine("\n===== Menu Cliente =====");
            Console.WriteLine("1. Escolher Categoria");
            Console.WriteLine("2. Realizar Compra");
            Console.WriteLine("3. Mostrar Clientes Registrados");
            Console.WriteLine("4. Alterar Cliente");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoC_; 
using MySql.Data.MySqlClient;
using ProjetoC_.Data;
using System.Linq.Expressions;
using ProjetoC_.Models;

class Program
{
    static Conexao conexao = new Conexao();
    static ClienteData clienteData = new ClienteData(conexao);
    static FuncionarioData funcionarioData = new FuncionarioData(conexao);
    static ProdutoData produtoData = new ProdutoData(conexao);
    static CategoriaData categoriaData = new CategoriaData(conexao);

    static VendaData vendaData = new VendaData(conexao);
    static void Main()
    { 
        Cliente? cliente;
        Console.WriteLine("Bem-vindo ao sistema de vendas de Hardware!");

        Console.Write("Você é um Cliente (C) ou Funcionário (F)?\n"); // Usuario escolhe Cliente ou Funcionário
        char tipoUsuario = Console.ReadKey().KeyChar;

        if (char.ToUpper(tipoUsuario) == 'C')
        {
            Console.WriteLine("\nVocê já está Cadastrado? (S/N)");
            char isCadastrado= Console.ReadKey().KeyChar;
            if (char.ToUpper(isCadastrado) == 'S')
            {
                cliente = ListarClientes();
                if (cliente == null){
                    return;
                }
            }
            else
            {
                cliente = AdicionarCliente();
                try
                {
                    clienteData.AdicionarCliente(cliente);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Processing failed: {e.Message}");
                }
                Console.WriteLine($"Bem-vindo, {cliente.Nome}!");
            }
            while (true)
            {
                MostrarMenuCliente();
                char opcao = Console.ReadKey().KeyChar;

                switch (opcao)
                {
                    case '1':
                        EscolherCategoria();
                        break;
                    case '2':
                        ListarTodosProdutos();
                        break;
                    case '3':
                        RealizarCompra(cliente);
                        break;
                    case '4':
                        cliente = AlterarCliente(cliente);
                        break;
                    case '5':
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }


        }
        else if (char.ToUpper(tipoUsuario) == 'F')
        {
            Funcionario? funcionario;
            Console.WriteLine("\nVocê já está Cadastrado? (S/N)");
            char isCadastrado= Console.ReadKey().KeyChar;

            if (char.ToUpper(isCadastrado) == 'S')
            {
                funcionario = ListarFuncionarios();
                if (funcionario == null){
                    return;
                }
            }
            else
            {
                funcionario = AdicionarFuncionario();
                try
                {
                    funcionarioData.AdicionarFuncionario(funcionario);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Processing failed: {e.Message}");
                }
                Console.WriteLine($"Bem-vindo, {funcionario.Nome}!");
            }
            while (true)
            {
                MostrarMenuFuncionario();
                char opcao = Console.ReadKey().KeyChar;

                switch (opcao)
                {
                    case '1':
                        EscolherCategoria();
                        break;
                    case '2':
                        AdicionarProduto();
                        break;
                    case '3':
                        RealizarVendaFuncionario(funcionario);
                        break;
                    case '4':
                        MostrarRelatorioVendas();
                        break;
                    case '5':
                        MostrarClientesRegistrados();
                        break;
                    case '6':
                        MostrarFuncionariosRegistrados();
                        break;
                    case '7':
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Tipo de usuário inválido. Encerrando o programa.");
        }
    }

    static Cliente? ListarClientes()
    {
        List<Cliente> clientes =  clienteData.ConsultaTodos();
        for (int i = 0; i < clientes.Count; i++)
        {
            Console.WriteLine($"\n{i+1} - {clientes[i].Nome}");
        }
        bool opcaoInvalida = true;
        while(opcaoInvalida){
            string opcaoCliente = Console.ReadLine()!;
            if(int.TryParse(opcaoCliente, out int intOpcao))
            {   
                if (intOpcao == 0){
                    return null;
                } else if (intOpcao>0 && intOpcao<= clientes.Count){
                    opcaoInvalida = false;
                    return clientes[intOpcao-1];
                }
                
            }
            Console.WriteLine("\nOpção inválida");
            
        }
        return null;
    }

    static Funcionario? ListarFuncionarios()
    {
        List<Funcionario> funcionarios =  funcionarioData.ConsultaTodos();
        for (int i = 0; i < funcionarios.Count; i++)
        {
            Console.WriteLine($"\n{i+1} - {funcionarios[i].Nome}");
        }
        Console.WriteLine($"\n0 - Sair");
        bool opcaoInvalida = true;
        while(opcaoInvalida){
            string opcaoFuncionario = Console.ReadLine()!;
            if(int.TryParse(opcaoFuncionario, out int intOpcao))
            {   
                if(intOpcao == 0){
                    return null;
                }
                opcaoInvalida = false;
                return funcionarios[intOpcao-1];
            }
            else{
                Console.WriteLine("\nOpção inválida");
            }
        }
        return null;
    }
    static Cliente AdicionarCliente()
    {
        Cliente cliente = new Cliente();
        Console.Write("\nDigite seu nome: ");
        cliente.Nome = Console.ReadLine()!;

        Console.Write("Digite seu CPF: ");
        cliente.CPF = Console.ReadLine()!;

        Console.Write("Digite seu email: ");
        cliente.Email = Console.ReadLine()!;

        Console.Write("Digite seu endereço: ");
        cliente.Endereco = Console.ReadLine()!;
        Console.WriteLine("Cliente cadastrado com sucesso!");

        return cliente; 
    }

    static Cliente AlterarCliente(Cliente cliente)
    {
        Cliente clienteNovo = new Cliente();
        bool alterou = false;

        ListarUmCliente(cliente);
        Console.WriteLine("Deixe em branco para manter os dados!"); 

        Console.Write("Digite o novo nome do cliente: ");
        clienteNovo.Nome = Console.ReadLine()!;
        
        if(clienteNovo.Nome != null){
            cliente.Nome = clienteNovo.Nome;
            alterou = true;
        }

        Console.Write("Digite o novo CPF do cliente: ");
        clienteNovo.CPF = Console.ReadLine()!;

        if(clienteNovo.CPF != null){
            cliente.CPF = clienteNovo.CPF;
            alterou = true;
        }

        Console.Write("Digite o novo e-mail do cliente: ");
        clienteNovo.Email = Console.ReadLine()!;

        if(clienteNovo.Email != null){
            cliente.Email = clienteNovo.Email;
            alterou = true;
        }

        Console.Write("Digite o novo endereço do cliente: ");
        clienteNovo.Endereco = Console.ReadLine()!;

        if(clienteNovo.Endereco != null){
            cliente.Endereco = clienteNovo.Endereco;
            alterou = true;
        }
        if(alterou){
            clienteData.AlterarCliente(cliente);
            Console.WriteLine($"Cliente {cliente.Nome} alterado com sucesso!");
        }else{
            Console.WriteLine("Não houve alterações no cliente");
        }

        return cliente;
    }

    static void ListarUmCliente(Cliente cliente){
        Console.WriteLine($"Nome atual: {cliente.Nome}");

        Console.WriteLine($"CPF atual: {cliente.CPF}");

        Console.WriteLine($"Email atual: {cliente.Email}");

        Console.WriteLine($"Endereço atual: {cliente.Endereco}");
    }

    static Funcionario AdicionarFuncionario()
    {
        Funcionario funcionario = new Funcionario();
        Console.Write("Digite o nome do funcionário: ");
        funcionario.Nome = Console.ReadLine()!;

        Console.Write("Digite o ID do funcionário: ");
        string Id=Console.ReadLine()!;
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            funcionario.Id = id;
        }
        Console.WriteLine("Funcionário cadastrado com sucesso!");

        return funcionario; 
    }

    static void MostrarClientesRegistrados()
    {
        Console.WriteLine("\n===== Clientes Registrados =====");
        List<Cliente> clientes = clienteData.ConsultaTodos();
        foreach (Cliente c in clientes)
        {
            Console.WriteLine($"Nome: {c.Nome}, CPF: {c.CPF}, Email: {c.Email}, Endereço: {c.Endereco}");
        }
        Console.WriteLine("===============================");
    }

    static void MostrarFuncionariosRegistrados()
    {
        Console.WriteLine("\n===== Funcionários Registrados =====");
        List<Funcionario> funcionarios = funcionarioData.ConsultaTodos();
        foreach (Funcionario f in funcionarios)
        {
            Console.WriteLine($"Nome: {f.Nome}, ID: {f.Id}");
        }
        Console.WriteLine("===============================");
    }
    static void MostrarMenuCliente()
    {
        Console.WriteLine("\n===== Menu Cliente =====");
        Console.WriteLine("1. Listar por Categoria");
        Console.WriteLine("2. Listar Todos os Produtos");
        Console.WriteLine("3. Realizar Compra");
        Console.WriteLine("4. Alterar Seus Dados");
        Console.WriteLine("5. Sair");
        Console.Write("Escolha uma opção: ");
    }
    static void MostrarMenuFuncionario()
    {
        Console.WriteLine("\n===== Menu Funcionário =====");
        Console.WriteLine("1. Escolher Categoria");
        Console.WriteLine("2. Adicionar Produto");
        Console.WriteLine("3. Realizar Venda");
        Console.WriteLine("4. Mostrar Relatório de Vendas");
        Console.WriteLine("5. Mostrar Clientes Registrados");
        Console.WriteLine("6. Mostrar Funcionários Registrados");
        Console.WriteLine("7. Sair");
        Console.Write("Escolha uma opção: ");
    }

    
    static Produto? EscolherCategoria()
    {
        Console.WriteLine("\n===== Escolher Categoria =====\n");
        Categoria categoria = ListarCategorias()!;
        List<Produto> produtos = produtoData.ConsultaPorCategoria(categoria);
        return EscolherProduto(produtos);

    }

    static Categoria? ListarCategorias(){
        Console.WriteLine("Categorias disponíveis:\n");
        List<Categoria> categorias = categoriaData.ConsultaTodos();
        for (int i = 0; i < categorias.Count; i++)
        {
            Console.WriteLine($"{i+1} - {categorias[i].Nome}");
        }
        bool opcaoInvalida = true; 
        Console.WriteLine($"\n0 - Sair");

        while(opcaoInvalida){
            string opcaoCategoria = Console.ReadLine()!;
            if(int.TryParse(opcaoCategoria, out int intOpcao))
            {   
                if(intOpcao == 0){
                    return null;
                }
                if(intOpcao >=1 && intOpcao<=categorias.Count){
                    opcaoInvalida = false;
                    return categorias[intOpcao-1];
                }
                
            }
            Console.WriteLine("\nOpção inválida, tente novamente...");
        }
        return null;
    }

    static Produto? EscolherProduto(List<Produto> produtos)
    {
        for (int i = 0; i < produtos.Count; i++)
        {
            Console.WriteLine($"{i+1} - Nome: {produtos[i].Nome} - Preço: R${produtos[i].Preco:F2} - Estoque: {produtos[i].Estoque}");
        }
        Console.WriteLine($"0 - Sair");
        bool opcaoInvalida = true;
        while(opcaoInvalida){
            string opcaoProduto = Console.ReadLine()!;
            if(int.TryParse(opcaoProduto, out int intOpcao))
            {   
                if(intOpcao == 0){
                    return null;
                }
                if(intOpcao >=1 && intOpcao<=produtos.Count){
                    opcaoInvalida = false;
                    return produtos[intOpcao-1];
                }
                
            }
            Console.WriteLine("\nOpção inválida, tente novamente...");
        }
        return null;
    }

    static void ListarTodosProdutos(){
        try
        {
            List<Produto> produtos = produtoData.ConsultaTodos();
            for (int i = 0; i < produtos.Count; i++)
            {
                Console.WriteLine($"{i+1} - Nome: {produtos[i].Nome} - Preço: R${produtos[i].Preco:F2} - Estoque: {produtos[i].Estoque}");
            }
            Console.WriteLine("===================\n");
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.ToString());
        }
    }


    static void AdicionarProduto()
    {
        Console.Write("Digite o nome do produto: ");
        string nome = Console.ReadLine()!;
        
        Console.Write("Digite a categoria do produto: ");
        Categoria? categoria = ListarCategorias();
        

        Console.Write("Digite o preço do produto: ");
        if (!double.TryParse(Console.ReadLine(), out double preco))
        {
            Console.WriteLine("Valor inválido para o preço.");
            return;
        }

        Console.Write("Digite a quantidade em estoque: ");
        if (!int.TryParse(Console.ReadLine(), out int estoque))
        {
            Console.WriteLine("Valor inválido para o estoque.");
            return;
        }

        Produto novoProduto = new Produto { Nome = nome, Categoria = categoria!, Preco = preco, Estoque = estoque };
        produtoData.AdicionarProduto(novoProduto);

        Console.WriteLine($"Produto '{nome}' adicionado ao estoque.\n");
    }

    static void RealizarCompra(Cliente cliente)
    {
        Venda venda = new Venda();

        Produto? produto = EscolherCategoria();

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }
        venda.produto = produto;
        Console.Write($"Digite a quantidade de '{produto.Nome}' que deseja comprar: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade))
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }
        if (quantidade > produto.Estoque)
        {
            Console.WriteLine("Quantidade em estoque insuficiente.");
            return;
        }

        venda.Quantidade = quantidade;
        venda.ValorTotal = quantidade * produto.Preco;
        venda.cliente = cliente;

        Console.WriteLine($"Total: R${venda.ValorTotal:F2}");

        RealizarPagamento(venda.ValorTotal);

        vendaData.AdicionarVenda(venda);
    }

    static void RealizarPagamento(double valorTotal) 
    {
        Console.WriteLine("Escolha o método de pagamento:");
        Console.WriteLine("1. Dinheiro (Cash)");
        Console.WriteLine("2. Cartão de Crédito (Card)");
        Console.WriteLine("3. Pix");

        string metodoPagamento = Console.ReadLine()!;
        FormaPagamento formaPagamento = new FormaPagamento();

        switch (metodoPagamento)
        {
            case "1":
                Console.WriteLine($"\nCompra realizada com sucesso!\nTotal: R${valorTotal:F2}\nPagamento: Dinheiro");
                break;
            case "2":
                Console.WriteLine($"\nCompra realizada com sucesso!\nTotal: R${valorTotal:F2}\nPagamento: Cartão de Crédito");
                break;
            case "3":
                Console.WriteLine($"\nCompra realizada com sucesso!\nTotal: R${valorTotal:F2}\nPagamento: Pix");
                break;
            default:
                Console.WriteLine("Método de pagamento inválido.");
                break;
        }
        formaPagamento.ProcessarPagamento(valorTotal);

    }

    static void RealizarVendaFuncionario(Funcionario funcionario)
    {
        Produto? produto = EscolherCategoria();

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        Console.Write($"Digite a quantidade de '{produto.Nome}' que deseja vender: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade))
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        if (quantidade > produto.Estoque)
        {
            Console.WriteLine("Quantidade em estoque insuficiente.");
            return;
        }

        double valorTotal = quantidade * produto.Preco;
        Console.WriteLine($"Total: R${valorTotal:F2}");

        RealizarPagamento(valorTotal);

        vendaData.AdicionarVenda(new Venda { produto = produto, Quantidade = quantidade, ValorTotal = valorTotal, funcionario = funcionario});
    }


    static void MostrarRelatorioVendas()
    {
        Console.WriteLine("\n===== Relatório de Vendas ====="); 
        List<Venda> vendas = vendaData.ConsultaTodas();
        foreach (Venda venda in vendas)
        {
            Console.WriteLine($"Data: {venda.Data}, Produto: {venda.produto.Nome}, Quantidade: {venda.Quantidade}, Valor Total: R${venda.ValorTotal:F2}");
        }
        Console.WriteLine("=============================\n");
    }
   
}
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<(int Codigo, string Nome, int Quantidade, double Preco)> estoque = new List<(int, string, int, double)>();

    static void Main()
    {
        while (true)
        {
            MostrarMenu();
            string opcao = Console.ReadLine();

            try
            {
                switch (opcao)
                {
                    case "1":
                        CadastrarProduto();
                        break;
                    case "2":
                        ConsultarProduto();
                        break;
                    case "3":
                        AtualizarEstoque();
                        break;
                    case "4":
                        GerarRelatorios();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n=== Sistema de Gerenciamento de Estoque ===");
        Console.WriteLine("1. Cadastrar Produto");
        Console.WriteLine("2. Consultar Produto por Código");
        Console.WriteLine("3. Atualizar Estoque");
        Console.WriteLine("4. Gerar Relatórios");
        Console.WriteLine("5. Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void CadastrarProduto()
    {
        Console.WriteLine("\n=== Cadastrar Produto ===");
        Console.Write("Código: ");
        int codigo = int.Parse(Console.ReadLine());

        if (estoque.Any(p => p.Codigo == codigo))
        {
            throw new InvalidOperationException("Produto com código já cadastrado.");
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());
        Console.Write("Preço: ");
        double preco = double.Parse(Console.ReadLine());

        estoque.Add((codigo, nome, quantidade, preco));
        Console.WriteLine("Produto cadastrado com sucesso!");
    }

    static void ConsultarProduto()
    {
        Console.WriteLine("\n=== Consultar Produto por Código ===");
        Console.Write("Digite o código do produto: ");
        int codigo = int.Parse(Console.ReadLine());

        var produto = estoque.FirstOrDefault(p => p.Codigo == codigo);

        if (produto.Equals(default((int, string, int, double))))
        {
            throw new ProdutoNaoEncontradoException("Produto não encontrado.");
        }

        Console.WriteLine($"Código: {produto.Codigo}, Nome: {produto.Nome}, Quantidade: {produto.Quantidade}, Preço: {produto.Preco}");
    }

    static void AtualizarEstoque()
    {
        Console.WriteLine("\n=== Atualizar Estoque ===");
        Console.Write("Digite o código do produto: ");
        int codigo = int.Parse(Console.ReadLine());

        var produto = estoque.FirstOrDefault(p => p.Codigo == codigo);

        if (produto.Equals(default((int, string, int, double))))
        {
            throw new ProdutoNaoEncontradoException("Produto não encontrado.");
        }

        Console.Write("Digite a quantidade a ser adicionada (negativa para saída): ");
        int quantidade = int.Parse(Console.ReadLine());

        if (produto.Quantidade + quantidade < 0)
        {
            throw new InvalidOperationException("Quantidade insuficiente para saída.");
        }

        produto.Quantidade += quantidade;
        Console.WriteLine("Estoque atualizado com sucesso!");
    }

    static void GerarRelatorios()
    {
        Console.WriteLine("\n=== Gerar Relatórios ===");

        Console.Write("Informe o limite de quantidade para o relatório 1: ");
        int limiteQuantidade = int.Parse(Console.ReadLine());

        var relatorio1 = estoque.Where(p => p.Quantidade < limiteQuantidade);
        Console.WriteLine("\nRelatório 1 - Produtos com quantidade em estoque abaixo do limite:");
        ImprimirRelatorio(relatorio1);

        Console.Write("\nInforme o valor mínimo para o relatório 2: ");
        double valorMinimo = double.Parse(Console.ReadLine());
        Console.Write("Informe o valor máximo para o relatório 2: ");
        double valorMaximo = double.Parse(Console.ReadLine());

        var relatorio2 = estoque.Where(p => p.Preco >= valorMinimo && p.Preco <= valorMaximo);
        Console.WriteLine("\nRelatório 2 - Produtos com valor entre o mínimo e o máximo:");
        ImprimirRelatorio(relatorio2);

        Console.WriteLine("\nRelatório 3 - Valor total do estoque e valor total de cada produto:");
        var valorTotalEstoque = estoque.Sum(p => p.Quantidade * p.Preco);
        Console.WriteLine($"Valor total do estoque: {valorTotalEstoque}");

        ImprimirRelatorio(estoque);
    }

    static void ImprimirRelatorio(IEnumerable<(int Codigo, string Nome, int Quantidade, double Preco)> relatorio)
    {
        foreach (var produto in relatorio)
        {
            Console.WriteLine($"Código: {produto.Codigo}, Nome: {produto.Nome}, Quantidade: {produto.Quantidade}, Preço: {produto.Preco}");
        }
    }
}

class ProdutoNaoEncontradoException : Exception
{
    public ProdutoNaoEncontradoException(string message) : base(message) { }
}

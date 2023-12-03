using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Tarefa> listaTarefas = new List<Tarefa>();

    static void Main()
    {
        while (true)
        {
            MostrarMenu();
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CriarTarefa();
                    break;
                case "2":
                    ListarTarefas();
                    break;
                case "3":
                    ListarTarefasConcluidas();
                    break;
                case "4":
                    ListarTarefasPendentes();
                    break;
                case "5":
                    MarcarTarefaConcluida();
                    break;
                case "6":
                    ExcluirTarefa();
                    break;
                case "7":
                    PesquisarTarefa();
                    break;
                case "8":
                    MostrarEstatisticas();
                    break;
                case "9":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n=== Sistema de Gerenciamento de Tarefas ===");
        Console.WriteLine("1. Criar Tarefa");
        Console.WriteLine("2. Listar Todas as Tarefas");
        Console.WriteLine("3. Listar Tarefas Concluídas");
        Console.WriteLine("4. Listar Tarefas Pendentes");
        Console.WriteLine("5. Marcar Tarefa como Concluída");
        Console.WriteLine("6. Excluir Tarefa");
        Console.WriteLine("7. Pesquisar Tarefa por Palavra-chave");
        Console.WriteLine("8. Mostrar Estatísticas");
        Console.WriteLine("9. Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void CriarTarefa()
    {
        Console.WriteLine("\n=== Criar Tarefa ===");
        Console.Write("Título: ");
        string titulo = Console.ReadLine();
        Console.Write("Descrição: ");
        string descricao = Console.ReadLine();
        Console.Write("Data de Vencimento (dd/MM/yyyy): ");
        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataVencimento))
        {
            Tarefa novaTarefa = new Tarefa(titulo, descricao, dataVencimento);
            listaTarefas.Add(novaTarefa);
            Console.WriteLine("Tarefa criada com sucesso!");
        }
        else
        {
            Console.WriteLine("Formato de data inválido. Tarefa não criada.");
        }
    }

    static void ListarTarefas()
    {
        Console.WriteLine("\n=== Lista de Todas as Tarefas ===");
        foreach (var tarefa in listaTarefas)
        {
            Console.WriteLine(tarefa);
        }
    }

    static void ListarTarefasConcluidas()
    {
        Console.WriteLine("\n=== Lista de Tarefas Concluídas ===");
        foreach (var tarefa in listaTarefas.Where(t => t.Concluida))
        {
            Console.WriteLine(tarefa);
        }
    }

    static void ListarTarefasPendentes()
    {
        Console.WriteLine("\n=== Lista de Tarefas Pendentes ===");
        foreach (var tarefa in listaTarefas.Where(t => !t.Concluida))
        {
            Console.WriteLine(tarefa);
        }
    }

    static void MarcarTarefaConcluida()
    {
        Console.WriteLine("\n=== Marcar Tarefa como Concluída ===");
        Console.Write("Digite o ID da tarefa: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Tarefa tarefa = listaTarefas.FirstOrDefault(t => t.Id == id);

            if (tarefa != null)
            {
                tarefa.Concluir();
                Console.WriteLine("Tarefa marcada como concluída!");
            }
            else
            {
                Console.WriteLine("ID de tarefa não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

    static void ExcluirTarefa()
    {
        Console.WriteLine("\n=== Excluir Tarefa ===");
        Console.Write("Digite o ID da tarefa: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Tarefa tarefa = listaTarefas.FirstOrDefault(t => t.Id == id);

            if (tarefa != null)
            {
                listaTarefas.Remove(tarefa);
                Console.WriteLine("Tarefa excluída com sucesso!");
            }
            else
            {
                Console.WriteLine("ID de tarefa não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

    static void PesquisarTarefa()
    {
        Console.WriteLine("\n=== Pesquisar Tarefa por Palavra-chave ===");
        Console.Write("Digite a palavra-chave: ");
        string palavraChave = Console.ReadLine();

        var tarefasEncontradas = listaTarefas.Where(t => t.Titulo.Contains(palavraChave, StringComparison.OrdinalIgnoreCase) || t.Descricao.Contains(palavraChave, StringComparison.OrdinalIgnoreCase));

        if (tarefasEncontradas.Any())
        {
            Console.WriteLine("\nTarefas encontradas:");
            foreach (var tarefa in tarefasEncontradas)
            {
                Console.WriteLine(tarefa);
            }
        }
        else
        {
            Console.WriteLine("Nenhuma tarefa encontrada com a palavra-chave informada.");
        }
    }

    static void MostrarEstatisticas()
    {
        Console.WriteLine("\n=== Estatísticas ===");
        Console.WriteLine($"Número total de tarefas: {listaTarefas.Count}");
        Console.WriteLine($"Número de tarefas concluídas: {listaTarefas.Count(t => t.Concluida)}");
        Console.WriteLine($"Número de tarefas pendentes: {listaTarefas.Count(t => !t.Concluida)}");

        var tarefaMaisAntiga = listaTarefas.OrderBy(t => t.DataVencimento).FirstOrDefault();
        var tarefaMaisRecente = listaTarefas.OrderByDescending(t => t.DataVencimento).FirstOrDefault();

        Console.WriteLine($"Tarefa mais antiga: {tarefaMaisAntiga?.DataVencimento.ToString("dd/MM/yyyy") ?? "Nenhuma tarefa"}");
        Console.WriteLine($"Tarefa mais recente: {tarefaMaisRecente?.DataVencimento.ToString("dd/MM/yyyy") ?? "Nenhuma tarefa"}");
    }
}

class Tarefa
{
    private static int proximoId = 1;

    public int Id { get; }
    public string Titulo { get; }
    public string Descricao { get; }
    public DateTime DataVencimento { get; }
    public bool Concluida { get; private set; }

    public Tarefa(string titulo, string descricao, DateTime dataVencimento)
    {
        Id = proximoId++;
        Titulo = titulo;
        Descricao = descricao;
        DataVencimento = dataVencimento;
        Concluida = false;
    }

    public void Concluir()
    {
        Concluida = true;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Título: {Titulo}, Descrição: {Descricao}, Data de Vencimento: {DataVencimento.ToString("dd/MM/yyyy")}, Concluída: {Concluida}";
    }
}

using System;
using System.Collections.Generic;

class Program
{
    static List<Task> tasks = new List<Task>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("======= Sistema de Gerenciamento de Tarefas =======");
            Console.WriteLine("1. Criar Tarefa");
            Console.WriteLine("2. Listar Todas as Tarefas");
            Console.WriteLine("3. Marcar Tarefa como Concluída");
            Console.WriteLine("4. Listar Tarefas Pendentes");
            Console.WriteLine("5. Listar Tarefas Concluídas");
            Console.WriteLine("6. Excluir Tarefa");
            Console.WriteLine("7. Pesquisar Tarefa por Palavra-Chave");
            Console.WriteLine("8. Exibir Estatísticas");
            Console.WriteLine("9. Sair");

            Console.Write("Escolha uma opção: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateTask();
                    break;
                case "2":
                    ListAllTasks();
                    break;
                case "3":
                    MarkTaskAsCompleted();
                    break;
                case "4":
                    ListPendingTasks();
                    break;
                case "5":
                    ListCompletedTasks();
                    break;
                case "6":
                    DeleteTask();
                    break;
                case "7":
                    SearchTasks();
                    break;
                case "8":
                    DisplayStatistics();
                    break;
                case "9":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void CreateTask()
    {
        Console.Write("Digite o título da tarefa: ");
        string title = Console.ReadLine();

        Console.Write("Digite a descrição da tarefa: ");
        string description = Console.ReadLine();

        Console.Write("Digite a data de vencimento (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
        {
            tasks.Add(new Task(title, description, dueDate));
            Console.WriteLine("Tarefa criada com sucesso!");
        }
        else
        {
            Console.WriteLine("Formato de data inválido. Tarefa não criada.");
        }
    }

    static void ListAllTasks()
    {
        Console.WriteLine("======= Lista de Todas as Tarefas =======");
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    static void MarkTaskAsCompleted()
    {
        Console.Write("Digite o número da tarefa a ser marcada como concluída: ");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
        {
            tasks[taskNumber - 1].MarkAsCompleted();
            Console.WriteLine("Tarefa marcada como concluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Número de tarefa inválido. Tarefa não marcada como concluída.");
        }
    }

    static void ListPendingTasks()
    {
        Console.WriteLine("======= Lista de Tarefas Pendentes =======");
        foreach (var task in tasks)
        {
            if (!task.IsCompleted)
            {
                Console.WriteLine(task);
            }
        }
    }

    static void ListCompletedTasks()
    {
        Console.WriteLine("======= Lista de Tarefas Concluídas =======");
        foreach (var task in tasks)
        {
            if (task.IsCompleted)
            {
                Console.WriteLine(task);
            }
        }
    }

    static void DeleteTask()
    {
        Console.Write("Digite o número da tarefa a ser excluída: ");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
        {
            tasks.RemoveAt(taskNumber - 1);
            Console.WriteLine("Tarefa excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Número de tarefa inválido. Tarefa não excluída.");
        }
    }

    static void SearchTasks()
    {
        Console.Write("Digite uma palavra-chave para a pesquisa: ");
        string keyword = Console.ReadLine();

        Console.WriteLine("======= Resultados da Pesquisa =======");
        foreach (var task in tasks)
        {
            if (task.Title.Contains(keyword) || task.Description.Contains(keyword))
            {
                Console.WriteLine(task);
            }
        }
    }

    static void DisplayStatistics()
    {
        int totalTasks = tasks.Count;
        int completedTasks = tasks.Count(t => t.IsCompleted);
        int pendingTasks = totalTasks - completedTasks;

        DateTime? oldestTask = tasks.Min(t => t.DueDate);
        DateTime? newestTask = tasks.Max(t => t.DueDate);

        Console.WriteLine("======= Estatísticas =======");
        Console.WriteLine($"Total de Tarefas: {totalTasks}");
        Console.WriteLine($"Tarefas Concluídas: {completedTasks}");
        Console.WriteLine($"Tarefas Pendentes: {pendingTasks}");
        Console.WriteLine($"Tarefa Mais Antiga: {oldestTask?.ToString("yyyy-MM-dd") ?? "N/A"}");
        Console.WriteLine($"Tarefa Mais Recente: {newestTask?.ToString("yyyy-MM-dd") ?? "N/A"}");
    }
}

class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; private set; }

    public Task(string title, string description, DateTime dueDate)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

    public override string ToString()
    {
        return $"Título: {Title}\nDescrição: {Description}\nData de Vencimento: {DueDate.ToString("yyyy-MM-dd")}\nStatus: {(IsCompleted ? "Concluída" : "Pendente")}\n";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

class Program {
    static void Main() {
        List<Advogado> advogados = new List<Advogado>();
        List<Cliente> clientes = new List<Cliente>();

        try {
            advogados.Add(new Advogado("Adv1", new DateTime(1990, 1, 1), "08419437530", "CNA1", advogados));
            advogados.Add(new Advogado("Adv2", new DateTime(1998, 7, 7), "12345678912", "CNA2", advogados));

            clientes.Add(new Cliente("Cli1", new DateTime(1999, 9, 9), "08419437531", "Solteiro", "Prof1"));
            clientes.Add(new Cliente("Cli2", new DateTime(1998, 5, 5), "08419437532", "Casado", "Prof2"));
        }
        catch (ArgumentException ex) {
            Console.WriteLine($"Erro ao adicionar pessoa: {ex.Message}");
        }

        Console.WriteLine("Advogados com idade entre 30 e 40 anos : ");
        var advogadosRelatorio = Relatorios.AdvogadosEntreIdades(advogados, 30, 40);

        foreach (var advogado in advogadosRelatorio) {
            Console.WriteLine(advogado.Nome);
        }

        Console.WriteLine("\n  Clientes  com idade entre 25 e 35 anos : ");
        var clientesRelatorio = Relatorios.ClientesEntreIdades(clientes, 25, 35);

        foreach (var cliente in clientesRelatorio) {
            Console.WriteLine(cliente.Nome);
        }

        Console.WriteLine("\n Clientes Casados : ");
        var clientesCasados = Relatorios.ClientePorEstadoCivil(clientes, "Casado");
        foreach (var cliente in clientesCasados) {
            Console.WriteLine(cliente.Nome);
        }

        Console.WriteLine("\n Clientes em ordem alfabética : ");
        var clientesOrdenados = Relatorios.ClienteEmOrdemAlfabetica(clientes);
        foreach (var cliente in clientesOrdenados) {
            Console.WriteLine(cliente.Nome);
        }

        Console.WriteLine("\n Clientes com profissão contendo 'Prof' : ");
        var ClientePorProfissao = Relatorios.ClientePorProfissao(clientes, "Prof");
        foreach (var cliente in ClientePorProfissao) {
            Console.WriteLine(cliente.Nome);
        }

        Console.WriteLine("\n Advogados e Clientes aniversariantes em Setembro");
        var aniversariantesSetembro = Relatorios.AniversarianteDoMes(advogados, clientes, 9);
        foreach (var pessoa in aniversariantesSetembro) {
            Console.WriteLine(pessoa.Nome);
        }
    }
}

class Pessoa {
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    public Pessoa(string nome, DateTime dataNascimento, string cpf) {
        Nome = nome;
        DataNascimento = dataNascimento;
        SetCpf(cpf);
    }

    private void SetCpf(string cpf) {
        if (cpf.Length != 11 || !cpf.All(char.IsDigit)) {
            throw new ArgumentException("CPF INVÁLIDO");
        }
        CPF = cpf;
    }
}

class Advogado : Pessoa {
    public string CNA { get; set; }

    public Advogado(string nome, DateTime dataNascimento, string cpf, string cna, List<Advogado> advogados) : base(nome, dataNascimento, cpf) {
        if (AdvogadoCnaJaExiste(cna, advogados)) {
            throw new ArgumentException("CNA já existe");
        }
        CNA = cna;
    }

    private static bool AdvogadoCnaJaExiste(string cna, List<Advogado> advogados) {
        // Verificar se o CNA já existe na lista de advogados
        return advogados.Any(a => a.CNA == cna);
    }
}

class Cliente : Pessoa {
    public string EstadoCivil { get; set; }
    public string Profissao { get; set; }

    public Cliente(string nome, DateTime dataNascimento, string cpf, string estadoCivil, string profissao) : base(nome, dataNascimento, cpf) {
        EstadoCivil = estadoCivil;
        Profissao = profissao;
    }
}

class Relatorios {
    public static IEnumerable<Advogado> AdvogadosEntreIdades(List<Advogado> advogados, int idadeMin, int idadeMax) {
        var dataAtual = DateTime.Now;
        return advogados.Where(a => dataAtual.Year - a.DataNascimento.Year >= idadeMin && dataAtual.Year - a.DataNascimento.Year <= idadeMax);
    }

    public static IEnumerable<Cliente> ClientesEntreIdades(List<Cliente> clientes, int idadeMin, int idadeMax) {
        var dataAtual = DateTime.Now;
        return clientes.Where(c => dataAtual.Year - c.DataNascimento.Year >= idadeMin && dataAtual.Year - c.DataNascimento.Year <= idadeMax);
    }

    public static IEnumerable<Cliente> ClientePorEstadoCivil(List<Cliente> clientes, string estadoCivil) {
        return clientes.Where(c => c.EstadoCivil.Equals(estadoCivil, StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<Cliente> ClienteEmOrdemAlfabetica(List<Cliente> clientes) {
        return clientes.OrderBy(c => c.Nome);
    }

    public static IEnumerable<Cliente> ClientePorProfissao(List<Cliente> clientes, string textoProfissao) {
        return clientes.Where(c => c.Profissao.Contains(textoProfissao, StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<Pessoa> AniversarianteDoMes(List<Advogado> advogados, List<Cliente> clientes, int mes) {
        return advogados.Cast<Pessoa>().Concat(clientes.Cast<Pessoa>()).Where(p => p.DataNascimento.Month == mes).OrderBy(p => p.DataNascimento.Day);
    }
}
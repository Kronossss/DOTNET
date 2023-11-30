using System;
using System.Collections.Generic;
using System.Linq;
class Program {
    static void Main() {
        List<Advogado> advogados = new List<Advogado>();

        List<Cliente> clientes= new List<Cliente>();

        try {

            advogados.Add(new Advogado("Adv1", new DateTime (1980, 1, 1),"08419437530","CNA1"));
            advogados.Add(new Advogado("Adv2", new DateTime (1998, 7, 7),"12345678912","CNA2"));
            

            clientes.Add(new Cliente("Cli1",new DateTime (1999, 9, 9),"08419437531","Solteiro","Prof1"));
            clientes.Add(new Cliente("Cli2",new DateTime (1998, 5, 5),"08419437532","Casado","Prof2"));
        }
        catch (ArgumentException ex){
            Console.WriteLine($"Erro ao adcionar pessoa ou advogado  :  {ex.Message}");
        }

    }
}

class Pessoa
{
    public string Nome {get; set;}
    public DateTime DataNascimento {get; set;}
    public string CPF {get; set;}

    public Pessoa(string nome, DateTime dataNascimento, string cpf)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;

    }
    private void SetCpf(string cpf){
        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
        {
            throw new ArgumentException("CPF INVÁLIDO");
        }
        CPF = cpf;              
    }
}

class Advogado : Pessoa 
{
    public string CNA {get; set;}

    public Advogado (string nome, DateTime dataNascimento, string cpf, string cna) : base(nome, dataNascimento,cpf)
    {
        CNA = cna;
    }
}

class Cliente : Pessoa
{
    public string EstadoCivil {get; set;}
    public string Profissao {get; set;}
    
    public Cliente(string   nome, DateTime dataNascimento, string cpf, string estadoCivil, string profissao) : base(nome, dataNascimento,cpf)
    {
        EstadoCivil = estadoCivil;
        Profissao = profissao;
    }      
}

class Relatorios
{
    public static IEnumerable<Advogado> AdvogadosEntreIdades(List<Advogado>advogados, int idadeMin, int idadeMax)
    {
        var dataAtual =  DateTime.Now;
        return advogados.Where(a => dataAtual.Year - a.DataNascimento.Year >= idadeMin && dataAtual.Year -a.DataNascimento.Year <= idadeMax);

    }
    public static IEnumerable<Cliente> ClientesEntreIdades(List<Cliente>clientes, int idadeMin, int idadeMax)
    {
        var dataAtual =  DateTime.Now;
        return clientes.Where(c => dataAtual.Year - c.DataNascimento.Year >= idadeMin && dataAtual.Year -c.DataNascimento.Year <= idadeMax);
    }

    public static IEnumerable<Cliente> ClientePorEstadoCivil(List<Cliente>clientes, string estadoCivil)
    {
        return clientes.Where(c => c.EstadoCivil.Equals(estadoCivil,StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<Cliente> ClienteEmOrdemAlfabetica(List<Cliente>clientes)
    {
        return clientes.OrderBy(c => c.Nome);
    }

}
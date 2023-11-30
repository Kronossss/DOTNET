﻿using System;
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

    public Pessoa(string nome, DateTime dataNascimento, string cpf){
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;

    }
    private void SetCpf(string cpf){
        if (cpf.Length != 11 || !cpf.All(char.IsDigit)){
            throw new ArgumentException("CPF INVÁLIDO");
        }
        CPF = cpf;              
    }
}

class Advogado : Pessoa 
{
    public string CNA {get; set;}

    public Advogado (string nome, DateTime dataNascimento, string cpf, string cna) : base(nome, dataNascimento,cpf){
        CNA = cna;
    }
}
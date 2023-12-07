using System;
using System.Collections.Generic;
using System.Linq;

class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    public Pessoa(string _nome, DateTime _dataNascimento, string _CPF)
    {
        this.Nome = _nome;
        this.DataNascimento = _dataNascimento;
        this.CPF = _CPF;
    }
}

class Advogado : Pessoa
{
    public string CNA { get; set; }

    public Advogado(string _nome, DateTime _dataNascimento, string _CPF, string _CNA) : base(_nome, _dataNascimento, _CPF)
    {
        this.CNA = _CNA;
    }
}

class Cliente : Pessoa
{
    public string EstadoCivil { get; set; }
    public string Profissao { get; set; }

    public Cliente(string _nome, DateTime _dataNascimento, string _CPF, string _estadoCivil, string _profissao) : base(_nome, _dataNascimento, _CPF)
    {
        this.EstadoCivil = _estadoCivil;
        this.Profissao = _profissao;
    }
}

class Documento
{
    public DateTime DataHoraModificacao { get; set; }
    public int Codigo { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }

    public Documento(DateTime _dataHoraModificacao, int _codigo, string _tipo, string _descricao)
    {
        this.DataHoraModificacao = _dataHoraModificacao;
        this.Codigo = _codigo;
        this.Tipo = _tipo;
        this.Descricao = _descricao;
    }
}

class CasoJuridico
{
    public DateTime Abertura { get; set; }
    public float ProbabilidadeSucesso { get; set; }
    public List<Documento> Documentos { get; set; }
    public List<(float, string)> Custos { get; set; }
    public DateTime? Encerramento { get; set; }
    public List<Advogado> Advogados { get; set; }
    public Cliente? Cliente { get; set; }
    public string Status { get; set; }

    public CasoJuridico()
    {
        Documentos = new List<Documento>();
        Custos = new List<(float, string)>();
        Advogados = new List<Advogado>();
        Status = "Em aberto";
    }
}

class Escritorio
{
    public List<Advogado> Advogados { get; set; }
    public List<Cliente> Clientes { get; set; }
    public List<CasoJuridico> CasosJuridicos { get; set; }

    public Escritorio()
    {
        Advogados = new List<Advogado>();
        Clientes = new List<Cliente>();
        CasosJuridicos = new List<CasoJuridico>();
    }

    public void IniciarCaso(CasoJuridico caso)
    {
        caso.Abertura = DateTime.Now;
        caso.Status = "Em aberto";
        CasosJuridicos.Add(caso);
    }

    public void AtualizarCaso(CasoJuridico caso)
    {
        if (caso.Status == "Concluído" && caso.Encerramento <= caso.Abertura)
        {
            throw new InvalidOperationException("A data de conclusão deve ser posterior à data de início.");
        }
        else
        {
            caso.Status = "Concluído";
            caso.Encerramento = DateTime.Now;
        }
    }

    // Relatórios
    public List<Advogado> AdvogadosEntreIdades(int idadeMinima, int idadeMaxima)
    {
        return Advogados.Where(a => (DateTime.Now.Year - a.DataNascimento.Year) >= idadeMinima && (DateTime.Now.Year - a.DataNascimento.Year) <= idadeMaxima).ToList();
    }

    public List<Cliente> ClientesEntreIdades(int idadeMinima, int idadeMaxima)
    {
        return Clientes.Where(c => (DateTime.Now.Year - c.DataNascimento.Year) >= idadeMinima && (DateTime.Now.Year - c.DataNascimento.Year) <= idadeMaxima).ToList();
    }

    public List<Cliente> ClientesPorEstadoCivil(string estadoCivil)
    {
        return Clientes.Where(c => c.EstadoCivil.Equals(estadoCivil, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Cliente> ClientesEmOrdemAlfabetica()
    {
        return Clientes.OrderBy(c => c.Nome).ToList();
    }

    public List<Advogado> AdvogadosPorProfissao(string profissao)
    {
        return Advogados.Where(a => a.Profissao.Equals(profissao, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}

class Program
{
    static void Main()
    {
        Escritorio escritorio = new Escritorio();

        // Inicialização de dados (apenas exemplo)
        Advogado advogado = new Advogado(_nome: "Advogado1", _dataNascimento: new DateTime(1980, 1, 1), _CPF: "12345678901", _CNA: "123456789");
        escritorio.Advogados.Add(advogado);

        Cliente cliente = new Cliente(_nome: "Cliente1", _dataNascimento: new DateTime(1980, 1, 1), _CPF: "12345678901", _estadoCivil: "Solteiro", _profissao: "Advogado");
        escritorio.Clientes.Add(cliente);

        CasoJuridico caso = new CasoJuridico { Cliente = cliente, Advogados = new List<Advogado> { advogado } };
        escritorio.IniciarCaso(caso);

        // Teste dos relatórios (apenas exemplo)
        List<Advogado> advogadosEntreIdades = escritorio.AdvogadosEntreIdades(30, 50);
        List<Cliente> clientesEntreIdades = escritorio.ClientesEntreIdades(25, 35);
        List<Cliente> clientesPorEstadoCivil = escritorio.ClientesPorEstadoCivil("Solteiro");
        List<Cliente> clientesEmOrdemAlfabetica = escritorio.ClientesEmOrdemAlfabetica();

        int opcao = 0;
        while (opcao == 0)
        {
            Console.WriteLine("Selecione uma opção: ");
            Console.WriteLine("1 - Advogados entre 30 e 50 anos");
            Console.WriteLine("2 - Clientes entre 25 e 35 anos");
            Console.WriteLine("3 - Clientes por estado civil");
            Console.WriteLine("4 - Clientes em ordem alfabetica");
            Console.WriteLine("5 - Sair");
            string input = Console.ReadLine();
            if (input != null)
            {
                opcao = int.Parse(input);
            }
            else
            {
                throw new Exception("Entrada inválida");
            }    
                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Advogados entre 30 e 50 anos:");
                        foreach (var adv in advogadosEntreIdades)
                        {
                            Console.WriteLine(adv.Nome + " - " + adv.CPF + " - " + adv.CNA); ;
                        }
                        break;
                    case 2:
                        Console.WriteLine("Clientes entre 25 e 35 anos:");
                        foreach (var cli in clientesEntreIdades)
                        {
                            Console.WriteLine(cli.Nome + " - " + cli.CPF + " - " + cli.DataNascimento);
                        }
                        break;
                    case 3:
                        Console.WriteLine("Clientes por estado civil:");
                        foreach (var cli in clientesPorEstadoCivil)
                        {
                            Console.WriteLine(cli.Nome + " - " + cli.CPF + " - " + cli.DataNascimento);
                        }
                        break;
                }
            }
        }
    }
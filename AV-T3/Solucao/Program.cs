#region Avaliamento-t3
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Globalization;

class Pessoa
{
    public string Nome { get; protected set; }
    public DateTime DataNascimento{ get; protected set;}
    public string CPF { get; protected set; }
    public int Idade { get; protected set; }

    public Pessoa(string _nome, int _idade, string _dataNascimento, string _CPF)
    {   
        this.Nome = _nome;
        this.Idade = _idade;
        this.DataNascimento = DateTime.ParseExact(_dataNascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        if(_CPF.Length == 11){this.CPF = _CPF;} else {throw new ArgumentException("CPF inválido");}
    }

    public void imprime()
    {
        Console.WriteLine($"{this.Nome} - {this.Idade}");
    }

    public void setNome(string _nome)
    {
        if (_nome.Length > 0)
        {
            this.Nome = _nome;
        }
        else
        {
            throw new ArgumentException("Nome inválido");
        }
    }

    public void setIdade(int _idade)
    {
        if (_idade < 0)
        {
            this.Idade = _idade;
            return;
        }
        else
        {
            throw new ArgumentException("Idade inválida");
        }
    }

    public void setDataNascimento(DateTime _dataNascimento)
    {
        if (_dataNascimento < DateTime.Now)
        {
            this.DataNascimento = _dataNascimento;
        }
        else
        {
            throw new ArgumentException("Data inválida");
        }
    }

    protected void setCPF(string _CPF)
    {
        if (_CPF.Length == 11 && _CPF.All(c => char.IsDigit(c)))
        {
            this.CPF = _CPF;
        }
        else
        {
            throw new ArgumentException("CPF inválido");
        }
    }
}

class Advogado : Pessoa
{
    public string CNA;

    public Advogado(string _nome, int _idade, string _dataNascimento, string _CPF, string _CNA) : base(_nome, _idade, _dataNascimento, _CPF)
    {
        this.CNA = _CNA;
        if(_CPF.Length == 11) this.CPF = _CPF;
    }

    public string getCNA()
    {
        return this.CNA;
    }

    protected void setCNA(string _CNA)
    {
        if (_CNA.Length > 0)
        {
            this.CNA = _CNA;
        }
        else
        {
            throw new ArgumentException("CNA inválido");
        }
    }

}

class Cliente : Pessoa
{
    public string _estadoCivil { get; protected set; }
    public string _profissao { get; protected set; }

    public Cliente(string _nome, int _idade, string _dataNascimento, string _CPF, string _estadoCivil, string _profissao) : base(_nome, _idade, _dataNascimento, _CPF)
    {
        this._estadoCivil = _estadoCivil;
        this._profissao = _profissao;
        if(_CPF.Length == 11){this.CPF = _CPF;} else {throw new ArgumentException("CPF inválido");}
    }

    public string getEstadoCivil()
    {
        return this._estadoCivil;
    }

    public void setEstadoCivil(string _estadoCivil)
    {
        if (_estadoCivil.Length > 0)
        {
            this._estadoCivil = _estadoCivil;
        }
        else
        {
            throw new ArgumentException("Estado civil inválido");
        }
    }

    public string getProfissao()
    {
        return this._profissao;
    }

    public void setProfissao(string _profissao)
    {
        if (_profissao.Length > 0)
        {
            this._profissao = _profissao;
        }
        else
        {
            throw new ArgumentException("Profissão inválida");
        }
    }
}

class ListaAdvogados
{
    public List<Advogado> listaAdvogados = new List<Advogado>();
    public void AdicionarAdvogado(Advogado advogado)
    {
        if (listaAdvogados.Any((c => c.CPF == advogado.CPF)) || listaAdvogados.Any((c => c.CNA == advogado.CNA)))
        {
            throw new Exception("Dados de advogado já cadastrados");
        }
        this.listaAdvogados.Add(advogado);
    }

    public List<Advogado> ObterAdvogados()
    {
        return listaAdvogados;
    }

    public List<Advogado> AdvogadosPorIdades(int _idade1, int _idade2)
    {
        List<Advogado> advogadosFilterIdade = new List<Advogado>();
        foreach (Advogado advogado in this.listaAdvogados)
        {
            if (advogado.Idade >= _idade1 && advogado.Idade <= _idade2)
            {
                advogadosFilterIdade.Add(advogado);
            }
        }
        foreach (var item in advogadosFilterIdade)
        {
            Console.WriteLine($"Nome {item.Nome} - Idade {item.Idade}");
        }
        return advogadosFilterIdade;
    }
}

class ListaClientes
{
    public List<Cliente> listaClientes = new List<Cliente>();
    public void AdicionarCliente(Cliente cliente)
    {
        if (listaClientes.Any((c => c.CPF == cliente.CPF)))
        {
            throw new Exception("Dados de cliente já cadastrados");
        }
        this.listaClientes.Add(cliente);
    }

    public void ClientesPorIdades(int _idade1, int _idade2)
    {
        List<Cliente> clientesFilterIdade = new List<Cliente>();
        foreach (Cliente cliente in this.listaClientes)
        {
            if (cliente.Idade >= _idade1 && cliente.Idade <= _idade2)
            {
                clientesFilterIdade.Add(cliente);
            }
        }
        foreach (var item in clientesFilterIdade)
        {
            Console.WriteLine($"Nome {item.Nome} - Idade {item.Idade}");
        }
    }

    public void ClientesPorEstadoCivil(string _estadoCivil)
    {
        List<Cliente> clientesFilterEstadoCivil = new List<Cliente>();
        foreach (Cliente cliente in this.listaClientes)
        {
            if (cliente._estadoCivil == _estadoCivil)
            {
                clientesFilterEstadoCivil.Add(cliente);
            }
        }
        foreach (var item in clientesFilterEstadoCivil)
        {
            Console.WriteLine($"Nome {item.Nome} - Estado civil {item._estadoCivil}");
        }
    }

    public void ClientesPorProfissao(string _profissao)
    {
        List<Cliente> clientesFilterProfissao = new List<Cliente>();
        foreach (Cliente cliente in this.listaClientes)
        {
            if (cliente._profissao == _profissao)
            {
                clientesFilterProfissao.Add(cliente);
            }
        }
        foreach (var item in clientesFilterProfissao)
        {
            Console.WriteLine($"Nome {item.Nome} - Profissão {item._profissao}");
        }
    }

    public void ClienteAlfaOrdenados()
    {
        List<Cliente> clientesAlfa = new List<Cliente>();
        foreach (Cliente cliente in this.listaClientes)
        {
            clientesAlfa.Add(cliente);
        }
        foreach (var item in clientesAlfa.OrderBy(c => c.Nome).ToList())
        {
            Console.WriteLine($"Nome {item.Nome} ");
        }
    }

    public List<Cliente> ObterClientes()
    {
        return listaClientes;
    }

}

class ListaAdvCli{
        public List<Pessoa> Pessoas = new List<Pessoa>();

        public void AdicionarPessoa(Pessoa pessoa)
        {
            this.Pessoas.Add(pessoa);
        }

        public void ImprimePessoas()
        {
            foreach (Pessoa pessoa in this.Pessoas)
            {
                pessoa.imprime();
            }
        }

        public void PessoasFilterMesAniversarios(int _mes)
        {
            List<Pessoa> pessoasFilterMes = new List<Pessoa>();
            foreach (Pessoa pessoa in this.Pessoas)
            {
                if (pessoa.DataNascimento.Month == _mes)
                {
                    pessoasFilterMes.Add(pessoa);
                }
            }
        foreach (var item in pessoasFilterMes)
            {
                Console.WriteLine($"Nome {item.Nome} - Mês aniversário {item.DataNascimento.Month}");
            };
        }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        ListaAdvogados listaAdvogados = new ListaAdvogados();
        ListaClientes listaClientes = new ListaClientes();
        ListaAdvCli listaAdvCli = new ListaAdvCli();

        Advogado Venancio = new Advogado("Venancio", 25, "01/01/2000", "123456", "123456");
        listaAdvogados.AdicionarAdvogado(Venancio);
        listaAdvCli.AdicionarPessoa(Venancio);

        Advogado Kadmo = new Advogado("Kadmo", 30, "01/01/2000", "09876543210", "654321");
        listaAdvogados.AdicionarAdvogado(Kadmo);
        listaAdvCli.AdicionarPessoa(Kadmo);

        Cliente Silas = new Cliente("Silas", 28, "01/01/2000", "0987654321","Solteiro", "Noia");
        listaClientes.AdicionarCliente(Silas);
        listaAdvCli.AdicionarPessoa(Silas);

        Cliente Levy = new Cliente("Levy", 30, "01/01/2000", "12345678910","Solteiro", "Mêoteiro");
        listaClientes.AdicionarCliente(Levy);
        listaAdvCli.AdicionarPessoa(Levy);

        Console.WriteLine("---CLIENTES ORDENADOS ALFABETICAMENTE---");
        listaClientes.ClienteAlfaOrdenados();
        Console.WriteLine("\n");
        Console.WriteLine("---CLIENTES ENTRE 20 E 30 ANOS---");
        listaClientes.ClientesPorIdades(20, 30);
        Console.WriteLine("\n");
        Console.WriteLine("---CLIENTES PROFISSAO = 'Noia' ---");
        listaClientes.ClientesPorProfissao("Noia");
        Console.WriteLine("\n");
        Console.WriteLine("---CLIENTES ESTADO CIVIL = 'Solteiro'---");
        listaClientes.ClientesPorEstadoCivil("Solteiro");
        Console.WriteLine("\n");
        Console.WriteLine("---ADVOGADOS COM IDADE ENTRE 20 E 30 ANOS---");
        listaAdvogados.AdvogadosPorIdades(20, 30);
        Console.WriteLine("\n");
        Console.WriteLine("---ADVOGADOS E CLIENTES ANIVERSARIANTE DO MÊS 03---");
        listaAdvCli.PessoasFilterMesAniversarios(1);
        Console.WriteLine("\n");
        Console.WriteLine("FElIZ NATAL :D");
    }        
}
#endregion
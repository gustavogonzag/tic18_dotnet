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
        this.CPF = _CPF;
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

    public List<Cliente> ClientesPorIdades(int _idade1, int _idade2)
    {
        List<Cliente> clientesFilterIdade = new List<Cliente>();
        foreach (Cliente cliente in this.listaClientes)
        {
            if (cliente.Idade >= _idade1 && cliente.Idade <= _idade2)
            {
                clientesFilterIdade.Add(cliente);
            }
        }
        return clientesFilterIdade;
    }

    public List<Cliente> ClientesPorEstadoCivil(string _estadoCivil)
    {
        List<Cliente> clientesFilterEstadoCivil = new List<Cliente>();
        foreach (Cliente cliente in this.listaClientes)
        {
            if (cliente._estadoCivil == _estadoCivil)
            {
                clientesFilterEstadoCivil.Add(cliente);
            }
        }
        return clientesFilterEstadoCivil;
    }

    public List<Cliente> ClientesPorProfissao(string _profissao)
    {
        List<Cliente> clientesFilterProfissao = new List<Cliente>();
        foreach (Cliente cliente in this.listaClientes)
        {
            if (cliente._profissao == _profissao)
            {
                clientesFilterProfissao.Add(cliente);
            }
        }
        return clientesFilterProfissao;
    }

    public List<Cliente> ClienteAlfaOrdenados()
    {
        List<Cliente> clientesAlfa = new List<Cliente>();
        foreach (Cliente cliente in this.listaClientes)
        {
            clientesAlfa.Add(cliente);
        }
        return clientesAlfa.OrderBy(c => c.Nome).ToList();
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

        public List<Pessoa> PessoasFilterMesAniversarios(int _mes)
        {
            List<Pessoa> pessoasFilterMes = new List<Pessoa>();
            foreach (Pessoa pessoa in this.Pessoas)
            {
                if (pessoa.DataNascimento.Month == _mes)
                {
                    pessoasFilterMes.Add(pessoa);
                }
            }
            return pessoasFilterMes;
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

        listaAdvogados.AdicionarAdvogado(new Advogado("Joaquim", 25, "11/11/2000", "12345678901", "123456789"));
        listaAdvogados.AdicionarAdvogado(new Advogado("PEdro", 22, "25/02/1998", "234324132", "34534567678"));

        listaClientes.AdicionarCliente(new Cliente("Marcelo", 28, "11/03/1997", "12345678901", "Solteiro", "Noia"));
        listaClientes.AdicionarCliente(new Cliente("Dab", 30, "20/06/1988", "2137648438", "Solteiro", "Meotêro"));

        listaAdvCli.AdicionarAdvogados(listaAdvogados.ObterAdvogados());
        listaAdvCli.AdicionarClientes(listaClientes.ObterClientes());

        listaClientes.ClienteAlfaOrdenados();
        listaClientes.ClientesPorIdades(20, 30);
        listaClientes.ClientesPorProfissao("Noia");
        listaClientes.ClientesPorEstadoCivil("Solteiro");

        listaAdvogados.AdvogadosPorIdades(20, 30);
    }        
}
#endregion
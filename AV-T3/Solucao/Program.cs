#region Avaliamento-t3
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

class Pessoa{
        public string nome{get; protected set;}
        protected DateTime DataNascimento;
        public string CPF { get; protected set;}
        public int idade{get; protected set;}

        public Pessoa(string _nome, int _idade, DateTime _dataNascimento, string _CPF){
            this.nome = _nome;
            this.idade = _idade;
            this.DataNascimento = _dataNascimento;
            this.CPF = _CPF;
        }

        public void imprime(){
            Console.WriteLine($"{this.nome} - {this.idade}");
        }

        public void setNome(string _nome){
            if(_nome.Length > 0){
                this.nome = _nome;
            } else {
                throw new ArgumentException("Nome inválido");
            }
        }

        public void setIdade(int _idade){
            if(_idade < 0){
                this.idade = 0;
                return;
            } else {
                throw new ArgumentException("Idade inválida");
            }
        }

        public void setDataNascimento(DateTime _dataNascimento){
            if(_dataNascimento < DateTime.Now){
                this.DataNascimento = _dataNascimento;
            } else {
                throw new ArgumentException("Data inválida");
            }
        }

        public void setCPF(string _CPF){
            if(_CPF.Length == 11 && _CPF.All(c => char.IsDigit(c))){
                this.CPF = _CPF;
            } else {
                throw new ArgumentException("CPF inválido");
            }
        }
    }

class Advogado : Pessoa {
    public string CNA;

    public Advogado(string _nome, int _idade, DateTime _dataNascimento, string _CPF, string _CNA) : base(_nome, _idade, _dataNascimento, _CPF){
        this.CNA = _CNA;
    }

    public string getCNA(){
        return this.CNA;
    }

    protected void setCNA(string _CNA){
        if(_CNA.Length > 0){
            this.CNA = _CNA;
        } else {
            throw new ArgumentException("CNA inválido");
        }
    }

}

class Cliente : Pessoa {
    public string _estadoCivil {get; protected set;}
    public string _profissao{get; protected set;}

    public Cliente(string _nome, int _idade, DateTime _dataNascimento, string _CPF, string _estadoCivil, string _profissao) : base(_nome, _idade, _dataNascimento, _CPF){
        this._estadoCivil = _estadoCivil;
        this._profissao = _profissao;
    }

    public string getEstadoCivil(){
        return this._estadoCivil;
    }

    public void setEstadoCivil(string _estadoCivil){
        if(_estadoCivil.Length > 0){
            this._estadoCivil = _estadoCivil;
        } else {
            throw new ArgumentException("Estado civil inválido");
        }
    }

    public string getProfissao(){
        return this._profissao;
    }

    public void setProfissao(string _profissao){
        if(_profissao.Length > 0){
            this._profissao = _profissao;
        } else {
            throw new ArgumentException("Profissão inválida");
        }
    }
}

class ListaAdvogados{
    public List<Advogado> listaAdvogados = new List<Advogado>();
    public void AdicionarAdvogado(Advogado advogado){
        if(listaAdvogados.Any((c => c.CPF == advogado.CPF)) || listaAdvogados.Any((c => c.CNA == advogado.CNA))){
                throw new Exception("Dados de advogado já cadastrados");
            }
        this.listaAdvogados.Add(advogado);
    }

    public List<Advogado> AdvogadosPorIdades(int _idade1, int _idade2){
        List<Advogado> advogadosFilterIdade = new List<Advogado>();
        foreach(Advogado advogado in this.listaAdvogados){
            if(advogado.idade >= _idade1 && advogado.idade <= _idade2){
                advogadosFilterIdade.Add(advogado);
            }
        }
        return advogadosFilterIdade;
    }
}

class ListaClientes{
    public List<Cliente> listaClientes = new List<Cliente>();
    public void AdicionarCliente(Cliente cliente){
        if(listaClientes.Any((c => c.CPF == cliente.CPF))){
                throw new Exception("Dados de cliente já cadastrados");
            }
        this.listaClientes.Add(cliente);
    }

    public List<Cliente> ClientesPorIdades(int _idade1, int _idade2){
        List<Cliente> clientesFilterIdade = new List<Cliente>();
        foreach(Cliente cliente in this.listaClientes){
            if(cliente.idade >= _idade1 && cliente.idade <= _idade2){
                clientesFilterIdade.Add(cliente);
            }
        }
        return clientesFilterIdade;
    }

    public List<Cliente> ClientesPorEstadoCivil(string _estadoCivil){
        List<Cliente> clientesFilterEstadoCivil = new List<Cliente>();
        foreach(Cliente cliente in this.listaClientes){
            if(cliente._estadoCivil == _estadoCivil){
                clientesFilterEstadoCivil.Add(cliente);
            }
        }
        return clientesFilterEstadoCivil;
    }

    public List<Cliente> ClientesPorProfissao(string _profissao){
        List<Cliente> clientesFilterProfissao = new List<Cliente>();
        foreach(Cliente cliente in this.listaClientes){
            if(cliente._profissao == _profissao){
                clientesFilterProfissao.Add(cliente);
            }
        }
        return clientesFilterProfissao;
    }

    public List<Cliente> ClienteAlfaOrdenados(){
        List<Cliente> clientesAlfa = new List<Cliente>();
        foreach(Cliente cliente in this.listaClientes){
            clientesAlfa.Add(cliente);
        }
        return clientesAlfa.OrderBy(c => c.nome).ToList();
    }
}

#endregion
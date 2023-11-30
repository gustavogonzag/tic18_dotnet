#region Avaliamento-t3
using Microsoft.VisualBasic;

class Pessoa{
        protected string nome;
        protected DateTime DataNascimento;
        public string CPF { get; protected set;}
        protected int idade;

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
            if(_CPF.Length == 11){
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
    protected string _estadoCivil;
    protected string _profissao;

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
}

class ListaClientes{
    public List<Cliente> listaClientes = new List<Cliente>();
    public void AdicionarCliente(Cliente cliente){
        if(listaClientes.Any((c => c.CPF == cliente.CPF))){
                throw new Exception("Dados de cliente já cadastrados");
            }
        this.listaClientes.Add(cliente);
    }
}

#endregion
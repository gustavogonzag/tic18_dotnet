// #region Veiculo
//     Veiculo corsa = new Veiculo("Corsa", "2020", "Preto");
//     Console.WriteLine(corsa.imprime());

//     class Veiculo{
//         private string modelo;
//         private string ano;
//         private string cor;

//         public Veiculo(string _modelo, string _ano, string _cor){
//             this.modelo = _modelo;
//             this.ano = _ano;
//             this.cor = _cor;
//         }

//         public int IdadeVeiculo => DateTime.Now.Year - int.Parse(this.ano);

//         public string imprime(){
//             return $"Modelo: {this.modelo}\nAno: {this.ano}\nCor: {this.cor} \nIdade: {this.IdadeVeiculo}";
//         }
//     }
// #endregion

// #region ContaBancaria

//     ContaBancaria conta = new ContaBancaria();
//     conta.Saldo = 0;
//     Console.WriteLine(conta.Saldo);

//     class ContaBancaria{
//         private string _nome;
//         private double _saldo;

//         public double Saldo{
//             get { return _saldo; }
//             set { 
//                 if(value > 0){
//                     _saldo = value;
//                 } else {
//                     throw new ArgumentException("Saldo deve ser maior que zero");
//                 }
//             }
//         }
//     }
// #endregion

#region Agenda

    Agenda agenda = new Agenda();
    agenda.AdicionarContato(new Contato("Gustavo", "123456789", "987654321"));
    agenda.imprime();
    agenda.AdicionarContato(new Contato("Gustavo", "123456789", "987654321"));
    agenda.imprime();


    class Contato{
        public string nome {get; set;}
        public string CPF{get;}
        public string telefone{get; set;}

        public Contato(string _nome, string _CPF, string _telefone){
            this.nome = _nome;
            this.CPF = _CPF;
            this.telefone = _telefone;
        }
    }
    class Agenda{
        private List<Contato> contatos = new List<Contato>();

        public void AdicionarContato(Contato contato){
            if(contatos.Any(c => c.CPF == contato.CPF)){
                throw new Exception("Contato ja existe");
            }
            this.contatos.Add(contato);
        }

        public void imprime(){
            foreach(Contato contato in this.contatos){
                Console.WriteLine($"{contato.nome} - {contato.CPF} - {contato.telefone}");
            }
        }
    }    

#endregion
using System;

namespace ObjetoTransferencia
{
    //A CLASSE É PUBLICA PARA QUE CONSIGA ACESSAR OS METODOS 

    public class Cliente
    {
        //PROPRIEDADES DO CLIENTE(OBS TIPOS DE DADOS AZUL ESCURO E AZUL CLARO string é um apelido para a classe original que é String, o int que usamos é o apelido de Int32)
        public Int32 IdCliente { get; set; }
        public String Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Boolean Sexo { get; set; }
        public Decimal LimiteCompra { get; set; }
    }
}

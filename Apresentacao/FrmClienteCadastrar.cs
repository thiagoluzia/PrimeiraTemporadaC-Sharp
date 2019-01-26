using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//COM ISSO POSSO USAR OS OBJETOS CONTIDOS EM OBJETOS TRANSFERENCIA 
using ObjetoTransferencia;

namespace Apresentacao
{
    public partial class FrmClienteCadastrar : Form
    {
        //criando variaveis da classe, para serem usadas em qualquer parte do codigo dentro desse namespace
        AcaoNaTela acaoNaTelaSelecionada;
        public FrmClienteCadastrar(AcaoNaTela acaoNaTela, Cliente cliente)//MODIFICANDO O CONSTRUTOR PARA QUE ELE POSSA UTILIZAR O OBJETO TRANSFERENCIA E EFETUAR AS OPERAÇÕES NO BANCO LEVANDO OS DADOS DO CLIENTE
        {
            InitializeComponent();
            acaoNaTelaSelecionada = acaoNaTela;//inicializando a variavel da classe

            if (acaoNaTela.Equals(AcaoNaTela.Inserir))
            {
                this.Text = "Inserir Cliente";//Abre o form so que com nome diferente
            }
            else if (acaoNaTela.Equals(AcaoNaTela.Alterar))
            {
                this.Text = "Alterar Cliente";

                //CARREGAR DADOS NA TELA
                textBoxCodigo.Text = cliente.IdCliente.ToString();//pega o id do cliente dentro do textBox
                textBoxNome.Text = cliente.Nome;//pega o nome do cliente dentro do textBox
                dateTimeDataNascimento.Value = cliente.DataNascimento.Date;
                if (cliente.Sexo == true)
                {
                    radioButtonMasculino.Checked = true;//Sexo masculino
                }
                else
                {
                    radioButtonFeminino.Checked = true;//Sexo feminino
                }
                textBoxLimiteCompra.Text = cliente.LimiteCompra.ToString("C2");//C2 formata o valor R$
            }
            else if (acaoNaTela.Equals(AcaoNaTela.Consultar))//EFETUA UMA CONSULTA
            {
                this.Text = "Consultar Cliente";

                //CARREGAR DADOS NA TELA
                textBoxCodigo.Text = cliente.IdCliente.ToString();//pega o id do cliente dentro do textBox
                textBoxNome.Text = cliente.Nome;//pega o nome do cliente dentro do textBox
                dateTimeDataNascimento.Value = cliente.DataNascimento.Date;
                if (cliente.Sexo == true)
                {
                    radioButtonMasculino.Checked = true;//Sexo masculino
                }
                else
                {
                    radioButtonFeminino.Checked = true;//Sexo feminino
                }
                textBoxLimiteCompra.Text = cliente.LimiteCompra.ToString("C2");//C2 formata o valor R$

                //DESABILITA CAMPOS DA TELA
                textBoxNome.ReadOnly = true;
                textBoxNome.TabStop = false;

                dateTimeDataNascimento.Enabled = false;
                dateTimeDataNascimento.TabStop = false;

                radioButtonFeminino.Enabled = false;
                radioButtonFeminino.TabStop = false;

                radioButtonMasculino.Enabled = false;
                radioButtonMasculino.TabStop = false;

                textBoxLimiteCompra.ReadOnly = true;
                textBoxLimiteCompra.TabStop = false;

                //DEIXAR INVISIVEL O BOTAO SALVAR
                buttonSalvar.Visible = false;

                //MUDAR O NOME DO BOTÃO DE CANCELAR PARA FECHAR
                buttonCancelar.Text = "Fechar";
                buttonCancelar.Focus();

            }
        }
        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            //VERIFICAR SE É  INSERÇAO OU ALTERAÇÃO
            if (acaoNaTelaSelecionada.Equals(AcaoNaTela.Inserir))
            {

            }
            else if (acaoNaTelaSelecionada.Equals(AcaoNaTela.Alterar))
            {

            }
        }
    }
}

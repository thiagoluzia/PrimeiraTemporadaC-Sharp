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
using Negocios;

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
                textBoxLimiteCompra.Text = cliente.LimiteCompra.ToString();
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
                textBoxLimiteCompra.Text = cliente.LimiteCompra.ToString();

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
            DialogResult = DialogResult.No;//ele devolve o no, ou seja ele fechar a tela e nos devolve um No e  a tela de baixo nao faz nada 
            //Close();//fecha a tela 
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            //VERIFICAR SE É  INSERÇAO OU ALTERAÇÃO
            if (acaoNaTelaSelecionada.Equals(AcaoNaTela.Inserir))
            {
                //instanciando um cliente 
                Cliente cliente = new Cliente();
                //as propriedades do cliente recebendo dados pelos componentes
                cliente.Nome = textBoxNome.Text;
                cliente.DataNascimento = dateTimeDataNascimento.Value;
                //verificando qual campo esta checado, masculino ou feminino
                if(radioButtonMasculino.Checked == true)
                {
                    cliente.Sexo = true;
                }
                else
                {
                    cliente.Sexo = false;
                }
                //Convertendo um string para decimal
                cliente.LimiteCompra = Convert.ToDecimal(textBoxLimiteCompra.Text);
                //instanciando a regra de negocio, la que tera os parametros para inserir
                ClienteNegocios clienteNegocio = new ClienteNegocios();
                string retorno = clienteNegocio.Inserir(cliente);

                //tentar converter para inteiro o retorno da procedure
                //se de certo é porque devolveu o codigo do cliente 
                //se de errado tem a mensagem de erro
                try
                {
                    int IdCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente inserido com sucesso. codigo : " + retorno.ToString());
                    this.DialogResult = DialogResult.Yes;

                }
                catch (Exception)
                {

                    MessageBox.Show("Não foi possivel inserir o cliente. Detalhes: " + retorno, "Erro", MessageBoxButtons.OK , MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }


            }
            else if (acaoNaTelaSelecionada.Equals(AcaoNaTela.Alterar))
            {
                //instanciando um cliente //CRIO UM NOVO CLIENTE
                Cliente cliente = new Cliente();

                //COLOCO OS CAMPOS DA TELA EM UM OBJETO CLIENTE, E ENVIO PARA ALTERAR NO BANCO
                cliente.IdCliente = Convert.ToInt32(textBoxCodigo.Text);
                //as propriedades do cliente recebendo dados pelos componentes
                cliente.Nome = textBoxNome.Text;
                cliente.DataNascimento = dateTimeDataNascimento.Value;
                //verificando qual campo esta checado, masculino ou feminino
                if (radioButtonMasculino.Checked == true)
                {
                    cliente.Sexo = true;
                }
                else
                {
                    cliente.Sexo = false;
                }
                //Convertendo um string para decimal
                cliente.LimiteCompra = Convert.ToDecimal(textBoxLimiteCompra.Text);
                //instanciando a regra de negocio, la que tera os parametros para inserir
                ClienteNegocios clienteNegocio = new ClienteNegocios();
                string retorno = clienteNegocio.Alterar(cliente);

                //tentar converter para inteiro o retorno da procedure
                //se de certo é porque devolveu o codigo do cliente 
                //se de errado tem a mensagem de erro
                try
                {
                    int IdCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente atualizado com sucesso. codigo : " + retorno.ToString());
                    this.DialogResult = DialogResult.Yes;

                }
                catch (Exception)
                {

                    MessageBox.Show("Não foi possivel atualizar o cliente. Detalhes: " + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }

            }
        }
    }
}

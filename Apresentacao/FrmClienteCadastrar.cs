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
        public FrmClienteCadastrar(AcaoNaTela acaoNaTela, Cliente cliente)//MODIFICANDO O CONSTRUTOR PARA QUE ELE POSSA UTILIZAR O OBJETO TRANSFERENCIA E EFETUAR AS OPERAÇÕES NO BANCO LEVANDO OS DADOS DO CLIENTE
        {
            InitializeComponent();
            if (acaoNaTela.Equals(AcaoNaTela.Inserir))
            {
                this.Text = "Inserir Cliente";//Abre o form so que com nome diferente
            }
            else if (acaoNaTela.Equals(AcaoNaTela.Alterar))
            {
                this.Text = "Alterar Cliente";
                textBoxCodigo.Text = cliente.IdCliente.ToString();//pega o id do cliente dentro do textBox
                textBoxNome.Text = cliente.Nome;//pega o nome do cliente dentro do textBox
                dateTimeDataNascimento.Value = cliente.DataNascimento;
                if(cliente.Sexo == true )
                {
                    radioButtonMasculino.Checked = true;//Sexo masculino
                }
                else
                {
                    radioButtonFeminino.Checked = true;//Sexo feminino
                }
                textBoxLimiteCompra.Text = cliente.LimiteCompra.ToString("C2");//C2 formata o valor R$
            }
            else if (acaoNaTela.Equals(AcaoNaTela.Consultar))
            {
                this.Text = "Consultar Cliente";
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

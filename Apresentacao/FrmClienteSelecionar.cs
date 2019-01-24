using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//1ºREFERENCIAR OS DOIS PROJETOS NA CAMADA DE APRESNTAÇÃO
using Negocios;
using ObjetoTransferencia;

namespace Apresentacao
{
    public partial class FrmClienteSelecionar : Form
    {
        public FrmClienteSelecionar()
        {
            InitializeComponent();
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();//APOS A REFATORAÇÃO, CHAMANDO O METODO DE ATUALIZAR O GRID

        }
        //METODO REFATORADO NO BOTAO EXCLUIR, PARA NAO TER QUE REPETIR O CODIGO EM VARIOS LUGARES, ANTERIORMENTE ESTAVA NO EVENTO CLICK DO BOTAO PESQUISAR
        private void AtualizarGrid()
        {
            ClienteNegocios clienteNegocios = new ClienteNegocios();//2ºINSTANCIAR A REGRA DE NEGOCIOO OBJETO(CLASSE) ClienteNegocio

            ClienteColecao clienteColecao = new ClienteColecao();//2ºINSTANCIAR O OBJETO(CLASSE) ClienteColecao

            clienteColecao = clienteNegocios.ConsultarPorNome(textBoxPesquisar.Text);//CLIENTE COLEÇÃO(NOSSA COLECAO DE CLIENTE) RECEBE CLIENTE NEGOCIO E O METODO CONSULTAR POR NOME, PASSANDO COMO PARAMETRO O NOSSO TEXTBOX (textBoxPesquisar.Text) OU SEJA O NAME DO COMPONENTE MAIS SUA PROPRIEDADE DE RETORNO NA QUL QUEREMOS PEGAR

            dataGridViewPrincipal.DataSource = null;//DATAGRIDVIEWPRINCIPAL(NAME DO NOSSO DATA GRID ) .DATASOURCE(PROPRIEDADE DO CAMINHO) = NULL (OU SEJA ELE LIMPA O DATA GRID)
            dataGridViewPrincipal.DataSource = clienteColecao;//AGORA O DATA GRID VIEW PRINCIPAL E SEU CAMINHO RECEBE UMA COLECAO DE CLIENTES

            dataGridViewPrincipal.Update();//ATUALIZAR OS DADOS NO GRID
            dataGridViewPrincipal.Refresh();//PARA ATUALIZADA A VISUALIZAÇÃO
        }
        private void buttonFechar_Click(object sender, EventArgs e)
        {
            //FECHAR O FORMULARIO ATUAL
            Close();
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            //1º VERIFICAR SE TEM REGISTRO SELECIONADO 
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado. ");
                return;
            }

            //2º PERGUNTAR SE REALMENTE QUER EXCLUIR 
            DialogResult resultado = MessageBox.Show("Deseja realmente excluir esse cliente?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //VERIFICANDO O RESULTADO COM CONDICIONAL
            if (resultado == DialogResult.No)
            {
                return;
            }

            //PEGAR CLIENTE SELECIONADO
            Cliente clienteSelecionado = new Cliente();
            clienteSelecionado.IdCliente = int.Parse(dataGridViewPrincipal.CurrentRow.Cells[0].Value.ToString());//Caso o valor //do IdCliente esteja na célula [0], senão indica a posição certa.


            //4º INSTANCIAR A REGRA DE NEGOCIO
            ClienteNegocios clienteNegocios = new ClienteNegocios();

            //CHAMAR O METODO EXCLUIR
            string retorno = clienteNegocios.Excluir(clienteSelecionado);

            //VERIFICAR SE EXCLUIU COM SUCESSO
            try
            { 
                int idCliente = int.Parse(retorno);//verificando se a string tem o valor int
                MessageBox.Show("Cliente Excluido com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarGrid();

                
            }
            catch
            {
               
            }

        }
    }
}

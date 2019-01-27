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
            Cliente clienteSelecionado = dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente;
            //Caso o valor //do IdCliente esteja na célula [0], senão indica a posição certa.



            //4º INSTANCIAR A REGRA DE NEGOCIO
            ClienteNegocios clienteNegocios = new ClienteNegocios();

            //CHAMAR O METODO EXCLUIR
            string retorno = clienteNegocios.Excluir(clienteSelecionado);

            //VERIFICAR SE EXCLUIU COM SUCESSO
            try
            {
                int idCliente;
                bool boolSucesso = int.TryParse(retorno, out idCliente);//verificando se a string tem o valor int
                MessageBox.Show("O Cliente de ID" + idCliente.ToString() + " Foi excluido com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //ATUALIZAR O GRID, POIS SÓ EXCLUIR ELE NAO SOME DO GRID NA HORA.
                AtualizarGrid();
            }
            catch
            {
                MessageBox.Show("Não foi possível, excluir. Detalhes: " + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void buttonInserir_Click(object sender, EventArgs e)
        {
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Inserir, null);//AcaoNaTela.Inserir foi inserido depois em aulas de enumeradores //DEVO PASSAR COMO PARAMETRO UM CLIENTE POREM POR SER UM NOVO CLIENTE USO O NULL #AULA ALTERAR E CONSULTAR #
            //atualizar o grid apos inserir um dado com sucesso
            DialogResult dialogResult = frmClienteCadastrar.ShowDialog();

            if (dialogResult == DialogResult.Yes)
            {
                AtualizarGrid();
            }
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            //1º VERIFICAR SE TEM REGISTRO SELECIONADO 
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado");
                return;
            }
            //2ºPEGAR CLIENTE SELECIONADO
            Cliente clienteSelecionado = new Cliente();
            clienteSelecionado = dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente;

            //3ºINSTANCIA O FORMULARIO DE CADASTRO *JA SE ENCONTRA INSTANCIADO, FEITO ANTERIORMENTE, RESTANDO AGORA PASSAR SOMENTE O NOVO PARAMETRO,  QUE É CLIENTE SELECIONADO
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Alterar, clienteSelecionado);
            frmClienteCadastrar.ShowDialog();
        }

        private void buttonConsultar_Click(object sender, EventArgs e)
        {
            //1º VERIFICAR SE TEM REGISTRO SELECIONADO 
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado");
                return;
            }
            //2ºPEGAR CLIENTE SELECIONADO
            Cliente clienteSelecionado = new Cliente();
            clienteSelecionado = dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente;

            //3ºINSTANCIA O FORMULARIO DE CADASTRO *JA SE ENCONTRA INSTANCIADO, FEITO ANTERIORMENTE, RESTANDO AGORA PASSAR SOMENTE O NOVO PARAMETRO,  QUE É CLIENTE SELECIONADO

            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Consultar, clienteSelecionado);
            frmClienteCadastrar.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

//DECLARANDO O OBJETO AcessoBancoDados (o projeto) com isso posso usar suas classes publicas sem ter que ficar instanciando toda vez que precisar(com isso posso usar as collection cliente)
using AcessoBancoDados;
//DECLARANDO O OBJETO ObjetoTrnasferencia para usar para tranferir os parametros 
using ObjetoTransferencia;

namespace Negocios
{
    //
    public class ClienteNegocios
    {
        //INSTANCIAR O BANCO = CRIAR UM NOVO OBJETO BASEADO EM UM MODELO
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();

        //CRIANDO O METODO PARA INSERIR, COMO PARAMETRO DE ENTRADA VAMOS PASSAR UM CLIENTE COMO PARAMETRO (OBJETO TRANSFERENCIA) Cliente é a classe contendo todos os parametros de cliente(todos atributos)
        public string Inserir(Cliente cliente)
        
        {
            try//tratamento de erro
            {
                acessoDadosSqlServer.LimparParametros();//Limpar os parametros antes de qualquer coisa(LIMPARA PARAMETROS É UMA CLASSE QUE CRIAMOS EM ACESSO DADOS SQLSERVER)
                acessoDadosSqlServer.AdcionarParametros("@Nome", cliente.Nome);//Ele vai pegar la na Classe Cliente o campo Nome e inserir no banco no parametro @nome
                acessoDadosSqlServer.AdcionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdcionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdcionarParametros("@LimiteCompra", cliente.LimiteCompra);
                //APOS COLETARMOS TODOS OS PARAMETROS DE ENTRADA IREMOS EXECUTAR A MANIPULAÇÃO e  Armazeno ela em uma variavel para retorno na seguite linha
                string IdCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();
                //retorno do id que esta na procedure 
                return IdCliente;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        //CRIANDO O METODO PARA ALTERAR, COMO PARAMETRO DE ENTRADA VAMOS PASSAR UM CLIENTE COMO PARAMETRO (OBJETO TRANSFERENCIA)
        public string Alterar(Cliente cliente)
        {
            //tratamento de erro
            try
            {
                acessoDadosSqlServer.LimparParametros();//Limpara parametros
                //Adcionar os parametros que a procedure alterar precisa
                acessoDadosSqlServer.AdcionarParametros("IdCliente", cliente.IdCliente);
                acessoDadosSqlServer.AdcionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdcionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdcionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdcionarParametros("@LimiteCompra", cliente.LimiteCompra);
                //APOS COLETARMOS TODOS OS PARAMETROS DE ENTRADA IREMOS EXECUTAR A MANIPULAÇÃO e  Armazeno ela em uma variavel para retorno na seguite linha
                string IdCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteAlterar").ToString();
                //retorno do id que esta na procedure 
                return IdCliente;
            }
            catch (Exception excepton)
            {
                return excepton.Message;
            }
        }
        //CRIANDO O METODO PARA EXCLUIR, COMO PARAMETRO DE ENTRADA VAMOS PASSAR UM CLIENTE COMO PARAMETRO (OBJETO TRANSFERENCIA)
        public string Excluir(Cliente cliente)
        {   
            //tratamento de excessao
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdcionarParametros("@IdCliente", cliente.IdCliente);
               return acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();//COM PROCEDURE CODIGO SQL NO BANCO

               //return acessoDadosSqlServer.ExecutarManipulacao(CommandType.Text, "DELETE FROM tblCliente WHERE IdCliente = @").ToString();//SEM PROCEDURE, CODIGO SQL NA PASSAGEM DE PARAMETROS

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //CRIANDO O METODO PARA CONSULTAR POR NOME, COMO PARAMETRO DE ENTRADA VAMOS PASSAR UM CLIENTE COLEÇÃO COMO PARAMETRO (OBJETO TRANSFERENCIA)
        public ClienteColecao ConsultarPorNome(string nome)
        {
            try
            {
                //Criar uma nova coleção de cliente (aqui ela esta vazia) ou seja o vetor coleção esta vazio
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdcionarParametros("@Nome", nome);
                //consulto no banco o cliente e retorno o resultado em um dataTable(uma colção de clientes)
                //cada linha de um data table é um cliente 
                DataTable dataTableCliente = acessoDadosSqlServer.ExecutaConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                //Percorrer o DataTable e transformar em uma coleção de cliente
                //Cada linha do DataTable é um cliente
                //Data = Dados e Row = Linha 
                //Fro = para, Each = cada
                foreach (DataRow linha in dataTableCliente.Rows) //por linhas Rows
                {
                    //Criar um cliente vazio
                    Cliente cliente = new Cliente();

                    //colocar os dados da linha nele
                    cliente.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);

                    //Adicionar ele na coleção
                    clienteColecao.Add(cliente);//Como se fosse um vetor do tipo cliente
                }

                return clienteColecao;
            }
            catch (Exception ex)
            { 
                throw new Exception("Nao foi possivel consultar o cliente por nome, detalhes: " + ex);
            }
        }
        //CRIANDO O METODO PARA CONSULTAR POR ID, COMO PARAMETRO DE ENTRADA VAMOS PASSAR UM CLIENTE COLEÇÃO COMO PARAMETRO (OBJETO TRANSFERENCIA)
        public ClienteColecao ConsultarPorId(int Id)
        {
            try
            {
                //Criar uma nova coleção de cliente (aqui ela esta vazia) ou seja o vetor coleção esta vazio
                ClienteColecao clienteColecao= new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdcionarParametros("@IdCliente", Id);
                //consulto no banco o cliente e retorno o resultado em um dataTable(uma colção de clientes)
                //cada linha de um data table é um cliente 
                DataTable dataTableCliente = acessoDadosSqlServer.ExecutaConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorId");

                //Percorrer o DataTable e transformar em uma coleção de cliente
                //Cada linha do DataTable é um cliente
                //Data = Dados e Row = Linha 
                //Fro = para, Each = cada
                foreach (DataRow linha in dataTableCliente.Rows) //por linhas Rows
                {
                    //Criar um cliente vazio
                    Cliente cliente = new Cliente();

                    //colocar os dados da linha nele
                    cliente.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);

                    //Adicionar ele na coleção
                    clienteColecao.Add(cliente);//Como se fosse um vetor do tipo cliente
                }

                return clienteColecao;
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi possivel consultar o cliente pelo Codigo, detalhes: " + ex);
            }
        }

    }
}

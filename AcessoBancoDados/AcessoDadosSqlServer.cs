using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.SqlClient;
using AcessoBancoDados.Properties;

namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        //1º CRIAR CONEXÃO
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //2º PARAMETROS QUE VÃO PARA O BANCO
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }
        public void AdcionarParametros(string nomeParametros, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametros, valorParametro));
        }

        //3º PERSISTENCIA - (INSERIR, ALTERAR, EXCLUIR)
        public object ExecutarManipulacao(CommandType commandType, string nomeStoredProcedure)
        {
            //TRATAMENTO DE ERRO
            try//tente executar
            {
                //1ºCRIAR CONEXAO
                SqlConnection sqlConnection = CriarConexao();
                //2ºABRIR CONEXAO
                sqlConnection.Open();
                //3ºCRIAR COMANDO QUE VAI LEVAR A INFORMAÇÃO PARA O BANCO
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //4ºCOLOCANDO AS COISAS DENTRO DO COMANDO 
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedure;
                sqlCommand.CommandTimeout = 7200;//EM SEGUNDOS
                                                 //4ºADICIONAR OS PARAMETROS
                foreach (SqlParameter sqlParameter in sqlParameterCollection)//PARA CADA PARAMETRO SQL PARAMETRO NA COLEÇÃO
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));//CRIA UM NOVO E COLOCA O NOME E O VALOR
                }
                    return sqlCommand.ExecuteScalar();//ELE TRAZ A PRIMEIRA COLUNA DA PRIMEIRA LINHA SE ESTIVER LA NA PROCEDURE E O RESTO ELE VAI IGNORAR
                }
            //SE NAO CONSEGUIR EXECUTAR 
            catch (Exception ex)//CAPTURE O ERRO
            {
                throw new Exception(ex.Message);
            }
        }
        //4º CONSULTAR REGISTROS  DO BANCO DE DADOS
        public DataTable ExecutaConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            //TRATAMENTO DE ERRO
            try//tente executar
            {
                //1ºCRIAR CONEXAO
                SqlConnection sqlConnection = CriarConexao();
                //2ºABRIR CONEXAO
                sqlConnection.Open();
                //3ºCRIAR COMANDO QUE VAI LEVAR A INFORMAÇÃO PARA O BANCO
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //4ºCOLOCANDO AS COISAS DENTRO DO COMANDO 
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200;//EM SEGUNDOS
                                                 //4ºADICIONAR OS PARAMETROS
                foreach (SqlParameter sqlParameter in sqlParameterCollection)//PARA CADA PARAMETRO SQL PARAMETRO NA COLEÇÃO
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));//CRIA UM NOVO E COLOCA O NOME E O VALOR
                }
                //CRIAR UM ADAPTADOR(DENTRO DO BANCO A LINGUAGEM É UMA, EM NOSSA APLICAÇÃO É OUTRA) O ADAPTADOR PEGA O CODIGO SQL MODIFICA PARA APLICAÇÃO, E COLOCA EM UM DATATABLE
                //SqlDataAdapter = ADAPTADOR DE DADOS SQL
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);//ESSE SQLCOMMAND CRIADO ANTERIORMENTE TEM TODO CODIGO NECESSARIO PARA EXECUTAR
                //DataTable = tabela de dados vazia omde vou colocar os dados que vem do banco                                                                               
                DataTable dataTable = new DataTable();
                //MANDAR O COMANDO IR ATE O BANCO E BUSCAR OS DADOS E O ADAPTADOR PREENCHEO DATATABLE
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            //SE NAO CONSEGUIR EXECUTAR 
            catch (Exception ex)//CAPTURE O ERRO
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

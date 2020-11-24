using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.SqliteClient;


public class Banco : MonoBehaviour
{

    private IDbConnection conec;
    private IDbCommand command;
    private IDataReader reader;



    private string stringConexao = "URI=File:meuBanco.db";

    private bool conectar()
    {
        try
        {
            conec = new SqliteConnection(stringConexao);
            command = conec.CreateCommand(); //instancia a conexão
            conec.Open(); // abre conexão com banco de dados

            string comandoSql = "CREATE TABLE IF NOT EXISTS USUARIOS" + "(ID INTEGER PRIMARY KEY AUTOINCREMENT, NOME VARCHAR(50));";

            command.CommandText = comandoSql;

            command.ExecuteNonQuery(); //vai executar de fato

            return true;
        }
        catch (System.Exception)
        {

            return false;
        }
        finally
        {

        }
    }

    public bool inserir(string nome)
    {
        try
        {
            conectar();

            string comandoSql = "INSERT INTO USUARIOS(NOME) VALUES ('" + nome + "');";

            command.CommandText = comandoSql;

            command.ExecuteNonQuery();

            return true;
        }
        catch (System.Exception ex)
        {
            return false;
            
        }
        finally
        {
            conec.Close();
        }
    }

    public bool consultar()
    {
        try
        {
            conectar();

            string comandoSql = "SELECT * FROM USUARIOS;";

            command.CommandText = comandoSql;
            reader = command.ExecuteReader();

            int cont = 0;

            while (reader.Read())
            {
                print("ID: " + reader.GetInt32(0));
                print("NOME: " + reader.GetString(1));

                cont++;
            }

            print("Total de Registros: " + cont);

            return true;
        }
        catch (System.Exception ex)
        {
            return false;
            
        }
        finally
        {
            conec.Close();
        }
    }

    public bool alterar (int id, string nome)
    {
        try
        {
            conectar();

            string comandoSql = "UPDATE USUARIOS SET NOME = '" + nome + "' " + "WHERE ID = " + id + ";";

            command.CommandText = comandoSql;

            command.ExecuteNonQuery();

            return true;
        }
        catch (System.Exception ex)
        {

            return false;
        }
        finally
        {
            conec.Close();
        }
    }

    public bool remover (int id)
    {
        try
        {
            conectar();

            string comandoSql = "DELETE FROM USUARIOS WHERE ID = " + id + ";";

            command.CommandText = comandoSql;

            command.ExecuteNonQuery();

            return true;
        }
        catch (System.Exception ex)
        {
            return false;
        }
        finally
        {
            conec.Close();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.UI;
using Mono.Data.SqliteClient;
using Unity.Collections;

public class BancoInventario : MonoBehaviour
{


    private IDbConnection conec;
    private IDbCommand command;
    private IDataReader reader;
    //Beleza. sim. ra ti funciona a foreign key sem esse bagulho? Ta.
    private string stringConexao = "URI=File:meuBanco.db";

    public static int idInserido;
    private string item1 = "ESFERA";
    private string item2 = "CUBO";
    private string item3 = "CAPSULA";
    private string item4 = "CILINDRO";


    private bool conectar()
    {
        try
        {
            conec = new SqliteConnection(stringConexao);
            command = conec.CreateCommand(); //instancia a conexão
            conec.Open(); // abre conexão com banco de dados

            string comandoSql = "PRAGMA foreign_keys = ON; CREATE TABLE IF NOT EXISTS USUARIOS" + "(USUARIOS_ID INTEGER PRIMARY KEY AUTOINCREMENT, NOME VARCHAR(50), POSICAO_X REAL, POSICAO_Y REAL, POSICAO_Z REAL, ROTACAO_Y REAL);" +
                                "CREATE TABLE IF NOT EXISTS INVENTARIO" + "(INVENTARIO_ID INTEGER PRIMARY KEY AUTOINCREMENT,  NOME VARCHAR(50), QUANTIDADE INTEGER, USUARIOS_ID INTEGER, FOREIGN KEY(USUARIOS_ID) REFERENCES USUARIOS(USUARIOS_ID));";


            command.CommandText = comandoSql;

            command.ExecuteNonQuery(); //vai executar de fato

            return true;
        }
        catch (System.Exception ex)
        {
            Debug.Log("Erro ao conectar:" + ex);
            return false;
        }
        finally
        {

        }
    }
    public bool inserirScalar(string nome)
    {
        try
        {
            conectar();

            //string comandoSql = "INSERT INTO USUARIOS(NOME) VALUES ($nome);";
            string comandoSql = "INSERT INTO USUARIOS(NOME, POSICAO_X, POSICAO_Y, POSICAO_Z, ROTACAO_Y) VALUES ('" + nome + "', 0, 0, 0, 0);" +
                               "SELECT last_insert_rowid();";
            //"SELECT CAST(scope_identity() AS int)";

            command.CommandText = comandoSql;
            //command.Parameters.Add(nome);
            var teste = command.ExecuteScalar();

            idInserido = int.Parse(teste.ToString());



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
    public bool inserir()
    {
        try
        {
            conectar();

            string comandoSql = "INSERT INTO INVENTARIO(NOME, QUANTIDADE, USUARIOS_ID) VALUES('" + item1 + "', 0, " + idInserido + ");" +
                                "INSERT INTO INVENTARIO(NOME, QUANTIDADE, USUARIOS_ID) VALUES('" + item2 + "', 0, " + idInserido + ");" +
                                "INSERT INTO INVENTARIO(NOME, QUANTIDADE, USUARIOS_ID) VALUES('" + item3 + "', 0, " + idInserido + ");" +
                                "INSERT INTO INVENTARIO(NOME, QUANTIDADE, USUARIOS_ID) VALUES('" + item4 + "', 0, " + idInserido + ");";


            command.CommandText = comandoSql;//nao pode ser e sguido okk

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

    public bool atualizar(string item)
    {
        try
        {
            conectar();

            string comandoSql = "UPDATE INVENTARIO SET QUANTIDADE = QUANTIDADE + 1 WHERE NOME = '" + item + "' AND USUARIOS_ID = " + idInserido + ";";

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
    public bool consultar(string nome)
    {
        try
        {
            conectar();

            //puxa o ID
            string comandoSql = "SELECT USUARIOS_ID FROM USUARIOS WHERE NOME = '" + nome + "';";

            command.CommandText = comandoSql;
            reader = command.ExecuteReader();

            reader.Read();
            idInserido = reader.GetInt32(0);
            print("ID: " + reader.GetInt32(0));

            //load nos itens
            for (int i = 0; i < Configuracoes.numeroTotalItens; i++)
            {
                comandoSql = "SELECT QUANTIDADE FROM INVENTARIO WHERE USUARIOS_ID = " + idInserido + " AND NOME = '" + Configuracoes.item[i] + "';";

                command.CommandText = comandoSql;
                reader = command.ExecuteReader();

                reader.Read();

                print("QUANTIDADE: " + Configuracoes.item[i] + " " + reader.GetInt32(0));

                int quantidade = reader.GetInt32(0);
                if (Configuracoes.item[i] == "ESFERA") InstanciaObjeto.esfera = quantidade;
                else if (Configuracoes.item[i] == "CUBO") InstanciaObjeto.cubo = quantidade;
                else if (Configuracoes.item[i] == "CAPSULA") InstanciaObjeto.capsula = quantidade;
                else if (Configuracoes.item[i] == "CILINDRO") InstanciaObjeto.cilindro = quantidade;



            }



            return true;
        }
        catch (System.Exception ex)
        {
            print("nao encontrou save");
            return false;

        }
        finally
        {
            conec.Close();
        }
    }

    public bool consultarLoadPosicao()
    {
            string nome = Configuracoes.nomeUsuario;
        try
        {
            conectar();

            string comandoSql = "SELECT POSICAO_X FROM USUARIOS WHERE NOME = '" + nome + "';";
            command.CommandText = comandoSql;
            reader = command.ExecuteReader();
            reader.Read();
            print("Pos_x:" + reader.GetFloat(0));
            float valor = reader.GetFloat(0);
            LoadPosicaoJogador.pos_x = valor;

            comandoSql = "SELECT POSICAO_Y FROM USUARIOS WHERE NOME = '" + nome + "';";
            command.CommandText = comandoSql;
            reader = command.ExecuteReader();
            reader.Read();
            print("Pos_y:" + reader.GetFloat(0));
            valor = reader.GetFloat(0);
            LoadPosicaoJogador.pos_y = valor;

            comandoSql = "SELECT POSICAO_Z FROM USUARIOS WHERE NOME = '" + nome + "';";
            command.CommandText = comandoSql;
            reader = command.ExecuteReader();
            reader.Read();
            print("Pos_z:" + reader.GetFloat(0));
            valor = reader.GetFloat(0);
            LoadPosicaoJogador.pos_z = valor;

            comandoSql = "SELECT ROTACAO_Y FROM USUARIOS WHERE NOME = '" + nome + "';";
            command.CommandText = comandoSql;
            reader = command.ExecuteReader();
            reader.Read();
            print("rot_y" + reader.GetFloat(0));
            valor = reader.GetFloat(0);
            LoadPosicaoJogador.rot_y = valor;


            return true;
        }
        catch (System.Exception ex)
        {
            print("erro load posicao");
            return false;

        }
        finally
        {
            conec.Close();
        }
    }

    public int consultarUsuario(string nome)
    {
        try
        {
            conectar();

            string comandoSql = "SELECT * FROM USUARIOS WHERE NOME = '" + nome + "';";

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


                return cont;
            

        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
            return -1;
        }
        finally
        {
            conec.Close();
        }
    }

    public bool SalvarPosicao(float pos_x, float pos_y, float pos_z, float rot_y)
    {
        string nome = Configuracoes.nomeUsuario;
        try
        {
            conectar();

            string comandoSql = "UPDATE USUARIOS SET POSICAO_X = '" + pos_x + "' WHERE NOME = '" + nome + "';" +
                                "UPDATE USUARIOS SET POSICAO_Y = '" + pos_y + "' WHERE NOME = '" + nome + "';" +
                                "UPDATE USUARIOS SET POSICAO_Z = '" + pos_z + "' WHERE NOME = '" + nome + "';" +
                                "UPDATE USUARIOS SET ROTACAO_Y = '" + rot_y + "' WHERE NOME = '" + nome + "';";

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

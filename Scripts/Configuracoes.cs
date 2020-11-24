using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Configuracoes : MonoBehaviour
{
    public static string nomeUsuario;
    public Text nome;
    public static int numeroTotalItens = 4;


    public static string[] item = new string[4];


    private void Start()
    {
         item[0] = "ESFERA";
         item[1] = "CUBO";
         item[2] = "CAPSULA";
         item[3] = "CILINDRO";
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        AtualizarItem(item[1]);
    //    }
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        AtualizarItem(item[2]);
    //    }
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        AtualizarItem(item[3]);
    //    }
    //    if (Input.GetKeyDown(KeyCode.V))
    //    {
    //        AtualizarItem(item[4]);
    //    }
    //}
    public void Iniciar()
    {     
        BancoInventario bd = new BancoInventario();
        if (bd.consultarUsuario(nome.text) == 0)
        {
            if (bd.inserirScalar(nome.text))
            {
                print("Usuario Salvo com sucesso!");
            }
            else
            {
                print("Erro ao salvar Usuario!");
            }

            if (bd.inserir())
            {
                print("Inventario Salvo com sucesso!");
            }
            else
            {
                print("Erro ao salvar Inventario!");
            }
            nomeUsuario = nome.text;
            SceneManager.LoadScene("Jogo");
        }

        else print("Ja existe usuario com esse nome");

        
    }

    public void AtualizarItem(string item)
    {
        BancoInventario bd = new BancoInventario();
        if (bd.atualizar(item))
        {
            print("Alterado com sucesso!");
        }
        else
        {
            print("Erro ao alterar!");
        }
    }

    public void LoadGame()
    {
        BancoInventario bd = new BancoInventario();
        if (bd.consultarUsuario(nome.text) > 0)
        {
            nomeUsuario = nome.text;
            SceneManager.LoadScene("Jogo");
        }

        else print("Não existe usuario com esse nome");

    }


}

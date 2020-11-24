using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public Text jogador_txt;
    void Start()
    {
        jogador_txt.text = Configuracoes.nomeUsuario;
        DarLoad();
    }

    public void DarLoad()
    {
        BancoInventario bd = new BancoInventario();
        if (bd.consultar(Configuracoes.nomeUsuario))
        {
            print("Consultado com sucesso!");
        }
        else
        {
            print("Erro ao consultar!");
        }


    }
}

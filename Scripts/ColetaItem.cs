using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColetaItem : MonoBehaviour
{
    private string tagItem;
    void Start()
    {
        tagItem = gameObject.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AtualizaHUD();
            AtualizarItem(tagItem);
            Destroy(gameObject);
        }

    }
    void AtualizarItem(string item)
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

    void AtualizaHUD()
    {


        if (tagItem == "ESFERA")
        {
            InstanciaObjeto.esfera++;
        }
        else if (tagItem == "CUBO")
        {
            InstanciaObjeto.cubo++;
        }
        else if (tagItem == "CAPSULA")
        {
            InstanciaObjeto.capsula++;
        }
        else if (tagItem == "CILINDRO")
        {
            InstanciaObjeto.cilindro++;
        }

    }

}









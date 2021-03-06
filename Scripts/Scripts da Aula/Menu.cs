﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Text id;
    public Text nome;


    public void Cadastrar()
    {
        Banco bd = new Banco();
        if (bd.inserir(nome.text))
        {
            print("Salvo com sucesso!");
        }
        else
        {
            print("Erro ao salvar!");
        }
    }

    public void Alterar()
    {
        Banco bd = new Banco();
        if (bd.alterar(int.Parse(id.text), nome.text))
        {
            print("Alterado com sucesso!");
        }
        else
        {
            print("Erro ao alterar!");
        }
    }

    public void Remover()
    {
        Banco bd = new Banco();
        if (bd.remover(int.Parse(id.text)))
        {
            print("Removido com sucesso!");
        }
        else
        {
            print("Erro ao remover!");
        }
    }

    public void Consultar()
    {
        Banco bd = new Banco();
        if (bd.consultar())
        {
            print("Consultado com sucesso!");
        }
        else
        {
            print("Erro ao consultar!");
        }
    }
}

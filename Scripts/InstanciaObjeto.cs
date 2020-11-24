using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;


public class InstanciaObjeto : MonoBehaviour
{
    public static int esfera;
    public static int cubo;
    public static int cilindro;
    public static int capsula;

    public Text esfera_txt;
    public Text cubo_txt;
    public Text cilindro_txt;
    public Text capsula_txt;

    public Transform marcador1;
    public Transform marcador2;
    public GameObject[] item;
    public Vector3 spawnSpot = new Vector3(0, 2, 0);

    void Start()
    {
        StartCoroutine(SpawnaItem());
    }

    private void Update()
    {
        esfera_txt.text = esfera.ToString();
        capsula_txt.text = capsula.ToString();
        cilindro_txt.text = cilindro.ToString();
        cubo_txt.text = cubo.ToString();
    }

    IEnumerator SpawnaItem()
    {

        for (int i = 0; i < item.Length; i++)
        {
            int qualItem = Random.Range(0, item.Length);
            GameObject item_spawn = (GameObject)Instantiate(item[qualItem], spawnAleatorio(), transform.rotation);
        }
        yield return new WaitForSeconds(20);
        StartCoroutine(SpawnaItem());
    }

    Vector3 spawnAleatorio()
    {
        float x = Random.Range(marcador1.position.x, marcador2.position.x);
        float z = Random.Range(marcador1.position.z, marcador2.position.z);
        return new Vector3(x, 0.5f, z);
    }
}

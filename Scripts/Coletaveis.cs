using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletaveis : MonoBehaviour
{
    public static int coletaveisTotal = 4;
    public GameObject[] coletavel;
    public Transform marcador1;
    public Transform marcador2;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < coletavel.Length; i++)
        {
            float x = Random.Range(marcador1.position.x, marcador2.position.x);
            float z = Random.Range(marcador1.position.z, marcador2.position.z);
            coletavel[i].transform.position = new Vector3(x, 0.5f, z);
        }


    }
}

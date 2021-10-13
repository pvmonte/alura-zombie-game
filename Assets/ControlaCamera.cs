using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    public GameObject jogador;
    Vector3 dist = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        dist = transform.position - jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = jogador.transform.position + dist;
    }
}

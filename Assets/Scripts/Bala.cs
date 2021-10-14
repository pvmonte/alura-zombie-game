using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidade;
    Rigidbody rigidbodyBola;

    private void Start()
    {
        rigidbodyBola = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbodyBola.MovePosition(rigidbodyBola.position + transform.forward * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);
        }

        Destroy(gameObject);
    }
}

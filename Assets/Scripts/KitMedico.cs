using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    int quantidadeDeCura = 15;
    int tempoDeDestruicao = 5;

    private void Start()
    {
        Destroy(gameObject, tempoDeDestruicao);
    }

    private void OnTriggerEnter(Collider objetoDeColisão)
    {
        if(objetoDeColisão.tag == "Jogador")
        {
            objetoDeColisão.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
    }
}

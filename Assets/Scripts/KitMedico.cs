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

    private void OnTriggerEnter(Collider objetoDeColis�o)
    {
        if(objetoDeColis�o.tag == "Jogador")
        {
            objetoDeColis�o.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
    }
}

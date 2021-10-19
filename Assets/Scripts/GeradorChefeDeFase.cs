using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefeDeFase : MonoBehaviour
{
    float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject chefePrefab;

    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Instantiate(chefePrefab, transform.position, Quaternion.identity);
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }
}

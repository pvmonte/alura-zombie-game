using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefeDeFase : MonoBehaviour
{
    float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject chefePrefab;

    ControlaInterface scriptControlaInterface;

    public Transform[] posicoesPossíveisDeGeracao;
    Transform jogador;

    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;

        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(chefePrefab, posicaoDeCriacao, Quaternion.identity);
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
            scriptControlaInterface.AparecerTextoChefeCriado();
        }
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach (var posicao in posicoesPossíveisDeGeracao)
        {
            float distanciaEnrteJogador = Vector3.Distance(posicao.position, jogador.position);

            if(distanciaEnrteJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEnrteJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }

        return posicaoDeMaiorDistancia;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlaChefe : MonoBehaviour , IMatavel
{
    Transform jogador;
    NavMeshAgent agente;
    Status statuChefe;
    AnimacaoPersonagem animacaoChefe;
    MovimentoPersonagem movimentaChefe;
    public GameObject kitMedico;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        agente = GetComponent<NavMeshAgent>();
        statuChefe = GetComponent<Status>();
        agente.speed = statuChefe.velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentaChefe = GetComponent<MovimentoPersonagem>();
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath)
        {
            bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

            if (estouPertoDoJogador)
            {
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentaChefe.Rotacionar(direcao);
            }
            else
            {
                animacaoChefe.Atacar(false);
            }
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statuChefe.vida -= dano;

        if(statuChefe.vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        movimentaChefe.Morrer();
        enabled = false;
        agente.enabled = false;
        Instantiate(kitMedico, transform.position, Quaternion.identity);
    }
}

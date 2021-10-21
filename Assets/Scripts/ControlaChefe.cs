using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour , IMatavel
{
    Transform jogador;
    NavMeshAgent agente;
    Status statusChefe;
    AnimacaoPersonagem animacaoChefe;
    MovimentoPersonagem movimentaChefe;
    public GameObject kitMedico;

    public Slider sliderVidaChefe;
    public Image imagemSlider;
    public Color corVidaMaxima, corVidaMinima;
    public GameObject particulaSangueZumbi;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        agente.speed = statusChefe.velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentaChefe = GetComponent<MovimentoPersonagem>();
        sliderVidaChefe.maxValue = statusChefe.vidaInicial;
        AtualizarInterface();
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
        statusChefe.vida -= dano;
        AtualizarInterface();

        if(statusChefe.vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(particulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        movimentaChefe.Morrer();
        enabled = false;
        agente.enabled = false;
        Instantiate(kitMedico, transform.position, Quaternion.identity);
    }

    void AtualizarInterface()
    {
        sliderVidaChefe.value = statusChefe.vida;

        float porcentagemDaVida = (float)statusChefe.vida / statusChefe.vidaInicial;
        imagemSlider.color = Color.Lerp(corVidaMinima, corVidaMaxima, porcentagemDaVida);
    }
}

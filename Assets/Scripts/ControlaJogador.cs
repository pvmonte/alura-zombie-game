using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour , IMatavel , ICuravel
{
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;

    MovimentaJogador meuMovimentaJogador;
    AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    private void Start()
    {
        statusJogador = GetComponent<Status>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        meuMovimentaJogador = GetComponent<MovimentaJogador>();
        print(meuMovimentaJogador);
    }

    // Update is called once per frame
    void Update()
    {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);
    }

    void FixedUpdate()
    {
        meuMovimentaJogador.Movimentar(direcao, statusJogador.velocidade);

        meuMovimentaJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano (int dano)
    {
        statusJogador.vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if(statusJogador.vida <= 0)
        {
            Morrer();
        }        
    }

    public void Morrer()
    {        
        scriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusJogador.vida += quantidadeDeCura;

        if(statusJogador.vida > statusJogador.vidaInicial)
        {
            statusJogador.vida = statusJogador.vidaInicial;
        }

        scriptControlaInterface.AtualizarSliderVidaJogador();
    }
}

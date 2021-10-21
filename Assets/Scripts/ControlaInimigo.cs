using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    public GameObject Jogador;

    MovimentoPersonagem movimentaInimigo;
    AnimacaoPersonagem animacaoInimigo;
    Status statusInimigo;

    public AudioClip somDeMorte;
    Vector3 posicaoAleatoria;
    Vector3 direcao;

    float contadorVagar;
    float tempoEntrePosicoesAleatorias;

    float porcentagemGerarKitMedico = 0.1f;
    public GameObject kitMedico;

    public ControlaInterface scriptControlaInterface;
    [HideInInspector] public GeradorZumbis meuGerador;

    public GameObject particulaSangueZumbi;

    // Use this for initialization
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        AleatorizarZumbi();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<Status>();
        scriptControlaInterface = FindObjectOfType<ControlaInterface>();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        direcao = Jogador.transform.position - transform.position;

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);


        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            direcao = Jogador.transform.position - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.velocidade);

            animacaoInimigo.Atacar(false);
        }
        else
        {
            animacaoInimigo.Atacar(true);
        }
    }

    private void Vagar()
    {
        contadorVagar -= Time.deltaTime;

        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }

        bool ficouPertoSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05f;

        if (!ficouPertoSuficiente)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.velocidade);
        }

    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    private void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.vida -= dano;

        if (statusInimigo.vida <= 0)
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
        Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        enabled = false;
        ControlaAudio.instancia.PlayOneShot(somDeMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
        movimentaInimigo.Morrer();
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if(Random.value <= porcentagemGeracao)
        {
            Instantiate(kitMedico, transform.position, Quaternion.identity);
        }
    }
}

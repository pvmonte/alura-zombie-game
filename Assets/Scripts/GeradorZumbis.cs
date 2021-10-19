using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask layerZumbi;

    float distanciarDeGeracao = 3;
    float distanciaDoJogadorParaGeracao = 20;

    private GameObject jogador;

    int quantidadeMaximaDezumbisVivos = 2;
    int quantidadeDeZumbisVivos;
    float tempoProximoAumentoDeDificuldade = 30;
    float contadorDeAumentarDificuldade;

    // Use this for initialization
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");

        for (int i = 0; i < quantidadeMaximaDezumbisVivos; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if (possoGerarZumbisPelaDistancia && quantidadeDeZumbisVivos < quantidadeMaximaDezumbisVivos)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;

        if(Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
        {
            quantidadeDeZumbisVivos++;
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciarDeGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        var posicaoDeCriacao = AleatorizarPosição();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, layerZumbi);

        while (colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosição();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, layerZumbi);
            yield return null;
        }

        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        quantidadeDeZumbisVivos++;
    }

    Vector3 AleatorizarPosição()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciarDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos()
    {
        quantidadeDeZumbisVivos--;
    }
}

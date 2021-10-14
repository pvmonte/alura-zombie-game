using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject jogador;
    public float velocidade = 5;

    Rigidbody rigidbodyInimigo;
    Animator animatorInimigo;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);

        rigidbodyInimigo = GetComponent<Rigidbody>();
        animatorInimigo = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        Vector3 direcao = jogador.transform.position - transform.position;

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyInimigo.MoveRotation(novaRotacao);

        if (distancia > 2.5f)
        {
            rigidbodyInimigo.MovePosition(rigidbodyInimigo.position + direcao.normalized * velocidade * Time.deltaTime);

            animatorInimigo.SetBool("atacando", false);
        }
        else
        {
            animatorInimigo.SetBool("atacando", true);
        }
    }

    void AtacaJogador()
    {
        Time.timeScale = 0;
        Jogador jogadorScript = jogador.GetComponent<Jogador>();
        jogadorScript.textoGameOver.SetActive(true);
        jogadorScript.vivo = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour
{
    public Animator anim;
    public float speed;
    Vector3 direcao = new Vector3();
    public LayerMask mascaraDoChao;

    public GameObject textoGameOver;
    public bool vivo = true;
    Rigidbody rigidbodyJogador;
    Animator animatorJogador;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
        {
            animatorJogador.SetBool("running", true);
        }
        else
        {
            animatorJogador.SetBool("running", false);
        }

        if (vivo == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    private void FixedUpdate()
    {
        rigidbodyJogador.MovePosition(rigidbodyJogador.position + (direcao * speed * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if(Physics.Raycast(raio, out impacto, 100, mascaraDoChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            rigidbodyJogador.MoveRotation(novaRotacao);
        }

        
    }
}

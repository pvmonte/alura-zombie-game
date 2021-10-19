using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    Animator meuAnimator;

    private void Awake()
    {
        meuAnimator = GetComponent<Animator>();
    }

    public void Atacar(bool estado)
    {
        meuAnimator.SetBool("Atacando", estado);
    }

    public void Movimentar(float valorDoMovimento)
    {
        meuAnimator.SetFloat("Movendo", valorDoMovimento);
    }

    public void Morrer()
    {
        meuAnimator.SetTrigger("Morrer");
    }
}

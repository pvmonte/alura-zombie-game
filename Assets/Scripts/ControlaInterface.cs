using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;

    public GameObject painelDeGameover;
    public Text textoTempoDeSobrevivencia;
    public Text textoTempoPontuacaoMaxima;
    float tempoPontuacaoSalvo;

    int quantidadeDeZumbisMortos;
    public Text textoQuantidadeZumbisMortos;

    public Text textoChefeAparece;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        scriptControlaJogador = GameObject.FindWithTag("Jogador")
                                .GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.vida;
        AtualizarSliderVidaJogador();
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos()
    {
        quantidadeDeZumbisMortos++;
        textoQuantidadeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        painelDeGameover.SetActive(true);

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        textoTempoDeSobrevivencia.text = "Você sobreviveu por " + minutos + "min e " + segundos + "s";
        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima(int minutos, int segundos)
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            textoTempoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", minutos, segundos);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }

        if (textoTempoPontuacaoMaxima.text == "")
        {
            minutos = (int)(tempoPontuacaoSalvo / 60);
            segundos = (int)(tempoPontuacaoSalvo % 60);
            textoTempoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", minutos, segundos);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }

    public void AparecerTextoChefeCriado()
    {
        StartCoroutine(DesaparecerText(1, textoChefeAparece));
    }

    IEnumerator DesaparecerText(float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);

        print("text");
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        
        yield return new WaitForSeconds(tempoDeSumico);
        float contador = 0;

        while (textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;

            if (textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }

            yield return null;
        }

    }
}

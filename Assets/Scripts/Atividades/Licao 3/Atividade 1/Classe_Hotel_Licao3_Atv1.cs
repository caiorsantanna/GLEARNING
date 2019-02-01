using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Hotel_Licao3_Atv1 : MonoBehaviour
{
    public GameObject canvas_botao, btn_hotel, pnl_certeza_hotel, pnl_acertos, pnl_tudo;

    public Image estrela_1, estrela_2, estrela_3;
    public Sprite img_sim, img_nao;

    public bool hotel_certo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas_botao.SetActive(true);
        btn_hotel.SetActive(true);
        pnl_certeza_hotel.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvas_botao.SetActive(false);
        btn_hotel.SetActive(true);
        pnl_certeza_hotel.SetActive(false);
    }

    public void Metodo_Escolher_Hotel(){
        Classe_Controle_Licao3_Atv1 controle = new Classe_Controle_Licao3_Atv1();
        pnl_tudo.SetActive(false);
        pnl_acertos.SetActive(true);
        if (hotel_certo)
        {
            estrela_1.sprite = img_sim;
            estrela_2.sprite = img_sim;
            estrela_3.sprite = img_sim;
            controle.Metodo_Adicionar_Pontos_Licao3_Atv1();
        }
        else
        {
            estrela_1.sprite = img_nao;
            estrela_2.sprite = img_nao;
            estrela_3.sprite = img_nao;
        }
        Time.timeScale = 0f;
    }    
}

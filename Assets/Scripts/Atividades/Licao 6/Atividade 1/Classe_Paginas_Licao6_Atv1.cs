using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Paginas_Licao6_Atv1 : MonoBehaviour
{
    public Button btn_direita, btn_esquerda;
    public GameObject pnl_fotos_1, pnl_fotos_2, pnl_fotos_3, pnl_fotos_4;

    public void Metodo_Iniciar_Paginas()
    {
        Classe_Controle_Licao6_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao6_Atv1>();

        switch (controle.nivel)
        {
            case 1:
                pnl_fotos_1.SetActive(true);
                pnl_fotos_2.SetActive(false);
                pnl_fotos_3.SetActive(false);
                pnl_fotos_4.SetActive(false);
                btn_direita.interactable = true;
                btn_esquerda.interactable = false;
                break;
            case 2:
                pnl_fotos_1.SetActive(true);
                pnl_fotos_2.SetActive(true);
                pnl_fotos_3.SetActive(false);
                pnl_fotos_4.SetActive(false);
                btn_direita.interactable = true;
                btn_esquerda.interactable = false;
                break;
            case 3:
                pnl_fotos_1.SetActive(true);
                pnl_fotos_2.SetActive(true);
                pnl_fotos_3.SetActive(false);
                pnl_fotos_4.SetActive(false);
                btn_direita.interactable = true;
                btn_esquerda.interactable = false;
                break;
            default:
                btn_direita.interactable = false;
                btn_esquerda.interactable = false;
                break;
        }
    }

    public void Metodo_Virar_Direita()
    {
        Classe_Controle_Licao6_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao6_Atv1>();        
        btn_direita.interactable = false;
        btn_esquerda.interactable = true;

        pnl_fotos_1.SetActive(false);
        pnl_fotos_2.SetActive(false);
        pnl_fotos_3.SetActive(true);
        if (controle.nivel == 3) pnl_fotos_4.SetActive(true); else pnl_fotos_4.SetActive(false);        
    }

    public void Metodo_Virar_Esquerda()
    {               
        btn_direita.interactable = true;
        btn_esquerda.interactable = false;

        pnl_fotos_1.SetActive(true);
        pnl_fotos_2.SetActive(true);
        pnl_fotos_3.SetActive(false);
        pnl_fotos_4.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Phone_Licao3_Atv1 : MonoBehaviour
{
    public GameObject canvas_botao, btn_phone, pnl_certeza;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas_botao.SetActive(true);
        btn_phone.SetActive(true);
        pnl_certeza.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvas_botao.SetActive(false);
        btn_phone.SetActive(true);
        pnl_certeza.SetActive(false);
    }

    public void Metodo_Receber_Dica()
    {

    }
}

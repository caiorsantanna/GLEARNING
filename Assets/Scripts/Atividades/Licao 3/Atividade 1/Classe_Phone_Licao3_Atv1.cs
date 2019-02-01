using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Phone_Licao3_Atv1 : MonoBehaviour
{
    public GameObject canvas_botao, btn_phone, pnl_certeza, pnl_dica, pnl_tudo;
    public Text txt_dica, txt_dicas;

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
        Classe_Controle_Licao3_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao3_Atv1>();

        pnl_tudo.SetActive(false);
        pnl_dica.SetActive(true);

        if(controle.moedas > 0)
        {
            int rNDica = Random.Range(0, controle.dicas.ToArray().Length);
            txt_dica.text = controle.dicas[rNDica];
            txt_dicas.text = txt_dicas.text + "- " + controle.dicas[rNDica] + "\n";
            controle.dicas.RemoveAt(rNDica);

            controle.moedas = controle.moedas - 1;
        }
        else
        {
            txt_dica.text = "You're out of coins!";
        }
    }
}

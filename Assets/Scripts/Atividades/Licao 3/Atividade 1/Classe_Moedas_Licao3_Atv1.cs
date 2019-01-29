using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Moedas_Licao3_Atv1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Classe_Controle_Licao3_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao3_Atv1>();
        controle.moedas = controle.moedas + 1;
        this.gameObject.SetActive(false);
    }
}

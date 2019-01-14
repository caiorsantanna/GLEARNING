using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Chefe_Licao2_Atv1 : MonoBehaviour
{

    public GameObject canvas_dialogo_começo;
    public GameObject canvas_botao_chefe;
    
    void Start()
    {
        canvas_dialogo_começo.SetActive(true);
    }

    void OnTriggerEnter2D()
    {
        canvas_botao_chefe.SetActive(true);
    }

    void OnTriggerExit2D()
    {
        canvas_botao_chefe.SetActive(false);
    }
}

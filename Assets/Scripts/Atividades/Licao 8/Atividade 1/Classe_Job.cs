using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Job : MonoBehaviour
{
    public Classe_Controle_Licao8_Atv1 control;
    public string nome_1, idade_1, nome_2, idade_2, incorrect_description, correct_description;

    void Start()
    {
        control = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao8_Atv1>();

        int randomNomeIndex = Random.Range(0, control.nomes.Count);
        int randomIdadeIndex = Random.Range(0, control.idades.Count);

        nome_1 = control.nomes[randomIdadeIndex];
        idade_1 = control.idades[randomIdadeIndex];

        control.nomes.RemoveAt(randomNomeIndex);
        control.idades.RemoveAt(randomIdadeIndex);

        randomNomeIndex = Random.Range(0, control.nomes.Count);
        randomIdadeIndex = Random.Range(0, control.idades.Count);

        nome_2 = control.nomes[randomIdadeIndex];
        idade_2 = control.idades[randomIdadeIndex];
    }
}

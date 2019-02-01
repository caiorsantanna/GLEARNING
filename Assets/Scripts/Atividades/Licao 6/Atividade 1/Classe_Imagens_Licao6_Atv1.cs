using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Imagens_Licao6_Atv1 : MonoBehaviour
{
    public List<Sprite> fotos = new List<Sprite>();
    public GameObject pnl_foto_prefab, pnl_conteudo, ponto_inicial, pnl_imagem;



    public void Metodo_Abrir_Selecao_Fotos(int i)
    {
        fotos.AddRange(Resources.LoadAll<Sprite>("L6_A1/Imagens/"));

        float xFoto = 0;
        float yFoto = 0;
        int contador = 0;
        int acumulador = 5;

        for (int j = 0; j < fotos.ToArray().Length; j++)
        {
            if (contador == acumulador)
            {
                GameObject img = Instantiate(pnl_foto_prefab, new Vector3(ponto_inicial.transform.position.x, ponto_inicial.transform.position.y + yFoto, 0), Quaternion.identity, pnl_conteudo.transform);
                img.transform.Find("img_foto").GetComponent<Image>().sprite = fotos[j];
                img.GetComponent<Button>().onClick.RemoveAllListeners();
                img.GetComponent<Button>().onClick.AddListener(() => { this.Metodo_Selecionar_Foto(i, img.transform.Find("img_foto").GetComponent<Image>().sprite); });

                xFoto = 0;
                yFoto = yFoto - (img.GetComponent<RectTransform>().rect.height + 10);
                acumulador = acumulador + 5;

            }
            else
            {
                GameObject img = Instantiate(pnl_foto_prefab, new Vector3(ponto_inicial.transform.position.x + xFoto, ponto_inicial.transform.position.y + yFoto, 0), Quaternion.identity, pnl_conteudo.transform);
                img.transform.Find("img_foto").GetComponent<Image>().sprite = fotos[j];
                img.GetComponent<Button>().onClick.RemoveAllListeners();
                img.GetComponent<Button>().onClick.AddListener(() => { this.Metodo_Selecionar_Foto(i, img.transform.Find("img_foto").GetComponent<Image>().sprite); });

                xFoto = xFoto + (img.GetComponent<RectTransform>().rect.width + 10);
                //yFoto = yFoto - (img.GetComponent<RectTransform>().rect.height + 10);
                contador++;
            }

        }

        fotos.Clear();
    }

    public void Metodo_Fechar()
    {
        foreach (Transform child in pnl_conteudo.transform)
        {
            if(child.name != "ponto_inicial")
            GameObject.Destroy(child.gameObject);
        }
    }

    public void Metodo_Selecionar_Foto(int i, Sprite img_selecionada)
    {        
        GameObject.Find("img_foto_" + i).GetComponent<Image>().sprite = img_selecionada;
        pnl_imagem.SetActive(false);
        Metodo_Fechar();
    }
}

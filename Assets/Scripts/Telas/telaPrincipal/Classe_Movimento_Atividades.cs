using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Movimento_Atividades : MonoBehaviour {

    float offsetx, offsety;
    float offsetx_tudo, offsety_tudo;

    public void Metodo_Começar_Mover()
    {
        offsetx = this.transform.position.x - Input.mousePosition.x;
        offsety = this.transform.position.y - Input.mousePosition.y;
        offsetx_tudo = GameObject.Find("pnl_tudo").transform.position.x - Input.mousePosition.x;
        offsety_tudo = GameObject.Find("pnl_tudo").transform.position.y - Input.mousePosition.y;
    }

    public void MoverUIHorizontal()
    {
        if(GetComponent<RectTransform>().anchoredPosition.x >= 4)
        {
            this.transform.position = new Vector3(offsetx + Input.mousePosition.x, this.transform.position.y, 0);
            if (this.name != "pnl_tudo")
            {
                GameObject.Find("pnl_tudo").transform.position = new Vector3(GameObject.Find("pnl_tudo").transform.position.x, offsety_tudo + Input.mousePosition.y, 0);
            }
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(4, GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    public void MoverUIVertical()
    {
        this.transform.position = new Vector3(this.transform.position.x, offsety + Input.mousePosition.y, 0);
    }
}

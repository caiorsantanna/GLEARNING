using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao_Simples_Objetos_Vertical : MonoBehaviour
{
    float XInicial, YInicial, XFinal, YFinal;
    Vector3 final;
    bool posicao;

    void Start()
    {
        XInicial = this.transform.position.x;
        YInicial = this.transform.position.y;
        XFinal = this.transform.position.x;
        YFinal = this.transform.position.y+1;
        final = new Vector3(XFinal, YFinal, this.transform.position.z);
    }

    void Update()
    {
        
        if (this.transform.position.y == YInicial+0.8f)
        {            
            final = new Vector3(XFinal, YFinal, this.transform.position.z);            
        } else if (this.transform.position.y == YFinal-.8f)
        {          
            final = new Vector3(XInicial, YInicial, this.transform.position.z);            
        }

        this.transform.position = Vector3.Lerp(this.transform.position, final, Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Evento_Licao4_Atv1 : MonoBehaviour
{
    //VARIAVEIS REPRESENTANDO AS ETAPAS RELATIVAS A CADA EVENTO
    public string evento_task, etapa_1, etapa_2, etapa_3;
    public bool e_pra_fazer = false;

    public Dropdown dpw_ordem1, dpw_ordem2, dpw_ordem3;
    public Text txt_ordem1, txt_ordem2, txt_ordem3;
    public Button btn_terminar;

    public GameObject pnl_evento_errado, canvas_botao, pnl_evento, pnl_tudo, pnl_erro_ordem;

    public Dictionary<string, string> ordem_etapas = new Dictionary<string, string>();
    public Dictionary<string, string> ordem_respondida = new Dictionary<string, string>();

    void Start()
    {
        ordem_etapas.Add("1", etapa_1);
        ordem_etapas.Add("2", etapa_2);
        ordem_etapas.Add("3", etapa_3);
    }

    public void Metodo_Iniciar_Evento(string i)
    {
        Classe_Controle_Licao4_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao4_Atv1>();

        btn_terminar.onClick.RemoveAllListeners();
        btn_terminar.onClick.AddListener(() => { this.Metodo_Concluir_Tarefa(); });

        if (e_pra_fazer)
        {            

            pnl_evento.SetActive(true);
            pnl_tudo.SetActive(false);

            List<Text> ordens = new List<Text>();
            ordens.Add(txt_ordem1);
            ordens.Add(txt_ordem2);
            ordens.Add(txt_ordem3);

            List<string> textos = new List<string>();
            textos.Add(controle.eventos_questao[i].GetComponent<Classe_Evento_Licao4_Atv1>().etapa_1);
            textos.Add(controle.eventos_questao[i].GetComponent<Classe_Evento_Licao4_Atv1>().etapa_2);
            textos.Add(controle.eventos_questao[i].GetComponent<Classe_Evento_Licao4_Atv1>().etapa_3);

            int rNOrdem, rNTexto;
            for(int j = 0; j < 3; j++)
            {               
                rNOrdem = Random.Range(0, ordens.ToArray().Length);
                rNTexto = Random.Range(0, textos.ToArray().Length);
                
                ordens[rNOrdem].text = textos[rNTexto];

                ordens.RemoveAt(rNOrdem);
                textos.RemoveAt(rNTexto);

            }            
        }
        else
        {
            pnl_evento_errado.SetActive(true);
        }
    }

    public void Metodo_Concluir_Tarefa()
    {
        Classe_Controle_Licao4_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao4_Atv1>();

        try
        {
            ordem_respondida.Clear();
            ordem_respondida.Add(dpw_ordem1.captionText.text, txt_ordem1.text);
            ordem_respondida.Add(dpw_ordem2.captionText.text, txt_ordem2.text);
            ordem_respondida.Add(dpw_ordem3.captionText.text, txt_ordem3.text);

            if ((ordem_respondida["1"] == ordem_etapas["1"]) && (ordem_respondida["2"] == ordem_etapas["2"]) && (ordem_respondida["3"] == ordem_etapas["3"]))
            {
                controle.acertos_erros[this.name] = true;
            }
            else
            {
                controle.acertos_erros[this.name] = false;
            }            

            this.e_pra_fazer = false;
            controle.atv_feitas++;
            pnl_tudo.SetActive(true);
            pnl_evento.SetActive(false);
        }
        catch
        {
            pnl_erro_ordem.SetActive(true);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas_botao.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvas_botao.SetActive(false);
    }
}

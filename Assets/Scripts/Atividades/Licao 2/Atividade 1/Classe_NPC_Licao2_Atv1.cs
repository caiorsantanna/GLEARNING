using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                           

public class Classe_NPC_Licao2_Atv1 : MonoBehaviour
{
    public int id;
    public string nome;
    public string descricao;
    public string tag_empresa;
    public string posicao;
    public Sprite logo_1;
    public Sprite logo_2;
    public Sprite logo_3;       

    BoxCollider2D colisor;
    SpriteRenderer base_npc;
    public SpriteRenderer roupa_npc;
    public SpriteRenderer cabelo_npc;

    GameObject canvas;
    public GameObject canvas_dialogo, canvas_botao;
    public Image img_logo1_botao, img_logo1_dialogo, img_logo2_botao, img_logo2_dialogo, img_logo3_botao, img_logo3_dialogo;
    public Text txt_dialogo, txt_nome;    

    void Start()
    {
        Classe_Controle_Licao2_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao2_Atv1>();
        colisor = GetComponent<BoxCollider2D>();
        base_npc = GetComponent<SpriteRenderer>();

        canvas = GameObject.Find("Canvas").gameObject;

        //GERAR TAG
        int rTag = Random.Range(0, controle.tags.Count);
        this.tag_empresa = controle.tags[rTag];
        controle.tags.RemoveAt(rTag);

        //GERAR NOME EMPRESA        
        this.nome = controle.nomes_empresas[this.tag_empresa] ;
        controle.nomes_empresas.Remove(this.tag_empresa);          

        //GERAR DESCRICOES        
        this.descricao = controle.descricoes_empresas[this.tag_empresa];
        controle.descricoes_empresas.Remove(this.tag_empresa);

        //GERAR LOGO
        int rLogo = Random.Range(0, controle.logos_1.Count);
        this.logo_1 = Resources.Load<Sprite>("L2_A1/" + controle.logos_1[rLogo]);        

        rLogo = Random.Range(0, controle.logos_2.Count);
        this.logo_2 = Resources.Load<Sprite>("L2_A1/" + controle.logos_2[rLogo]);        
        
        this.logo_3 = Resources.Load<Sprite>("L2_A1/" + controle.logos_3[this.tag_empresa]);
        controle.logos_3.Remove(this.tag_empresa);

        img_logo1_botao.sprite = logo_1;
        img_logo2_botao.sprite = logo_2;
        img_logo3_botao.sprite = logo_3;
        img_logo1_dialogo.sprite = logo_1;
        img_logo2_dialogo.sprite = logo_2;
        img_logo3_dialogo.sprite = logo_3;

        //GERAR SEXO NPC
        int rSexo = Random.Range(0, 2);

        //MUDA IMAGEM E COLISAO DE ACORDO COM POSICAO
        int rRoupa, rCabelo;
        for(int i = 0; i < controle.posicoes_possiveis.GetLength(0); i++)
        {
            
            if(this.transform.position.ToString() == controle.posicoes_possiveis[i, 1].ToString())
            {
                switch(controle.posicoes_possiveis[i, 0])
                {
                    case "CIMA":
                        posicao = "CIMA";
                        //colisor.offset = new Vector2(2.980232e-08f, 0.08079089f);
                        //colisor.size = new Vector2(0.7382117f, 0.4815818f);
                        switch (rSexo)
                        {
                            case 0:
                                rRoupa = Random.Range(0, controle.cima_r_m.Count);
                                rCabelo = Random.Range(0, controle.cima_c_m.Count);
                                
                                base_npc.sprite = controle.cima_m;
                                roupa_npc.sprite = controle.cima_r_m[rRoupa];
                                cabelo_npc.sprite = controle.cima_c_m[rCabelo];

                                controle.cima_r_m.RemoveAt(rRoupa);
                                controle.cima_c_m.RemoveAt(rCabelo);
                                break;
                            case 1:
                                rRoupa = Random.Range(0, controle.cima_r_f.Count);
                                rCabelo = Random.Range(0, controle.cima_c_f.Count);
                                
                                base_npc.sprite = controle.cima_f;
                                roupa_npc.sprite = controle.cima_r_f[rRoupa];
                                cabelo_npc.sprite = controle.cima_c_f[rCabelo];

                                controle.cima_r_f.RemoveAt(rRoupa);
                                controle.cima_c_f.RemoveAt(rCabelo);
                                break;
                        }
                        break;
                    case "BAIXO":
                        posicao = "BAIXO";
                        //colisor.offset = new Vector2(2.980232e-08f, -0.08292091f);
                        //colisor.size = new Vector2(0.7382117f, 0.5507559f);
                        switch (rSexo)
                        {
                            case 0:
                                rRoupa = Random.Range(0, controle.baixo_r_m.Count);
                                rCabelo = Random.Range(0, controle.baixo_c_m.Count);
                                
                                base_npc.sprite = controle.baixo_m;
                                roupa_npc.sprite = controle.baixo_r_m[rRoupa];
                                cabelo_npc.sprite = controle.baixo_c_m[rCabelo];

                                controle.baixo_r_m.RemoveAt(rRoupa);
                                controle.baixo_c_m.RemoveAt(rCabelo);
                                break;
                            case 1:
                                rRoupa = Random.Range(0, controle.baixo_r_f.Count);
                                rCabelo = Random.Range(0, controle.baixo_c_f.Count);
                                
                                base_npc.sprite = controle.baixo_f;
                                roupa_npc.sprite = controle.baixo_r_f[rRoupa];
                                cabelo_npc.sprite = controle.baixo_c_f[rCabelo];

                                controle.baixo_r_f.RemoveAt(rRoupa);
                                controle.baixo_c_f.RemoveAt(rCabelo);
                                break;
                        }
                        break;
                    default:
                        print("ERRO CIMA BAIXO");
                        break;
                }                
            }
            
        }

        txt_nome.text = this.nome;
        txt_dialogo.text = this.descricao;

    }

    void OnTriggerEnter2D()
    {
        canvas_botao.SetActive(true);
    }

    void OnTriggerExit2D()
    {
        canvas_botao.SetActive(false);
    }

    public void Metodo_Abrir_Dialogo()
    {
        canvas.SetActive(false);
        canvas_dialogo.SetActive(true);
    }

    public void Metodo_Fechar_Dialogo()
    {
        canvas.SetActive(true);
        canvas_dialogo.SetActive(false);
    }
}

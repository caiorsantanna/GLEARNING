using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                           

public class Classe_NPC_Licao2_Atv1 : MonoBehaviour
{
    int id;
    string nome;
    string descricao;
    string tag_empresa;
    string posicao;
    Sprite logo_1;
    Sprite logo_2;
    Sprite logo_3;       

    BoxCollider2D colisor;
    SpriteRenderer base_npc;
    public SpriteRenderer roupa_npc;
    public SpriteRenderer cabelo_npc;


    public GameObject canvas;
    public Image img_logo_1, img_logo_2, img_logo_3;    

    GameObject pnl_tudo;
    public GameObject pnl_dialogo;
    public Text txt_dialogo, txt_id;
    public Image img_dialogo;    

    Sprite sprite_dialogo;

    public int Id { get => id; set => id = value; }
    public string Nome { get => nome; set => nome = value; }
    public string Descricao { get => descricao; set => descricao = value; }
    public string Tag_empresa { get => tag_empresa; set => tag_empresa = value; }
    public Sprite Logo_1 { get => logo_1; set => logo_1 = value; }
    public Sprite Logo_2 { get => logo_2; set => logo_2 = value; }
    public Sprite Logo_3 { get => logo_3; set => logo_3 = value; }    

    void Start()
    {
        Classe_Controle_Licao2_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao2_Atv1>();
        colisor = GetComponent<BoxCollider2D>();
        base_npc = GetComponent<SpriteRenderer>();

        pnl_tudo = GameObject.Find("pnl_tudo").gameObject;

        //GERAR TAG
        int rTag = Random.Range(0, controle.tags.Count);
        this.Tag_empresa = controle.tags[rTag];
        controle.tags.RemoveAt(rTag);

        //GERAR NOME EMPRESA        
        this.Nome = controle.nomes_empresas[this.Tag_empresa] ;
        controle.nomes_empresas.Remove(this.Tag_empresa);          

        //GERAR DESCRICOES        
        this.Descricao = controle.descricoes_empresas[this.Tag_empresa];
        controle.descricoes_empresas.Remove(this.Tag_empresa);

        //GERAR LOGO
        int rLogo = Random.Range(0, controle.logos_1.Count);
        this.Logo_1 = Resources.Load<Sprite>("L2_A1/" + controle.logos_1[rLogo]);
        controle.logos_1.RemoveAt(rLogo);

        rLogo = Random.Range(0, controle.logos_2.Count);
        this.Logo_2 = Resources.Load<Sprite>("L2_A1/" + controle.logos_2[rLogo]);
        controle.logos_2.RemoveAt(rLogo);
        
        this.Logo_3 = Resources.Load<Sprite>("L2_A1/" + controle.logos_3[this.Tag_empresa]);
        controle.logos_3.Remove(this.Tag_empresa);

        img_logo_1.sprite = logo_1;
        img_logo_2.sprite = logo_2;
        img_logo_3.sprite = logo_3;

        //GERAR SEXO NPC
        int rSexo = Random.Range(0, 2);

        //GERA MUDA IMAGEM E COLISAO DE ACORDO COM POSICAO
        int rRoupa, rCabelo;
        for(int i = 0; i < controle.posicoes_possiveis.GetLength(0); i++)
        {
            
            if(this.transform.position.ToString() == controle.posicoes_possiveis[i, 1].ToString())
            {
                switch(controle.posicoes_possiveis[i, 0])
                {
                    case "CIMA":
                        posicao = "CIMA";
                        colisor.offset = new Vector2(2.980232e-08f, 0.08079089f);
                        colisor.size = new Vector2(0.7382117f, 0.4815818f);
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
                        colisor.offset = new Vector2(2.980232e-08f, -0.08292091f);
                        colisor.size = new Vector2(0.7382117f, 0.5507559f);
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
                }                
            }
            
        }        

        //txt_dialogo.text = "Hello! My name is " + this.Nome + " " + this.Sobrenome + ", and I am " + this.Nascionalidade + "!";
        //img_dialogo.sprite = sprite_dialogo;

        //txt_id.text = this.Id.ToString();

    }

    void OnTriggerEnter2D()
    {
        canvas.SetActive(true);
    }

    void OnTriggerExit2D()
    {
        canvas.SetActive(false);
    }

    public void Metodo_Abrir_Dialogo()
    {
        //pnl_tudo.SetActive(false);
        //pnl_dialogo.SetActive(true);
    }

    public void Metodo_Fechar_Dialogo()
    {
        //pnl_tudo.SetActive(true);
        //pnl_dialogo.SetActive(false);
    }
}

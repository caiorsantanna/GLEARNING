using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_NPC_Licao1_Atv1 : MonoBehaviour {

	public int id;
    public string nome;
    public string sobrenome;
    public string nascionalidade;
    public string sexo;
    public string posicao;

    BoxCollider2D colisor;
    SpriteRenderer base_npc;
    public SpriteRenderer roupa_npc;
    public SpriteRenderer cabelo_npc;

    public GameObject canvas;

    GameObject pnl_tudo;
    public GameObject pnl_dialogo;
    public Text txt_dialogo, txt_id;
    public Image base_dialogo, roupa_dialogo, cabelo_dialogo;    

    void Start () {
        Classe_Controle_Licao1_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao1_Atv1>();
        colisor = GetComponent<BoxCollider2D>();
        base_npc = GetComponent<SpriteRenderer>(); 

        pnl_tudo = GameObject.Find("pnl_tudo").gameObject;

        //GERAR SEXO
        int rSexo = Random.Range(0, 2);
        switch (rSexo)
        {
            case 0:
                sexo = "M";
                break;
            case 1:
                sexo = "F";
                break;
        }

        //GERAR NOME
        int rNome;
		switch(rSexo){
			case 0:
				rNome = Random.Range(0,controle.nomes_masculinos.Count);
				nome = controle.nomes_masculinos[rNome];
				controle.nomes_masculinos.RemoveAt(rNome);
				break;
			case 1:
				rNome = Random.Range(0,controle.nomes_femininos.Count);
				nome = controle.nomes_femininos[rNome];
				controle.nomes_femininos.RemoveAt(rNome);
				break;
		}

		//GERAR SOBRENOME
		int rSobrenome;
		rSobrenome = Random.Range(0, controle.sobrenomes.Count);
		this.sobrenome = controle.sobrenomes[rSobrenome];
		controle.sobrenomes.RemoveAt(rSobrenome);

		//GERAR NASCIONALIDADE
		int rNascionalidade;
		rNascionalidade = Random.Range(0, controle.nascionalidades.Count);
		this.nascionalidade = controle.nascionalidades[rNascionalidade];
		controle.nascionalidades.RemoveAt(rNascionalidade);

        //MUDA IMAGEM E COLISAO DE ACORDO COM POSICAO
        int rRoupa, rCabelo;
        for (int i = 0; i < controle.posicoes_possiveis.GetLength(0); i++)
        {

            if (this.transform.position.ToString() == controle.posicoes_possiveis[i, 1].ToString())
            {
                switch (controle.posicoes_possiveis[i, 0])
                {
                    case "CIMA":
                        posicao = "CIMA";
                        colisor.offset = new Vector2(0.005240679f, 0.1643222f);
                        colisor.size = new Vector2(0.686008f, 0.6663014f);
                        base_npc.sortingOrder = 7;
                        cabelo_npc.sortingOrder = 8;
                        roupa_npc.sortingOrder = 8;
                        switch (rSexo)
                        {
                            case 0:
                                rRoupa = Random.Range(0, controle.cima_r_m.Count);
                                rCabelo = Random.Range(0, controle.cima_c_m.Count);

                                base_npc.sprite = controle.cima_m;
                                roupa_npc.sprite = controle.cima_r_m[rRoupa];
                                cabelo_npc.sprite = controle.cima_c_m[rCabelo];

                                base_dialogo.sprite = controle.cima_m;
                                roupa_dialogo.sprite = controle.cima_r_m[rRoupa];
                                cabelo_dialogo.sprite = controle.cima_c_m[rCabelo];

                                controle.cima_r_m.RemoveAt(rRoupa);
                                controle.cima_c_m.RemoveAt(rCabelo);
                                break;
                            case 1:
                                rRoupa = Random.Range(0, controle.cima_r_f.Count);
                                rCabelo = Random.Range(0, controle.cima_c_f.Count);

                                base_npc.sprite = controle.cima_f;
                                roupa_npc.sprite = controle.cima_r_f[rRoupa];
                                cabelo_npc.sprite = controle.cima_c_f[rCabelo];

                                base_dialogo.sprite = controle.cima_f;
                                roupa_dialogo.sprite = controle.cima_r_f[rRoupa];
                                cabelo_dialogo.sprite = controle.cima_c_f[rCabelo];

                                controle.cima_r_f.RemoveAt(rRoupa);
                                controle.cima_c_f.RemoveAt(rCabelo);
                                break;
                        }
                        break;
                    case "BAIXO":
                        posicao = "BAIXO";
                        colisor.offset = new Vector2(0.005240679f, -0.1368304f);
                        colisor.size = new Vector2(0.686008f, 0.6663016f);
                        base_npc.sortingOrder = 4;
                        cabelo_npc.sortingOrder = 5;
                        roupa_npc.sortingOrder = 5;
                        switch (rSexo)
                        {
                            case 0:
                                rRoupa = Random.Range(0, controle.baixo_r_m.Count);
                                rCabelo = Random.Range(0, controle.baixo_c_m.Count);

                                base_npc.sprite = controle.baixo_m;
                                roupa_npc.sprite = controle.baixo_r_m[rRoupa];
                                cabelo_npc.sprite = controle.baixo_c_m[rCabelo];

                                base_dialogo.sprite = controle.cima_m;
                                roupa_dialogo.sprite = controle.cima_r_m[rRoupa];
                                cabelo_dialogo.sprite = controle.cima_c_m[rCabelo];

                                controle.baixo_r_m.RemoveAt(rRoupa);
                                controle.baixo_c_m.RemoveAt(rCabelo);
                                break;
                            case 1:
                                rRoupa = Random.Range(0, controle.baixo_r_f.Count);
                                rCabelo = Random.Range(0, controle.baixo_c_f.Count);

                                base_npc.sprite = controle.baixo_f;
                                roupa_npc.sprite = controle.baixo_r_f[rRoupa];
                                cabelo_npc.sprite = controle.baixo_c_f[rCabelo];

                                base_dialogo.sprite = controle.cima_f;
                                roupa_dialogo.sprite = controle.cima_r_f[rRoupa];
                                cabelo_dialogo.sprite = controle.cima_c_f[rCabelo];

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

        txt_dialogo.text = "Hello! My name is "+nome+" "+sobrenome+", and I am "+nascionalidade+"!";        

        txt_id.text = id.ToString();
        
	}

    void OnTriggerEnter2D(){
        canvas.SetActive(true);
    }

    void OnTriggerExit2D(){
        canvas.SetActive(false);
    }

    public void Metodo_Abrir_Dialogo(){        
        pnl_tudo.SetActive(false);
        pnl_dialogo.SetActive(true);
    }

    public void Metodo_Fechar_Dialogo(){        
        pnl_tudo.SetActive(true);
        pnl_dialogo.SetActive(false);
    }
}

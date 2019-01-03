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
    Sprite logo_1;
    Sprite logo_2;
    Sprite logo_3;
    //PAROU AQUI
    //FAZER NPC
    //GERAR NOME EMPRESA, DESCRICAO, TAG, MECHER NO BANCO PARA FAZER 6 TAGS, LEMBRAR COMO ELE PEGA AS TAGS, GERAR O LOGO

    BoxCollider2D colisor;
    SpriteRenderer imagem;
    public Sprite cima_masculino, baixo_masculino;
    public Sprite cima_feminino, baixo_feminino;

    public GameObject canvas;

    GameObject pnl_tudo;
    public GameObject pnl_dialogo;
    public Text txt_dialogo, txt_id;
    public Image img_dialogo;

    Sprite sprite_dialogo;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }
    public string Sexo
    {
        get
        {
            return sexo;
        }

        set
        {
            sexo = value;
        }
    }

    public string Nome
    {
        get
        {
            return nome;
        }

        set
        {
            nome = value;
        }
    }
    public string Sobrenome
    {
        get
        {
            return sobrenome;
        }

        set
        {
            sobrenome = value;
        }
    }

    public string Nascionalidade
    {
        get
        {
            return nascionalidade;
        }

        set
        {
            nascionalidade = value;
        }
    }

    void Start()
    {
        Classe_Controle_Licao1_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao1_Atv1>();
        colisor = GetComponent<BoxCollider2D>();
        imagem = GetComponent<SpriteRenderer>();

        pnl_tudo = GameObject.Find("pnl_tudo").gameObject;

        //GERAR SEXO
        switch (Random.Range(0, 2))
        {
            case 0:
                this.sexo = "M";
                break;
            case 1:
                this.sexo = "F";
                break;
        }

        //GERAR NOME
        int rNome;
        switch (this.sexo)
        {
            case "M":
                rNome = Random.Range(0, controle.nomes_masculinos.Count);
                this.Nome = controle.nomes_masculinos[rNome];
                controle.nomes_masculinos.RemoveAt(rNome);
                break;
            case "F":
                rNome = Random.Range(0, controle.nomes_femininos.Count);
                this.Nome = controle.nomes_femininos[rNome];
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

        if (this.transform.position.y == 1f)
        {
            //BAIXO ESQUERDA
            if (this.transform.position.x == -35f)
            {
                colisor.offset = new Vector2(0.02005243f, -0.2839037f);
                colisor.size = new Vector2(1.195716f, 0.4543824f);
            }
            //BAIXO MEIO
            if (this.transform.position.x == -31f)
            {
                colisor.offset = new Vector2(0.2700756f, -0.2839037f);
                colisor.size = new Vector2(1.199918f, 0.4543824f);
            }
            //BAIXO DIREITA
            if (this.transform.position.x == -26f)
            {
                colisor.offset = new Vector2(0.1503165f, -0.2839037f);
                colisor.size = new Vector2(0.9604001f, 0.4543824f);
            }
            switch (this.sexo)
            {
                case "M":
                    imagem.sprite = baixo_masculino;
                    sprite_dialogo = cima_masculino;
                    break;
                case "F":
                    imagem.sprite = baixo_feminino;
                    sprite_dialogo = cima_feminino;
                    break;
            }

            imagem.sortingOrder = 5;

        }
        else
        {
            //CIMA ESQUERDA
            if (this.transform.position.x == -25f)
            {
                colisor.offset = new Vector2(-0.1034291f, 0.03831255f);
                colisor.size = new Vector2(0.9553266f, 0.4142821f);
            }
            //CIMA MEIO
            if (this.transform.position.x == -31f)
            {
                colisor.offset = new Vector2(0.2707059f, 0.03713453f);
                colisor.size = new Vector2(1.201179f, 0.4062266f);
            }
            //CIMA DIREITA
            if (this.transform.position.x == -34f)
            {
                colisor.offset = new Vector2(-0.2529879f, 0.03713453f);
                colisor.size = new Vector2(1.245321f, 0.4062266f);
            }
            switch (this.sexo)
            {
                case "M":
                    imagem.sprite = cima_masculino;
                    sprite_dialogo = cima_masculino;
                    break;
                case "F":
                    imagem.sprite = cima_feminino;
                    sprite_dialogo = cima_feminino;
                    break;
            }

            imagem.sortingOrder = 7;

        }

        txt_dialogo.text = "Hello! My name is " + this.Nome + " " + this.Sobrenome + ", and I am " + this.Nascionalidade + "!";
        img_dialogo.sprite = sprite_dialogo;

        txt_id.text = this.Id.ToString();

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
        pnl_tudo.SetActive(false);
        pnl_dialogo.SetActive(true);
    }

    public void Metodo_Fechar_Dialogo()
    {
        pnl_tudo.SetActive(true);
        pnl_dialogo.SetActive(false);
    }
}

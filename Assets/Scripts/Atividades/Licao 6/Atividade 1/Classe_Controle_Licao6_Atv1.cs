using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao6_Atv1 : MonoBehaviour
{
    public Image img_foto_1, img_foto_2, img_foto_3, img_foto_4, img_foto_5, img_foto_6, img_foto_7, img_foto_8;
    public Text txt_foto_1, txt_foto_2, txt_foto_3, txt_foto_4, txt_foto_5, txt_foto_6, txt_foto_7, txt_foto_8;

    public GameObject pnl_acertos;
    public Image img_estrela_1, img_estrela_2, img_estrela_3;
    public Sprite estrela_sim, estrela_nao;

    public List<Sprite> fotos = new List<Sprite>();
    public List<string> nomes = new List<string>();
    public List<string> nomes_a_popular = new List<string>();

    public int nivel = 1;
    int qtdFotos = 2;

    //VARIAVEIS DE BANCO
    MySqlCommand comando;
    MySqlDataReader dados;

    void Start()
    {
        //INICIALIZA AS CLASSES 'CONEXAO' E 'PLAYER'
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        //CONTATO COM O BANCO DE DADOS
        try
        {
            conexao.conectarBanco();            

            //COMANDO SQL NIVEL 2
            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=6";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();

            int rNFotoNome;
            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    int n = (int)dados["NIVEL_ATIVIDADE"];
                    if (n <= 10)
                    {
                        nivel = 1;
                        qtdFotos = 2;                        
                    }
                    else if (n <= 20)
                    {
                        nivel = 2;
                        qtdFotos = 4;                        
                    }
                    else
                    {
                        nivel = 3;
                        qtdFotos = 8;

                        
                    }
                }
            }
            

            fotos.AddRange(Resources.LoadAll<Sprite>("L6_A1/Imagens/"));

            for(int i = 0; i < fotos.ToArray().Length; i++)
            {
                nomes.Add(fotos[i].name);
                nomes_a_popular.Add(fotos[i].name);
            }


            switch (nivel)
            {
                case 1:
                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_1.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_2.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);
                    break;
                case 2:
                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_1.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_2.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_3.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_4.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);
                    break;
                case 3:
                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_1.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_2.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_3.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_4.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_5.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_6.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_7.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);

                    rNFotoNome = Random.Range(0, nomes_a_popular.ToArray().Length);
                    txt_foto_8.text = nomes_a_popular[rNFotoNome];
                    nomes_a_popular.RemoveAt(rNFotoNome);
                    break;
            }

            dados.Close();
            comando.Dispose();
        }
        catch (MySqlException e)
        {
            print(e);
        }
    }

    public void Metodo_Terminar()
    {
        int acertos = 0;
        string nome_img;
        switch (nivel)
        {
            case 1:
                nome_img = img_foto_1.sprite.name;
                if (nome_img == txt_foto_1.text) acertos++;
                nome_img = img_foto_2.sprite.name;
                if (nome_img == txt_foto_2.text) acertos++;
                break;

            case 2:
                nome_img = img_foto_1.sprite.name;
                if (nome_img == txt_foto_1.text) acertos++;
                nome_img = img_foto_2.sprite.name;
                if (nome_img == txt_foto_2.text) acertos++;

                nome_img = img_foto_3.sprite.name;
                if (nome_img == txt_foto_3.text) acertos++;
                nome_img = img_foto_4.sprite.name;
                if (nome_img == txt_foto_4.text) acertos++;
                break;

            case 3:
                nome_img = img_foto_1.sprite.name;
                if (nome_img == txt_foto_1.text) acertos++;
                nome_img = img_foto_2.sprite.name;
                if (nome_img == txt_foto_2.text) acertos++;

                nome_img = img_foto_3.sprite.name;
                if (nome_img == txt_foto_3.text) acertos++;
                nome_img = img_foto_4.sprite.name;
                if (nome_img == txt_foto_4.text) acertos++;

                nome_img = img_foto_5.sprite.name;
                if (nome_img == txt_foto_5.text) acertos++;
                nome_img = img_foto_6.sprite.name;
                if (nome_img == txt_foto_6.text) acertos++;

                nome_img = img_foto_7.sprite.name;
                if (nome_img == txt_foto_7.text) acertos++;
                nome_img = img_foto_8.sprite.name;
                if (nome_img == txt_foto_8.text) acertos++;
                break;
        }

        pnl_acertos.SetActive(true);

        //CALCULANDO PORCENTAGEM DE ACERTOS
        decimal porcentagem_acertos = (acertos * 100) / qtdFotos;

        print(porcentagem_acertos);

        //DEFININDO PONTOS FINAIS
        if (porcentagem_acertos < 50)
        {
            img_estrela_1.sprite = estrela_nao;
            img_estrela_2.sprite = estrela_nao;
            img_estrela_3.sprite = estrela_nao;
        }
        else if (porcentagem_acertos < 75)
        {
            img_estrela_1.sprite = estrela_sim;
            img_estrela_2.sprite = estrela_nao;
            img_estrela_3.sprite = estrela_nao;
        }
        else if (porcentagem_acertos < 100)
        {
            img_estrela_1.sprite = estrela_sim;
            img_estrela_2.sprite = estrela_sim;
            img_estrela_3.sprite = estrela_nao;
        }
        else //SE ACERTOU TUDO, ALMENTAR UM NIVEL DA ATIVIDADE
        {
            img_estrela_1.sprite = estrela_sim;
            img_estrela_2.sprite = estrela_sim;
            img_estrela_3.sprite = estrela_sim;
            Metodo_Adicionar_Pontos_Licao6_Atv1();
        }


    }

    public void Metodo_Adicionar_Pontos_Licao6_Atv1()
    {
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        try
        {
            //PEGA NIVEL ATUAL DA ATIVIDADE
            conexao.conectarBanco();

            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=6";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();
            int n = 0;
            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    n = (int)dados["NIVEL_ATIVIDADE"];
                    print(n);
                }
            }

            dados.Close();
            comando.Dispose();

            //ALMENTA UM NIVEL
            conexao.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=6";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            comando.ExecuteNonQuery();

            conexao.fecharBanco();
        }
        catch
        {

        }
    }

    public void Metodo_Voltar_Menu_Principal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
    }
}


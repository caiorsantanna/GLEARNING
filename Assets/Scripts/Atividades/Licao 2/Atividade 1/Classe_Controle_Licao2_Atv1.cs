using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Linq;
using System.IO;

public class Classe_Controle_Licao2_Atv1 : MonoBehaviour
{
    public GameObject NPC;    
    public List<GameObject> npcs = new List<GameObject>(); 
    public Dictionary<string,string> nomes_empresas = new Dictionary<string, string>();
    public Dictionary<string, string> descricoes_empresas = new Dictionary<string, string>();
    List<string> todos_nomes = new List<string>();
    List<string> todos_descricoes = new List<string>();
    public List<string> tags = new List<string>();
    public List<string> logos_1 = new List<string>();
    public List<string> logos_2 = new List<string>();
    public Dictionary<string, string> logos_3 = new Dictionary<string, string>();

    public object[,] posicoes_possiveis = new object[6,2];

    public List<List<Vector3>> posicoesNPC = new List<List<Vector3>>();
    public List<Vector3> posicoes1 = new List<Vector3>();
    public List<Vector3> posicoes2 = new List<Vector3>();

    public Sprite cima_m, baixo_m, cima_f, baixo_f;

    public List<Sprite> cima_r_m;
    public List<Sprite> baixo_r_m;
    public List<Sprite> cima_r_f;
    public List<Sprite> baixo_r_f;

    public List<Sprite> cima_c_m;
    public List<Sprite> baixo_c_m;
    public List<Sprite> cima_c_f;
    public List<Sprite> baixo_c_f;

    public GameObject pnl_empresa_1, pnl_empresa_2, pnl_empresa_3, pnl_empresa_4, pnl_empresa_5, pnl_empresa_6;
    public Dropdown dpw_nomes, dpw_descricoes;
    public Image logo_1_1, logo_2_1, logo_3_1;
    public Image logo_1_2, logo_2_2, logo_3_2;
    public Image logo_1_3, logo_2_3, logo_3_3;
    public Image logo_1_4, logo_2_4, logo_3_4;
    public Image logo_1_5, logo_2_5, logo_3_5;
    public Image logo_1_6, logo_2_6, logo_3_6;

    public Image logo_1_info, logo_2_info, logo_3_info;

    List<List<string>> empresas = new List<List<string>>();
    public List<string> empresa1 = new List<string>();
    public List<string> empresa2 = new List<string>();
    public List<string> empresa3 = new List<string>();
    public List<string> empresa4 = new List<string>();
    public List<string> empresa5 = new List<string>();
    public List<string> empresa6 = new List<string>();
    
    public Image img_estrela_1, img_estrela_2, img_estrela_3;
    public Sprite estrela_sim, estrela_nao;

    public int nivel = 3;
    int nEmpresa;

    MySqlCommand comando;
    MySqlDataReader dados;
    void Start()
    {        
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        cima_r_m = new List<Sprite>();
        baixo_r_m = new List<Sprite>();
        cima_r_f = new List<Sprite>();
        baixo_r_f = new List<Sprite>();

        cima_c_m = new List<Sprite>();
        baixo_c_m = new List<Sprite>();
        cima_c_f = new List<Sprite>();
        baixo_c_f = new List<Sprite>();

        posicoes_possiveis[0, 0] = "CIMA";
        posicoes_possiveis[0, 1] = new Vector3(-2.50f, 0.7f, 0);
        posicoes_possiveis[1, 0] = "CIMA";
        posicoes_possiveis[1, 1] = new Vector3(0.5f, 0.7f, 0);
        posicoes_possiveis[2, 0] = "CIMA";
        posicoes_possiveis[2, 1] = new Vector3(3.5f, 0.7f, 0);

        posicoes_possiveis[3, 0] = "BAIXO";
        posicoes_possiveis[3, 1] = new Vector3(-2.43f, -1.55f, 0);
        posicoes_possiveis[4, 0] = "BAIXO";
        posicoes_possiveis[4, 1] = new Vector3(0.55f, -1.55f, 0);
        posicoes_possiveis[5, 0] = "BAIXO";
        posicoes_possiveis[5, 1] = new Vector3(3.59f, -1.55f, 0);

        posicoes1.Add(new Vector3(-2.50f, 0.7f, 0));
        posicoes1.Add(new Vector3(0.5f, 0.7f, 0));
        posicoes1.Add(new Vector3(3.5f, 0.7f, 0));

        posicoes2.Add(new Vector3(-2.43f, -1.55f, 0));
        posicoes2.Add(new Vector3(0.55f, -1.55f, 0));
        posicoes2.Add(new Vector3(3.59f, -1.55f, 0));

        posicoesNPC.Add(posicoes1);
        posicoesNPC.Add(posicoes2);

        empresa1.Add(null);
        empresa1.Add(null);

        empresa2.Add(null);
        empresa2.Add(null);

        empresa3.Add(null);
        empresa3.Add(null);

        empresa4.Add(null);
        empresa4.Add(null);

        empresa5.Add(null);
        empresa5.Add(null);

        empresa6.Add(null);
        empresa6.Add(null);

        empresas.Add(empresa1);
        empresas.Add(empresa2);
        empresas.Add(empresa3);
        empresas.Add(empresa4);
        empresas.Add(empresa5);
        empresas.Add(empresa6);

        //PEGANDO BASES
        cima_m = Resources.LoadAll<Sprite>("Sprites/Masculino/Male_Base")[1];
        baixo_m = Resources.LoadAll<Sprite>("Sprites/Masculino/Male_Base")[10];
        cima_f = Resources.LoadAll<Sprite>("Sprites/Feminino/Female_Base")[1];
        baixo_f = Resources.LoadAll<Sprite>("Sprites/Feminino/Female_Base")[10];

        //PEGANDO ROUPAS MASCULINAS
        if (Resources.LoadAll<Sprite>("Sprites/Masculino/Roupas/Escritorio").Length > 0)
        {
            for (int i = 0; i < Resources.LoadAll<Sprite>("Sprites/Masculino/Roupas/Escritorio").Length / 12; i++)
            {
                cima_r_m.Add(Resources.LoadAll<Sprite>("Sprites/Masculino/Roupas/Escritorio/M_" + (i + 1))[1]);
                baixo_r_m.Add(Resources.LoadAll<Sprite>("Sprites/Masculino/Roupas/Escritorio/M_" + (i + 1))[10]);
            }
        }
        else
        {
            print("Sem Sprites Masculinos");
        }

        //PEGANDO ROUPAS FEMININAS
        if (Resources.LoadAll<Sprite>("Sprites/Feminino/Roupas/Escritorio").Length > 0)
        {
            for (int i = 0; i < Resources.LoadAll<Sprite>("Sprites/Feminino/Roupas/Escritorio").Length / 12; i++)
            {
                cima_r_f.Add(Resources.LoadAll<Sprite>("Sprites/Feminino/Roupas/Escritorio/F_" + (i + 1))[1]);
                baixo_r_f.Add(Resources.LoadAll<Sprite>("Sprites/Feminino/Roupas/Escritorio/F_" + (i + 1))[10]);
            }
        }
        else
        {
            print("Sem Sprites Feminino");
        }

        //PEGANDO CABELOS MASCULINOS
        if (Resources.LoadAll<Sprite>("Sprites/Masculino/Cabelos").Length > 0)
        {
            for (int i = 0; i < Resources.LoadAll<Sprite>("Sprites/Masculino/Cabelos").Length / 12; i++)
            {
                cima_c_m.Add(Resources.LoadAll<Sprite>("Sprites/Masculino/Cabelos/C_M_" + (i + 1))[1]);
                baixo_c_m.Add(Resources.LoadAll<Sprite>("Sprites/Masculino/Cabelos/C_M_" + (i + 1))[10]);
            }
        }
        else
        {
            print("Sem Cabelos Masculinos");
        }

        //PEGANDO CABELOS FEMININAS
        if (Resources.LoadAll<Sprite>("Sprites/Feminino/Cabelos").Length > 0)
        {
            for (int i = 0; i < Resources.LoadAll<Sprite>("Sprites/Feminino/Cabelos").Length / 12; i++)
            {
                cima_c_f.Add(Resources.LoadAll<Sprite>("Sprites/Feminino/Cabelos/C_F_" + (i + 1))[1]);
                baixo_c_f.Add(Resources.LoadAll<Sprite>("Sprites/Feminino/Cabelos/C_F_" + (i + 1))[10]);
            }
        }
        else
        {
            print("Sem Cabelos Feminino");
        }

        try
        {
            conexao.conectarBanco();

            // PEGAR NIVEL
            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=2";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    int n = (int)dados["NIVEL_ATIVIDADE"];
                    if (n <= 10)
                    {
                        nivel = 1;
                    }
                    else if (n <= 20)
                    {
                        nivel = 2;
                    }
                    else
                    {
                        nivel = 3;
                    }
                }
            }

            dados.Close();
            comando.Dispose();

            // PEGAR NOMES EMPRESAS
            conexao.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG2 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Nome' AND CONTEUDO_TAG1 = 'Empresa';";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    nomes_empresas.Add(dados["CONTEUDO_TAG2"].ToString(), dados["CONTEUDO_TEXTO"].ToString());
                    todos_nomes.Add(dados["CONTEUDO_TEXTO"].ToString());
                }
            }

            dados.Close();
            comando.Dispose();

            // PEGAR DESCRICOES
            conexao.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG2 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Descricao' AND CONTEUDO_TAG1 = 'Empresa';";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    descricoes_empresas.Add(dados["CONTEUDO_TAG2"].ToString(), dados["CONTEUDO_TEXTO"].ToString());
                    tags.Add(dados["CONTEUDO_TAG2"].ToString());
                    todos_descricoes.Add(dados["CONTEUDO_TEXTO"].ToString());
                }
            }            

            tags = tags.Distinct().ToList();

            dpw_nomes.AddOptions(todos_nomes);
            dpw_descricoes.AddOptions(todos_descricoes);

            dados.Close();
            comando.Dispose();

            // PEGAR LOGOS            
            conexao.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1, CONTEUDO_TAG2 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Logo'; ";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    switch (dados["CONTEUDO_TAG1"].ToString())
                    {
                        case "1":
                            logos_1.Add(dados["CONTEUDO_TEXTO"].ToString());                            
                            break;
                        case "2":
                            logos_2.Add(dados["CONTEUDO_TEXTO"].ToString());
                            break;
                        case "3":
                            logos_3.Add(dados["CONTEUDO_TAG2"].ToString(), dados["CONTEUDO_TEXTO"].ToString());
                            break;
                        default:
                            print("ERRO BUSCAR LOGOS");
                            break;
                    }
                }
            }

            dados.Close();
            comando.Dispose();            

            conexao.fecharBanco();
        }
        catch
        {

        }

        int nNpcs;        
        switch (nivel)
        {
            case 1:
                nNpcs = 2;
                break;
            case 2:
                nNpcs = 4;
                break;
            case 3:
                nNpcs = 6;
                break;
            default:
                nNpcs = 2;
                break;
        }

        int rPosicao1;
        int rPosicao2;
        // GERA NPCS
        for (int i = 0; i < nNpcs; i++)
        {
            rPosicao1 = Random.Range(0, posicoesNPC.Count);

            if (posicoesNPC[rPosicao1].Count == 0)
            {
                posicoesNPC.RemoveAt(rPosicao1);
                rPosicao1 = Random.Range(0, posicoesNPC.Count);
            }

            rPosicao2 = Random.Range(0, posicoesNPC[rPosicao1].Count);            
            GameObject npc = Instantiate(NPC, posicoesNPC[rPosicao1][rPosicao2], Quaternion.identity);
            Classe_NPC_Licao2_Atv1 c = npc.GetComponent<Classe_NPC_Licao2_Atv1>();
            c.id = i + 1;
            npcs.Add(npc);
            posicoesNPC[rPosicao1].RemoveAt(rPosicao2);

        }        
    }

    public void Metodo_Adicionar_Pontos_Licao1_Atv1()
    {
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        try
        {
            conexao.conectarBanco();

            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=2";
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

            conexao.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=2";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            comando.ExecuteNonQuery();

            conexao.fecharBanco();
        }
        catch
        {

        }
    }

    public void Metodo_Terminar_Licao2_Atv1()
    {
        int acertos = 0;

        List<string> nomes = new List<string>();
        nomes.Add(empresas[0][0]);
        nomes.Add(empresas[1][0]);
        nomes.Add(empresas[2][0]);
        nomes.Add(empresas[3][0]);
        nomes.Add(empresas[4][0]);
        nomes.Add(empresas[5][0]);

        List<string> descricoes = new List<string>();
        descricoes.Add(empresas[0][1]);
        descricoes.Add(empresas[1][1]);
        descricoes.Add(empresas[2][1]);
        descricoes.Add(empresas[3][1]);
        descricoes.Add(empresas[4][1]);
        descricoes.Add(empresas[5][1]);


        for (int i = 0; i < this.nivel * 2; i++)
        {
            GameObject objNpc = this.npcs[i].gameObject;
            Classe_NPC_Licao2_Atv1 npc = objNpc.GetComponent<Classe_NPC_Licao2_Atv1>();

            if (npc.nome.Equals(nomes[i]))
            {
                acertos++;
            }            
            if (npc.descricao.Equals(descricoes[i]))
            {
                acertos++;
            }
        }
        print(acertos);

        decimal porcentagem_acertos = (acertos * 100) / ((this.nivel * 2) * 2);        

        print(porcentagem_acertos);

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
        else
        {
            img_estrela_1.sprite = estrela_sim;
            img_estrela_2.sprite = estrela_sim;
            img_estrela_3.sprite = estrela_sim;
            Metodo_Adicionar_Pontos_Licao1_Atv1();
        }                

    }

    public void Metodo_Iniciar_Relatorio()
    {
        switch (nivel)
        {
            case 1:                               
                pnl_empresa_1.SetActive(true);
                logo_1_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_2.SetActive(true);
                logo_1_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                break;
            case 2:
                pnl_empresa_1.SetActive(true);
                logo_1_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_2.SetActive(true);
                logo_1_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_3.SetActive(true);
                logo_1_3.sprite = npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_3.sprite = npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_3.sprite = npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_4.SetActive(true);
                logo_1_4.sprite = npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_4.sprite = npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_4.sprite = npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                break;
            case 3:
                pnl_empresa_1.SetActive(true);
                logo_1_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_2.SetActive(true);
                logo_1_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_3.SetActive(true);
                logo_1_3.sprite = npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_3.sprite = npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_3.sprite = npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_4.SetActive(true);
                logo_1_4.sprite = npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_4.sprite = npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_4.sprite = npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_5.SetActive(true);
                logo_1_5.sprite = npcs[4].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_5.sprite = npcs[4].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_5.sprite = npcs[4].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_6.SetActive(true);
                logo_1_6.sprite = npcs[5].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_6.sprite = npcs[5].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_6.sprite = npcs[5].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                break;
            default:
                logo_1_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_1.sprite = npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;

                logo_1_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_2.sprite = npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                break;
        }
    }

    public void Metodo_Selecionar_InfoEmpresa(int nEmpresa)
    {
        int index;

        dpw_nomes.value = 0;
        this.nEmpresa = nEmpresa;

        logo_1_info.sprite = npcs[nEmpresa].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
        logo_2_info.sprite = npcs[nEmpresa].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
        logo_3_info.sprite = npcs[nEmpresa].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;

        if (empresas[nEmpresa][0] != null)
        {
            index = dpw_nomes.options.FindIndex((i) => { return i.text.Equals(empresas[nEmpresa][0]); });
            dpw_nomes.value = index;
        }
        else{ dpw_nomes.value = 0; }

        if (empresas[nEmpresa][1] != null)
        {
            index = dpw_descricoes.options.FindIndex((i) => { return i.text.Equals(empresas[nEmpresa][1]); });
            dpw_descricoes.value = index;
        }
        else { dpw_descricoes.value = 0; }


    }

    public void Metodo_OK_Empresas()
    {
        
        empresas[nEmpresa][0] = dpw_nomes.captionText.text;
        empresas[nEmpresa][1] = dpw_descricoes.captionText.text;
              
    }

    public void Metodo_Voltar_Menu_Principal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
    }
}

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

    public int nivel = 3;

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
                }
            }

            tags = tags.Distinct().ToList();

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
            c.Id = i + 1;
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

            conexao.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=1";
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

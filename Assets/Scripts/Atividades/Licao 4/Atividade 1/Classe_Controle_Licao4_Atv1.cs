using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao4_Atv1 : MonoBehaviour
{
    public GameObject pnl_loading;
    public GameObject gameobject_eventos, scroll_object, pnl_task_L4A1, canvas, pnl_tudo, pnl_acertos;

    public List<GameObject> todos_eventos = new List<GameObject>();
    public Dictionary<string, GameObject> eventos_questao = new Dictionary<string, GameObject>();
    public List<GameObject> tasks = new List<GameObject>();

    public Dictionary<string, bool> acertos_erros = new Dictionary<string, bool>();

    public Text txt_time;

    public Image img_estrela_1, img_estrela_2, img_estrela_3;
    public Sprite estrela_sim, estrela_nao;

    bool tempo_correndo = false;
    float tempo = 180;


    //VARIVAVEL DO NIVEL DA ATIVIDADE
    public int nivel = 1;
    int qtdAtividades = 3;
    public int atv_feitas = 0;

    //VARIAVEIS DE BANCO
    MySqlCommand comando;
    MySqlDataReader dados;

    private void Update()
    {
        if (atv_feitas == qtdAtividades)
        {
            Metodo_Terminar_Licao4_Atv1();
            atv_feitas = 0;
        }

        if (tempo_correndo)
        {
            tempo -= Time.deltaTime;
            txt_time.text = Mathf.RoundToInt(tempo) + "s";
            if (tempo <= 0)
            {
                tempo_correndo = false;
                Time.timeScale = 0f;
                Metodo_Terminar_Licao4_Atv1();
            }

        }


    }

    void Start()
    {
        //INICIALIZA AS CLASSES 'CONEXAO' E 'PLAYER'
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        try
        {
            conexao.conectarBanco();

            //COMANDO SQL NIVEL 2
            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=4";
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
                        qtdAtividades = 3;
                        tempo = 180;
                    }
                    else if (n <= 20)
                    {
                        nivel = 2;
                        qtdAtividades = 6;
                        tempo = 140;
                    }
                    else
                    {
                        nivel = 3;
                        qtdAtividades = 9;
                        tempo = 120;
                    }
                }
            }

            dados.Close();
            comando.Dispose();

            tempo_correndo = true;

            todos_eventos = GetChildren(gameobject_eventos);
            float y_task = 152f;

            int nREvento;
            for (int i = 0; i < qtdAtividades; i++)
            {
                nREvento = Random.Range(0, todos_eventos.ToArray().Length);
                todos_eventos[nREvento].SetActive(true);
                todos_eventos[nREvento].GetComponent<Classe_Evento_Licao4_Atv1>().e_pra_fazer = true;

                GameObject task = Instantiate(pnl_task_L4A1, new Vector3(canvas.transform.position.x, canvas.transform.position.y + y_task, 0), Quaternion.identity, scroll_object.transform);
                y_task = y_task - (task.GetComponent<RectTransform>().rect.height + 10);
                Text txt_task = task.transform.Find("txt_task").GetComponent<Text>();
                txt_task.text = todos_eventos[nREvento].GetComponent<Classe_Evento_Licao4_Atv1>().evento_task;

                eventos_questao.Add(todos_eventos[nREvento].name, todos_eventos[nREvento]);
                acertos_erros.Add(todos_eventos[nREvento].name, false);                

                tasks.Add(task);

                todos_eventos.RemoveAt(nREvento);
            }

        }
        catch (MySqlException e)
        {
            print(e);
        }

    }

    public void Metodo_Terminar_Licao4_Atv1()
    {
        int acertos = 0;

        pnl_acertos.SetActive(true);
        pnl_tudo.SetActive(false);

        foreach(bool acerto in acertos_erros.Values)
        {
            if (acerto) acertos++;
        }        

        //CALCULANDO PORCENTAGEM DE ACERTOS
        decimal porcentagem_acertos = (acertos * 100) / (qtdAtividades);

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
            Metodo_Adicionar_Pontos_Licao4_Atv1();
        }
    }

    public void Metodo_Adicionar_Pontos_Licao4_Atv1()
    {
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        try
        {
            //PEGA NIVEL ATUAL DA ATIVIDADE
            conexao.conectarBanco();

            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=4";
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
            conexao.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=4";
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
        Time.timeScale = 1f;
        pnl_loading.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
    }

    public static List<GameObject> GetChildren(GameObject go)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform tran in go.transform)
        {
            children.Add(tran.gameObject);
        }
        return children;
    }
}

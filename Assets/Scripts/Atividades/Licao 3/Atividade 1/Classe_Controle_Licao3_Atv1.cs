using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao3_Atv1 : MonoBehaviour
{
    public GameObject pnl_loading;
    public List<GameObject> spots_moedas = new List<GameObject>();
    public GameObject spots_moedas_object, hotel_1, hotel_2, hotel_3, hotel_4, pnl_tudo, pnl_acertos;
    public Transform Player, ponto;
    public Text txt_moedas, txt_time;

    public List<GameObject> eventos = new List<GameObject>();
    public List<Vector3> posicoes_eventos = new List<Vector3>();

    public Dictionary<Vector3, GameObject> mapa_eventos = new Dictionary<Vector3, GameObject>();

    public List<string> dicas = new List<string>();

    //VARIVAVEL DO NIVEL DA ATIVIDADE
    public int nivel = 1;
    public int nDicas = 4;
    public int moedas = 0;

    bool tempo_correndo = false;
    float tempo = 300;

    //VARIAVEIS DE BANCO
    MySqlCommand comando;
    MySqlDataReader dados;

    void Start()
    {
        //INICIALIZA AS CLASSES 'CONEXAO' E 'PLAYER'
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        posicoes_eventos.Add(new Vector3(-3f, 0f, 0));
        posicoes_eventos.Add(new Vector3(53f, -56f, 0));
        posicoes_eventos.Add(new Vector3(-3f, -112f, 0));
        posicoes_eventos.Add(new Vector3(-59f, -56f, 0));

        try
        {
            conexao.conectarBanco();

            //COMANDO SQL NIVEL 2
            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=3";
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
                        nDicas = 4;
                        tempo = 300;
                    }
                    else if (n <= 20)
                    {
                        nivel = 2;
                        nDicas = 3;
                        tempo = 250;
                    }
                    else
                    {
                        nivel = 3;
                        nDicas = 2;
                        tempo = 200;
                    }
                }
            }

            dados.Close();
            comando.Dispose();

            tempo_correndo = true;

            //CARREGANDO TODOS OS PREFABS DE EVENTOS
            for(int i = 0; i < Resources.LoadAll<GameObject>("L3_A1/Eventos/").Length; i++)
            {
                eventos.Add(Resources.Load<GameObject>("L3_A1/Eventos/evento_" + (i+1)));
            }

            //GERANDO TODOS OS EVENTOS NAS POSICOES POSSIVEIS
            int rNEvento, rNPosicao;
            int nPosicoes = posicoes_eventos.ToArray().Length;
            for (int i = 0; i < nPosicoes; i++)
            {
                rNEvento = Random.Range(0, eventos.ToArray().Length);
                rNPosicao = Random.Range(0, posicoes_eventos.ToArray().Length);

                Instantiate(eventos[rNEvento], posicoes_eventos[rNPosicao], Quaternion.identity);

                mapa_eventos.Add(posicoes_eventos[rNPosicao], eventos[rNEvento]);

                eventos.RemoveAt(rNEvento);
                posicoes_eventos.RemoveAt(rNPosicao);
            }

            //PEGANDO TODAS AS MOEDAS
            spots_moedas = GetChildren(spots_moedas_object);

            //GERANDO MOEDAS
            int rNMoeda;
            for(int i = 0; i < nDicas; i++)
            {
                rNMoeda = Random.Range(0, spots_moedas.ToArray().Length);
                spots_moedas[rNMoeda].SetActive(true);
                spots_moedas.RemoveAt(rNMoeda);
            }

            int rNHotel = Random.Range(0, 4);
            switch (rNHotel)
            {
                case 0:
                    dicas.Add("The Hotel is close to a " + mapa_eventos[new Vector3(-3f, 0f, 0)].GetComponent<Classe_Evento_Licao3_Atv1>().nome);
                    dicas.Add("The Hotel is close to a " + mapa_eventos[new Vector3(53f, -56f, 0)].GetComponent<Classe_Evento_Licao3_Atv1>().nome);
                    dicas.Add("The Hotel is on the right side of the neighborhood");
                    dicas.Add("The Hotel is on top of the neighborhood");
                    hotel_1.GetComponent<Classe_Hotel_Licao3_Atv1>().hotel_certo = true;
                    break;
                case 1:
                    dicas.Add("The Hotel is close to a " + mapa_eventos[new Vector3(-3f, 0f, 0)].GetComponent<Classe_Evento_Licao3_Atv1>().nome);
                    dicas.Add("The Hotel is close to a " + mapa_eventos[new Vector3(-59f, -56f, 0)].GetComponent<Classe_Evento_Licao3_Atv1>().nome);
                    dicas.Add("The Hotel is on the left side of the neighborhood");
                    dicas.Add("The Hotel is on top of the neighborhood");
                    hotel_2.GetComponent<Classe_Hotel_Licao3_Atv1>().hotel_certo = true;
                    break;
                case 2:
                    dicas.Add("The Hotel is close to a " + mapa_eventos[new Vector3(-59f, -56f, 0)].GetComponent<Classe_Evento_Licao3_Atv1>().nome);
                    dicas.Add("The Hotel is close to a " + mapa_eventos[new Vector3(-3f, -112f, 0)].GetComponent<Classe_Evento_Licao3_Atv1>().nome);
                    dicas.Add("The Hotel is on the left side of the neighborhood");
                    dicas.Add("The Hotel is at the bottom of the neighborhood");
                    hotel_3.GetComponent<Classe_Hotel_Licao3_Atv1>().hotel_certo = true;
                    break;
                case 3:
                    dicas.Add("The Hotel is close to a " + mapa_eventos[new Vector3(53f, -56f, 0)].GetComponent<Classe_Evento_Licao3_Atv1>().nome);
                    dicas.Add("The Hotel is close to a " + mapa_eventos[new Vector3(-3f, -112f, 0)].GetComponent<Classe_Evento_Licao3_Atv1>().nome);
                    dicas.Add("The Hotel is on the right side of the neighborhood");
                    dicas.Add("The Hotel is at the bottom of the neighborhood");
                    hotel_4.GetComponent<Classe_Hotel_Licao3_Atv1>().hotel_certo = true;
                    break;
            }
        }
        catch (MySqlException e)
        {
            print(e);
        }

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

    public void Metodo_Adicionar_Pontos_Licao3_Atv1()
    {
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        try
        {
            //PEGA NIVEL ATUAL DA ATIVIDADE
            conexao.conectarBanco();

            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=3";
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
            conexao.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=3";
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

    void Update()
    {
        ponto.position = new Vector3(Player.position.x - 224.6f, Player.position.y - 2.2f, -1);
        txt_moedas.text = moedas.ToString();

        if (tempo_correndo)
        {
            tempo -= Time.deltaTime;
            txt_time.text = Mathf.RoundToInt(tempo) + "s";
            if (tempo <= 0)
            {
                tempo_correndo = false;
                Time.timeScale = 0f;
                pnl_tudo.SetActive(false);
                pnl_acertos.SetActive(true);
            }
        }
    }

}

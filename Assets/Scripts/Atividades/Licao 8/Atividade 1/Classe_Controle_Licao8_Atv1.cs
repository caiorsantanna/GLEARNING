using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao8_Atv1 : MonoBehaviour
{
  public GameObject pnl_loading;
  public GameObject pnl_main, pnl_acertos, pnl_choose_curriculum, job;
  public Button curriculo_1, curriculo_2;
  public Text txt_nome_1, txt_idade_1, txt_descricao_1, txt_nome_2, txt_idade_2, txt_descricao_2;
  public Image img_estrela_1, img_estrela_2, img_estrela_3;
  public Sprite estrela_sim, estrela_nao;
  public List<string> nomes = new List<string>();
  public List<string> idades = new List<string>();
  public List<string> descricoes = new List<string>();
  public List<string> jobs = new List<string>();
  MySqlCommand command;
  MySqlDataReader data;
  Banco_Conexao connection = new Banco_Conexao();
  Objeto_Player player = new Objeto_Player();
  int level = 1;
  int correctAnswers = 0;
  int questionsToAnswer = 0;
  void Start()
  {
    try
    {
      connection.conectarBanco();

      connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=8";
      command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
      data = command.ExecuteReader();

      if (data.HasRows)
      {
        while (data.Read())
        {
          int n = (int)data["NIVEL_ATIVIDADE"];
          if (n <= 10)
          {
              level = 1;
          }
          else if (n <= 20)
          {
              level = 2;
          }
          else
          {
              level = 3;
          }
        }
      }

      data.Close();
      command.Dispose();

      List<string> banco_nomes = new List<string>();
      List<string> banco_sobrenomes = new List<string>();

      //Nomes
      connection.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Nome' AND (CONTEUDO_TAG1 = 'Masculino' OR CONTEUDO_TAG1 = 'Feminino');";
      command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
      data = command.ExecuteReader();

      if(data.HasRows){
        while(data.Read()){
          banco_nomes.Add(data["CONTEUDO_TEXTO"].ToString());
        }
      }

      data.Close();
      command.Dispose();

      //Sobrenomes
      connection.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Sobrenome';";
      command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
      data = command.ExecuteReader();

      if(data.HasRows){
        while(data.Read()){
          banco_sobrenomes.Add(data["CONTEUDO_TEXTO"].ToString());
        }
      }

      data.Close();
      command.Dispose();

      // Jobs
      connection.Sql = "SELECT CONTEUDO_TEXTO FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Rooms';";
      command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
      data = command.ExecuteReader();

      if(data.HasRows){
        while(data.Read()){
          jobs.Add(data["CONTEUDO_TEXTO"].ToString());
        }
      }

      data.Close();
      command.Dispose();

      // Descricoes
      connection.Sql = "SELECT CONTEUDO_TEXTO FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Job_Description';";
      command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
      data = command.ExecuteReader();

      if(data.HasRows){
        while(data.Read()){
          descricoes.Add(data["CONTEUDO_TEXTO"].ToString());
        }
      }

      data.Close();
      command.Dispose();

      for(int i = 0; i < banco_nomes.Count; i++) {
        nomes.Add($"{banco_nomes[i]} {banco_sobrenomes[i]}");
        idades.Add(Random.Range(18, 30).ToString());
      }

      float topIndex = 550f;
      for(int i = 0; i < level * 2; i++) {
        GameObject prefab = Instantiate(job, new Vector3(0, 0, 0), Quaternion.identity, pnl_main.transform);
        RectTransform rt = prefab.GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(rt.offsetMax.x, topIndex);
        rt.offsetMin = new Vector2(50, rt.offsetMin.y);
        rt.offsetMax = new Vector2(-50, rt.offsetMax.y);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 100);
        topIndex -= 220;

        Classe_Job control_job = prefab.GetComponent<Classe_Job>();
        Text txt_job = prefab.gameObject.transform.Find("txt_job").GetComponent<Text>();

        int randomDescriptionIndex = Random.Range(0, jobs.Count);

        control_job.correct_description = descricoes[randomDescriptionIndex];
        txt_job.text = $"Job: {jobs[randomDescriptionIndex]}";

        jobs.RemoveAt(randomDescriptionIndex);
        descricoes.RemoveAt(randomDescriptionIndex);

        randomDescriptionIndex = Random.Range(0, jobs.Count);

        control_job.incorrect_description = descricoes[randomDescriptionIndex];

        Button btn_job = prefab.gameObject.transform.Find("btn_job").GetComponent<Button>();
        btn_job.onClick.AddListener(() => {
          int randomPlace = Random.Range(1, 3);

          if(randomPlace == 1) {
            txt_nome_1.text = $"Name: {control_job.nome_1}";
            txt_nome_2.text = $"Name: {control_job.nome_2}";
            txt_idade_1.text = $"Age: {control_job.idade_1}";
            txt_idade_2.text = $"Age: {control_job.idade_2}";
            txt_descricao_1.text = $"Description: {control_job.correct_description}";
            txt_descricao_2.text = $"Description: {control_job.incorrect_description}";
            curriculo_1.onClick.RemoveAllListeners();
            curriculo_2.onClick.RemoveAllListeners();
            curriculo_1.onClick.AddListener(() => {
              Answer(true);
              btn_job.transform.Find("txt_button").GetComponent<Text>().text = control_job.nome_1;
              btn_job.interactable = false;
              pnl_choose_curriculum.SetActive(false);
            });
            curriculo_2.onClick.AddListener(() => {
              Answer();
              btn_job.transform.Find("txt_button").GetComponent<Text>().text = control_job.nome_2;
              btn_job.interactable = false;
              pnl_choose_curriculum.SetActive(false);
            });
          } else {
            txt_nome_1.text = $"Name: {control_job.nome_2}";
            txt_nome_2.text = $"Name: {control_job.nome_1}";
            txt_idade_1.text = $"Age: {control_job.idade_2}";
            txt_idade_2.text = $"Age: {control_job.idade_1}";
            txt_descricao_1.text = $"Description: {control_job.incorrect_description}";
            txt_descricao_2.text = $"Description: {control_job.correct_description}";
            curriculo_1.onClick.RemoveAllListeners();
            curriculo_2.onClick.RemoveAllListeners();
            curriculo_1.onClick.AddListener(() => {
              Answer();
              btn_job.transform.Find("txt_button").GetComponent<Text>().text = control_job.nome_2;
              btn_job.interactable = false;
              pnl_choose_curriculum.SetActive(false);
            });
            curriculo_2.onClick.AddListener(() => {
              Answer(true);
              btn_job.transform.Find("txt_button").GetComponent<Text>().text = control_job.nome_1;
              btn_job.interactable = false;
              pnl_choose_curriculum.SetActive(false);
            });
          }
          pnl_choose_curriculum.SetActive(true);
        });
      }

      connection.fecharBanco();
    } catch (MySqlException e) {
      Debug.LogError(e);
    }
  }

  public void Answer(bool correct = false) {
    questionsToAnswer++;

    if(correct) {
      correctAnswers++;
    }

    if(questionsToAnswer == (level * 2)) {
      FinalizeActivity();
    }
  }

  public void FinalizeActivity() {
    pnl_main.SetActive(false);
    pnl_acertos.SetActive(true);

    decimal correctPercent = (correctAnswers*100)/(level*2);

    Debug.Log($"{correctAnswers}, {level}, {correctPercent}");
    if(correctPercent < 50){
      img_estrela_1.sprite = estrela_nao;
      img_estrela_2.sprite = estrela_nao;
      img_estrela_3.sprite = estrela_nao;
    }else if(correctPercent < 75){
      img_estrela_1.sprite = estrela_sim;
      img_estrela_2.sprite = estrela_nao;
      img_estrela_3.sprite = estrela_nao;
    }else if(correctPercent < 100){
      img_estrela_1.sprite = estrela_sim;
      img_estrela_2.sprite = estrela_sim;
      img_estrela_3.sprite = estrela_nao;
    }else{
      img_estrela_1.sprite = estrela_sim;
      img_estrela_2.sprite = estrela_sim;
      img_estrela_3.sprite = estrela_sim;
      Metodo_Adicionar_Pontos_Licao8_Atv1();
    }
  }

  public void Metodo_Adicionar_Pontos_Licao8_Atv1()
    {
      Banco_Conexao connection = new Banco_Conexao();
      Objeto_Player player = new Objeto_Player();

      try
      {
        //PEGA NIVEL ATUAL DA ATIVIDADE
        connection.conectarBanco();

        connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=8";
        command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
        data = command.ExecuteReader();
        int n = 0;
        if (data.HasRows)
        {
          while (data.Read())
          {
            n = (int)data["NIVEL_ATIVIDADE"];
            Debug.Log(n);
          }
        }

        data.Close();
        command.Dispose();

        //ALMENTA UM NIVEL
        connection.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=8";
        command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
        command.ExecuteNonQuery();

        connection.fecharBanco();
      }
      catch
      {

      }
    }

    public void Metodo_Voltar_Menu_Principal()
    {
      pnl_loading.SetActive(true);
      UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
    }
}

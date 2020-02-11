	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using MySql.Data.MySqlClient;

	public class Classe_Controle_Licao7_Atv1 : MonoBehaviour
	{
    public GameObject pnl_loading;
    public GameObject pnl_main, pnl_acertos;
		public Text question, answer1, answer2, answer3;
    public Image img_estrela_1, img_estrela_2, img_estrela_3;
    public Sprite estrela_sim, estrela_nao;
		public List<string> places = new List<string>();
    public List<string> answers = new List<string>();
		MySqlCommand command;
		MySqlDataReader data;
    int level = 1;
    int answersTimes;
    int correctAnswers = 0;

		void Start()
		{
			Banco_Conexao connection = new Banco_Conexao();
			Objeto_Player player = new Objeto_Player();

			try
      {
        connection.conectarBanco();

        connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=7";
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

        connection.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Lugar' AND CONTEUDO_TAG1 = 'Lição 7 Atividade 1';";
				command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
				data = command.ExecuteReader();

				if(data.HasRows){
					while(data.Read()){
						places.Add(data["CONTEUDO_TEXTO"].ToString());
					}
				}

				data.Close();
				command.Dispose();

				connection.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Direção' AND CONTEUDO_TAG1 = 'Lição 7 Atividade 1';";
				command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
				data = command.ExecuteReader();

				if(data.HasRows){
					while(data.Read()){
						answers.Add(data["CONTEUDO_TEXTO"].ToString());
					}
				}

				data.Close();
				command.Dispose();

				connection.fecharBanco();
			} catch (MySqlException e) {
				Debug.LogError(e);
			}

			switch(level){
				case 1:
					answersTimes = 2;
					break;
				case 2:
					answersTimes = 4;
					break;
				case 3:
					answersTimes = 6;
					break;
				default:
					answersTimes = 2;
					break;
			}

      RandomizeActivitie();
		}

    public void Answer(int placeIndex = 0, bool correct = false) {
      if(correct) {
        correctAnswers++;

        places.RemoveAt(placeIndex);
        answers.RemoveAt(placeIndex);
      }

      if(answersTimes > 1) {
        RandomizeActivitie();
      } else {
        Finilize();
      }

      answersTimes--;
    }

    public void Finilize() {

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
        Metodo_Adicionar_Pontos_Licao7_Atv1();
      }
    }

    public void RandomizeActivitie() {
      List<string> tempPlaces = new List<string>(places);
      List<string> tempAnswers =  new List<string>(answers);

      int placeIndex = Random.Range(0, tempPlaces.Count);
      question.text = RandomizeQuestion(tempPlaces[placeIndex]);

      List<Text> answersPlaces = new List<Text>();
      answersPlaces.Add(answer1);
      answersPlaces.Add(answer2);
      answersPlaces.Add(answer3);

      int randomCorrectAnswer = Random.Range(0, answersPlaces.Count);
      answersPlaces[randomCorrectAnswer].text = tempAnswers[placeIndex];
      Button button = answersPlaces[randomCorrectAnswer].gameObject.transform.parent.GetComponent<Button>();
      button.onClick.RemoveAllListeners();
      button.onClick.AddListener(() => Answer(placeIndex, true));

      answersPlaces.RemoveAt(randomCorrectAnswer);
      tempPlaces.RemoveAt(placeIndex);
      tempAnswers.RemoveAt(placeIndex);

      answersPlaces.ForEach((answer) => {
        int incorrectPlaceIndex = Random.Range(0, tempPlaces.Count);

        answer.text = tempAnswers[incorrectPlaceIndex];
        Button incorrectButton = answer.gameObject.transform.parent.GetComponent<Button>();
        incorrectButton.onClick.RemoveAllListeners();
        incorrectButton.onClick.AddListener(() => Answer());

        tempPlaces.RemoveAt(incorrectPlaceIndex);
        tempAnswers.RemoveAt(incorrectPlaceIndex);
      });
    }

    public string RandomizeQuestion(string place) {
      int answerIndex = Random.Range(1, 3);
      switch(answerIndex) {
        case 1:
          return $"Hi! I would like to go to the {place}, could you indicate the place?";
        case 2:
          return $"Hello, where is the {place}?";
        case 3:
          return $"Hey, could you tell me where the {place} is?";
        default:
          return $"Hello, where is the {place}?";
      }
    }

    public void Metodo_Adicionar_Pontos_Licao7_Atv1()
    {
      Banco_Conexao connection = new Banco_Conexao();
      Objeto_Player player = new Objeto_Player();

      try
      {
        //PEGA NIVEL ATUAL DA ATIVIDADE
        connection.conectarBanco();

        connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=7";
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
        connection.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=7";
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

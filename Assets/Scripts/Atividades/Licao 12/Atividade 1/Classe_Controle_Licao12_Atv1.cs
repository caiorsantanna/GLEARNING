using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao12_Atv1 : MonoBehaviour
{
	public GameObject pnl_loading;
  public GameObject pnl_main, pnl_acertos, pnl_missing;
	public Image img_estrela_1, img_estrela_2, img_estrela_3;
	public Sprite estrela_sim, estrela_nao;
	public Sprite spt_food, spt_bitcoin, spt_trangenics, spt_semen;
	public Image img_graph;
	public Dropdown dpw_resolution;
	MySqlCommand command;
	MySqlDataReader data;
	Banco_Conexao connection = new Banco_Conexao();
	Objeto_Player player = new Objeto_Player();
	int level = 1;
	int correctAnswers = 0;
	int problemsSolved = 0;

	string correctKey = "";

	Dictionary<string, Sprite> graphs;
	Dictionary<string, string> descriptions;

	void Start()
	{
		graphs = new Dictionary<string, Sprite>() {
			{ "food", spt_food },
			{ "bitcoin", spt_bitcoin },
			{ "trangenics", spt_trangenics },
			{ "semen", spt_semen }
		};

		descriptions = new Dictionary<string, string>() {
			{ "blank", "" },
			{ "food", "This graph shows the tendency towards natural food diets. In this case Megacorp should invest on organic crops and the production of gluten free products, as well as considering the high rate of allergic people and the diets that would fit them. According to this perspective, in five years time, Megacorp is going to value small properties producers who practise small crops plantation and avoid using pesticides, instead of keeping buying from large scale producers that are not worried about herbicides and food quality. " },
			{ "bitcoin", "This graph shows the market movement towards investing on bitcoins. In this case commodities are bought and sold by Megacorp without any kind of investments on real assets. The tendency, in this case, is to turn the company into a smaller building and focus on stock market movements." },
			{ "trangenics", "This graph reveals the keeping of the movement situation it is presented nowadays. In other words, considering Brazil as a strong food producer because of its lands and climate, Megacorp is going to keep growing sponentially and investing on transgenic seeds, which is the basis the the market production today, mainly in US." },
			{ "semen", "This graph shows the inclination of the agribusiness market on focusing on animal raising. Winnig bulls and cows represent a significant ammount of money in the international market. Besides that, the semen is a resource that can be an investment for long term money return." }
		};

		try
    {
      connection.conectarBanco();

      connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=12";
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

			dpw_resolution.options.Clear();
			foreach(KeyValuePair<string, string> description in descriptions) {
				dpw_resolution.options.Add(new Dropdown.OptionData(description.Value));
			}

			NextGraph();

			connection.fecharBanco();

		} catch (MySqlException e) {
      Debug.LogError(e);
    }
	}

	public void Answer() {
		if(
			descriptions[correctKey] ==
			dpw_resolution.options[dpw_resolution.value].text
		) {
			correctAnswers++;

			if(problemsSolved == level) {
				Finish();
			} else {
				NextGraph();
			}
		} else {
			if(problemsSolved == level) {
				Finish();
			} else {
				NextGraph();
			}
		}
	}

	void NextGraph() {
		problemsSolved++;

		int randomGraph = Random.Range(1, graphs.Count);
		img_graph.sprite = graphs.ElementAt(randomGraph).Value;
		correctKey = graphs.ElementAt(randomGraph).Key;
		graphs.Remove(correctKey);
		dpw_resolution.value = 0;
	}

	void Finish() {
		pnl_main.SetActive(false);
		pnl_acertos.SetActive(true);

		decimal correctPercent = (correctAnswers*100)/(level);

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
			Metodo_Adicionar_Pontos_Licao12_Atv1();
		}
	}

	public void Metodo_Adicionar_Pontos_Licao12_Atv1()
	{
		Banco_Conexao connection = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();

		try
		{
			//PEGA NIVEL ATUAL DA ATIVIDADE
			connection.conectarBanco();

			connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=12";
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
			connection.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=12";
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

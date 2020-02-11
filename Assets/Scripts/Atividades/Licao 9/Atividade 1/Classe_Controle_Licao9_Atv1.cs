using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao9_Atv1 : MonoBehaviour
{
	public GameObject pnl_loading;
	public GameObject pnl_main, pnl_acertos, pnl_missing;
	public Image img_estrela_1, img_estrela_2, img_estrela_3;
  public Sprite estrela_sim, estrela_nao;
	public Text txt_rules;
	public Dropdown dpw_lecture, dpw_food, dpw_beverages, dpw_presentation;
	MySqlCommand command;
	MySqlDataReader data;
	Banco_Conexao connection = new Banco_Conexao();
	Objeto_Player player = new Objeto_Player();
	int level = 1;

	public List<string[]> lectures = new List<string[]>() {
		new string[2] { "", "blank" },
		new string[2] { "Dr. Will Bristol - 'Changes in everyday world feeding' (US$ 1.000 for royalties )", "expensive" },
		new string[2] { "Dr. Melanie Wool - 'Pancs and the good effects on human health' (US$ 500 for royalties)", "cheaper" }
	};
	public List<string[]> foods = new List<string[]>() {
		new string[2] { "", "blank" },
		new string[2] { "Italian food", "healthier" },
		new string[2] {"Fast Food (sandwiches)", "heavier" }
	};
	public List<string[]> beverages = new List<string[]>() {
		new string[2] { "", "c" },
		new string[2] {"Juices and water", "healthier" },
		new string[2] {"Soda and water", "heavier" }
	};
	public List<string[]> presentation_formats = new List<string[]>() {
		new string[2] { "", "blank" },
		new string[2] {"Oral presentation", "longer" },
		new string[2] {"Panels", "faster" }
	};
	public Dictionary<string, List<string[]>> contents;
	public List<string> rules = new List<string>();

	void Start()
	{
		contents = new Dictionary<string, List<string[]>>() {
			{ "lectures", lectures },
			{ "foods", foods },
			{ "beverages", beverages },
			{ "presentation_formats", presentation_formats }
		};

		try
    {
      connection.conectarBanco();

      connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=9";
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

			dpw_lecture.options.Clear();
			foreach(string[] lecture in lectures) {
				dpw_lecture.options.Add(new Dropdown.OptionData(lecture[0]));
			}

			dpw_food.options.Clear();
			foreach(string[] food in foods) {
				dpw_food.options.Add(new Dropdown.OptionData(food[0]));
			}

			dpw_beverages.options.Clear();
			foreach(string[] beverage in beverages) {
				dpw_beverages.options.Add(new Dropdown.OptionData(beverage[0]));
			}

			dpw_presentation.options.Clear();
			foreach(string[] presentation in presentation_formats) {
				dpw_presentation.options.Add(new Dropdown.OptionData(presentation[0]));
			}

			for(int i = 0; i < level * 2; i++) {
				int r1 = Random.Range(0, contents.Count);
				int r2 = Random.Range(1, contents.ElementAt(r1).Value.Count);

				rules.Add(contents.ElementAt(r1).Value[r2][1]);

				txt_rules.text += $"- {contents.ElementAt(r1).Value[r2][1]}\n";

				contents.Remove(contents.ElementAt(r1).Key);
			}

			connection.fecharBanco();

		} catch (MySqlException e) {
      Debug.LogError(e);
    }
	}

	public void Finish() {
		int lecture = System.Convert.ToInt32(dpw_lecture.value);
		int food = System.Convert.ToInt32(dpw_food.value);
		int beverage = System.Convert.ToInt32(dpw_beverages.value);
		int presentation = System.Convert.ToInt32(dpw_presentation.value);

		int correctAnswers = 0;

    if (
			lectures[lecture][1] != "blank" &&
			foods[food][1] != "blank" &&
			beverages[beverage][1] != "blank" &&
			presentation_formats[presentation][1] != "blank"
		) {
			for(int i = 0; i < rules.Count; i++) {
				if(
					lectures[lecture][1] == rules[i] ||
					foods[food][1] == rules[i] ||
					beverages[beverage][1] == rules[i] ||
					presentation_formats[presentation][1] == rules[i]
				) {
					correctAnswers++;
				}
			}

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
				Metodo_Adicionar_Pontos_Licao9_Atv1();
			}
		} else {
			pnl_missing.SetActive(true);
		}
	}

	public void Metodo_Adicionar_Pontos_Licao9_Atv1()
	{
		Banco_Conexao connection = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();

		try
		{
			//PEGA NIVEL ATUAL DA ATIVIDADE
			connection.conectarBanco();

			connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=9";
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
			connection.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=9";
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

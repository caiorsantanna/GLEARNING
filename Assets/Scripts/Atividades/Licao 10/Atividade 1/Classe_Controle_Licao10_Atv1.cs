using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao10_Atv1 : MonoBehaviour
{
	public GameObject pnl_loading;
	public GameObject pnl_main, pnl_acertos, pnl_missing;
	public Image img_estrela_1, img_estrela_2, img_estrela_3;
	public Sprite estrela_sim, estrela_nao;
	public Text txt_count, txt_problem;
	public Dropdown dpw_solution;
	MySqlCommand command;
	MySqlDataReader data;
	Banco_Conexao connection = new Banco_Conexao();
	Objeto_Player player = new Objeto_Player();
	int level = 1;
	int correctAnswers = 0;

	List<List<string>> problems = new List<List<string>>() {
		new List<string>() {
			"blank",
			""
		},
		new List<string>() {
			"There is strike going on and the material delivery for production is late",
			"Call other material providers."
		},
		new List<string>() {
			"Pesticides have damaged the honey production and there is not enough product to make medicines in the lab.",
			"Import the material from other countries."
		},
		new List<string>() {
			"One of the employees is spending too much time surfing in the net.",
			"Invite a psychologist to talk about deviation and problems with the lack of focus/ talk to the employee in private."
		},
		new List<string>() {
			"The computers are infected with viruses.",
			"Call the technical support and renew the viruses licence."
		},
		new List<string>() {
			"A client is visiting the company. Someone has to take him for dinner, but he's a vegetarian. What restaurant to go?",
			"We could take him to \"Veggie's World\" or to the \"Health Gourmet\"."
		},
		new List<string>() {
			"A whole department is off sick with a flu. What should the CEO do?",
			"Ask each depatment to send an employee and substitute the ones that are off."
		},
		new List<string>() {
			"One of the members from the Technical Department is very intelligent, but not very fond of group work.",
			"Assign him with tasks he is very good at and respect his personality trait."
		},
		new List<string>() {
			"A very important client is asking for a better discount than what he normally gets at MegaCorp. He asks for 20% instead of 10%.",
			"Offer him a 15 % discount, assuring that it is not going to become a habit in the transactions."
		},
	};

	List<List<string>> problemsToSolve = new List<List<string>>();

	int problemsSolved = -1;

	void Start()
	{
		try
    {
      connection.conectarBanco();

      connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=10";
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

			dpw_solution.options.Clear();
			foreach(List<string> problem in problems) {
				dpw_solution.options.Add(new Dropdown.OptionData(problem[1]));
			}

			for(int i = 0; i < level * 2; i++) {
				int randomProblem = Random.Range(1, problems.Count);
				problemsToSolve.Add(problems[randomProblem]);
				problems.RemoveAt(randomProblem);
			}

			NextProblem();

			connection.fecharBanco();

		} catch (MySqlException e) {
      Debug.LogError(e);
    }
	}

	public void Answer() {
		if(dpw_solution.value != 0) {
			if(
				dpw_solution.options[dpw_solution.value].text == problemsToSolve[problemsSolved][1]
			) {
				correctAnswers++;

				if((problemsSolved + 1) == problemsToSolve.Count) {
					Finish();
				} else {
					NextProblem();
				}
			} else {
				if((problemsSolved + 1) == problemsToSolve.Count) {
					Finish();
				} else {
					NextProblem();
				}
			}
		} else {
			pnl_missing.SetActive(true);
		}
	}

	void NextProblem() {
		dpw_solution.value = 0;
		problemsSolved++;

		txt_count.text = $"{problemsSolved + 1} / {problemsToSolve.Count}";

		txt_problem.text = problemsToSolve[problemsSolved][0];
	}

	void Finish() {
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
			Metodo_Adicionar_Pontos_Licao10_Atv1();
		}
	}

	public void Metodo_Adicionar_Pontos_Licao10_Atv1()
	{
		Banco_Conexao connection = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();

		try
		{
			//PEGA NIVEL ATUAL DA ATIVIDADE
			connection.conectarBanco();

			connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=10";
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
			connection.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=10";
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

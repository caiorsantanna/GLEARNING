using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao11_Atv1 : MonoBehaviour
{
	public GameObject pnl_loading;
	public GameObject pnl_main, pnl_acertos;
	public Image img_estrela_1, img_estrela_2, img_estrela_3;
	public Sprite estrela_sim, estrela_nao;
	public Text txt_dialog;
	public Button btn_airport, btn_hotel, btn_restaurant;
	MySqlCommand command;
	MySqlDataReader data;
	Banco_Conexao connection = new Banco_Conexao();
	Objeto_Player player = new Objeto_Player();
	int level = 1;
	int dialogToOccour = 0;
	int correctAnswers = 0;
	string correctAnswer = "";

	public List<List<string>> dialogs = new List<List<string>>() {
		new List<string>() {
@"- Good morning. My flight number is 704.
- Good morning, Sir. Would you like a window or an aisle seat?
- Window, please. 
- Smoker or non-smoker?
- Non-smoker, please?
- Do you have luggage to ship? Any bags to carry with you?
- Just a suitcase to ship, please.
- Ok, let me weigh it, please. Thank you
- Thank you.
- Your flight boarding begins in 20 minutes. Have a good trip, Sir.
- Thank you very much. Have a nice day.",
			"Airport"
		},
		new List<string>() {
@"- Good afternoon, Sir. How can I help you?
- I have a room booked in the name of my company, Megacorp, from Brazil.
- Oh, yes, Sir. You are staying for a week, right?
- That's it.
- Your room is 2423 and it has a view to the lake.
- Thank you.
- Our breakfast is served between 8 and 11 a.m., ok, Sir?
- Perfect. Thank you very much.
- We thank you.",
			"Hotel"
		},
		new List<string>() {
@"- Good evening. May I help you?
- A table for one, please.
- Certainly. This way, please. Have a seat.
- Thank you.
- Would you like something to drink?
- May I have white wine, please?
- Sure, I'll bring you the wine menu. May I suggest you our grilled shrimp? It would be perfect with the wine.
- Great, thank you.
...
- How do you like your meal, Sir?
- Oh, it's delicious. And the wine matches it very well.
- Glad to know that.
- Would you bring me the bill, please?
- May I offer you a desert?
- No, thank you. Just the bill and a coffee, please.
- Right away, Sir",
			"Restaurant"
		}
	};
	void Start()
	{
		try
    {
      connection.conectarBanco();

      connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=11";
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

			NextDialog();

			connection.fecharBanco();

		} catch (MySqlException e) {
      Debug.LogError(e);
    }
	}
	void NextDialog() {
		dialogToOccour++;

		int random = Random.Range(0, dialogs.Count);
		txt_dialog.text = dialogs[random][0];
		correctAnswer = dialogs[random][1];

		dialogs.RemoveAt(random);
	}

	public void Answer(Text buttonText) {
		if(
			buttonText.text == correctAnswer
		) {
			correctAnswers++;

			if(dialogToOccour == level) {
				Finish();
			} else {
				NextDialog();
			}
		} else {
			if(dialogToOccour == level) {
				Finish();
			} else {
				NextDialog();
			}
		}
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
			Metodo_Adicionar_Pontos_Licao11_Atv1();
		}
	}

	public void Metodo_Adicionar_Pontos_Licao11_Atv1()
	{
		Banco_Conexao connection = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();

		try
		{
			//PEGA NIVEL ATUAL DA ATIVIDADE
			connection.conectarBanco();

			connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=11";
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
			connection.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=11";
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

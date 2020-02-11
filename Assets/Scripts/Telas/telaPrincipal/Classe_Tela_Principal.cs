using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Tela_Principal : MonoBehaviour {

	public GameObject pnl_megacorp, pnl_loading;
	public GameObject pnl_licao_estrela_1,  pnl_licao_estrela_2, pnl_licao_estrela_3, pnl_licao_estrela_4, pnl_licao_estrela_5, pnl_licao_estrela_6, pnl_licao_estrela_7, pnl_licao_estrela_8, pnl_licao_estrela_9, pnl_licao_estrela_10, pnl_licao_estrela_11, pnl_licao_estrela_12;
	public Text txt_nome, txt_points;
  public Image img_base, img_cabelo, img_roupa;
	public Sprite estrela_sim, estrela_nao;
	MySqlCommand command;
	MySqlDataReader data;
	Banco_Conexao connection = new Banco_Conexao();
	Objeto_Player player = new Objeto_Player();

	void Start(){
		txt_nome.text = player.Nome;

		img_base.sprite = Resources.LoadAll<Sprite>("Sprites/"+ player.Pele)[1];
		img_cabelo.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Cabelo)[1];
		img_roupa.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Roupa)[1];

		Dictionary<string, GameObject> atividades = new Dictionary<string, GameObject>() {
			{ "pnl_licao_1", pnl_licao_estrela_1 },
			{ "pnl_licao_2", pnl_licao_estrela_2 },
			{ "pnl_licao_3", pnl_licao_estrela_3 },
			{ "pnl_licao_4", pnl_licao_estrela_4 },
			{ "pnl_licao_5", pnl_licao_estrela_5 },
			{ "pnl_licao_6", pnl_licao_estrela_6 },
			{ "pnl_licao_7", pnl_licao_estrela_7 },
			{ "pnl_licao_8", pnl_licao_estrela_8 },
			{ "pnl_licao_9", pnl_licao_estrela_9 },
			{ "pnl_licao_10", pnl_licao_estrela_10 },
			{ "pnl_licao_11", pnl_licao_estrela_11 },
			{ "pnl_licao_12", pnl_licao_estrela_12 },
		};

		int soma = 0;

		try
		{
			connection.conectarBanco();

			for(int i = 1; i <= 12; i++) {

				connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=" + i;
				command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
				data = command.ExecuteReader();

				if (data.HasRows)
				{
					while (data.Read())
					{
						int n = (int)data["NIVEL_ATIVIDADE"];
						soma += n;
						if(n != 1) {
							if (n <= 10)
							{
								atividades["pnl_licao_" + i].transform.Find("estrela_1").GetComponent<Image>().sprite = estrela_sim;
								atividades["pnl_licao_" + i].transform.Find("estrela_2").GetComponent<Image>().sprite = estrela_nao;
								atividades["pnl_licao_" + i].transform.Find("estrela_3").GetComponent<Image>().sprite = estrela_nao;
							}
							else if (n <= 20)
							{
								atividades["pnl_licao_" + i].transform.Find("estrela_1").GetComponent<Image>().sprite = estrela_sim;
								atividades["pnl_licao_" + i].transform.Find("estrela_2").GetComponent<Image>().sprite = estrela_sim;
								atividades["pnl_licao_" + i].transform.Find("estrela_3").GetComponent<Image>().sprite = estrela_nao;
							}
							else
							{
								atividades["pnl_licao_" + i].transform.Find("estrela_1").GetComponent<Image>().sprite = estrela_sim;
								atividades["pnl_licao_" + i].transform.Find("estrela_2").GetComponent<Image>().sprite = estrela_sim;
								atividades["pnl_licao_" + i].transform.Find("estrela_3").GetComponent<Image>().sprite = estrela_sim;
							}
						} else {
							atividades["pnl_licao_" + i].transform.Find("estrela_1").GetComponent<Image>().sprite = estrela_nao;
							atividades["pnl_licao_" + i].transform.Find("estrela_2").GetComponent<Image>().sprite = estrela_nao;
							atividades["pnl_licao_" + i].transform.Find("estrela_3").GetComponent<Image>().sprite = estrela_nao;
						}
					}
				}

				data.Close();
				command.Dispose();
			}

			txt_points.text = (soma - 12).ToString();
			connection.fecharBanco();

			pnl_loading.SetActive(false);

		} catch (MySqlException e) {
			Debug.LogError(e);
		}

	}

	public void Licao1_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_1_1");
	}

	public void Licao2_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_2_1");
	}

	public void Licao3_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_3_1");
	}

	public void Licao4_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_4_1");
	}

	public void Licao5_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_5_1");
	}

	public void Licao6_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_6_1");
	}

	public void Licao7_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_7_1");
	}

	public void Licao8_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_8_1");
	}

	public void Licao9_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_9_1");
	}

	public void Licao10_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_10_1");
	}

	public void Licao11_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_11_1");
	}

	public void Licao12_Atividade1()
	{
		pnl_loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_12_1");
	}
}

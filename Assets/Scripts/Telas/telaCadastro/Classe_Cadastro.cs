using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Classe_Cadastro : MonoBehaviour {
	public GameObject pnl_codigo, pnl_cadastro_info, pnl_cadastro_player, pnl_loading;
	MySqlDataReader dados;
	MySqlCommand comando;

	// -------------------- Verificar a sala ---------------------
	public void Metodo_Selecionar_Sala(){
		Banco_Conexao conexao = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();


		InputField txt_codigo = GameObject.Find("txt_codigo").GetComponent<InputField>();

		string codigo = txt_codigo.text;

		try{
			conexao.conectarBanco();
			conexao.Sql = "SELECT * FROM TB_SALA WHERE SALA_CODIGO='"+codigo+"';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){
				while(dados.Read()){
					player.CodSala = System.Convert.ToInt64(dados["SALA_ID"]);
					player.NomeSala = dados["SALA_NOME"].ToString();
				}
				dados.Close();
				comando.Dispose();

				pnl_codigo.SetActive(false);
				pnl_cadastro_info.SetActive(true);
				Text sala = GameObject.Find("txt_sala_info").GetComponent<Text>();
				sala.text = player.NomeSala;

			}else{
				dados.Close();
				comando.Dispose();

				StartCoroutine(Corrotina_Status_Cadastro("txt_status_codigo"));
			}

						conexao.fecharBanco();
				}
				catch(MySqlException e){
			dados.Close();
			comando.Dispose();
						conexao.fecharBanco();
						print(e);
						//reconexao.realizarReconexao();
		}	
	}
	// -------------------- Verificar o codigo ---------------------

	// -------------------- Validar Informações ---------------------
	public void Metodo_Validar_Info(){
		Banco_Conexao conexao = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();

		InputField txt_nome = GameObject.Find("txt_nome").GetComponent<InputField>();
		InputField txt_email = GameObject.Find("txt_email").GetComponent<InputField>();
		InputField txt_cpf = GameObject.Find("txt_cpf").GetComponent<InputField>();
		Text txt_status_info = GameObject.Find("txt_status_info").GetComponent<Text>();

		string nome = txt_nome.text;
		string email = txt_email.text;
		long cpf = System.Convert.ToInt64(txt_cpf.text);

		if((txt_nome.text == "")||(txt_email.text == "")||(txt_cpf.text == "")){

			txt_status_info.text = "Um dos campos está em branco!";
			StartCoroutine(Corrotina_Status_Cadastro("txt_status_info"));

		}else{
			try{
				conexao.conectarBanco();
				conexao.Sql = "SELECT USER_ESTUDANTE_CPF FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_EMAIL='"+email+"';";
				comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
				dados = comando.ExecuteReader();

				if(dados.HasRows){
					dados.Close();
					comando.Dispose();

					txt_status_info.text = "Email ou cpf já cadastrados! Contate um Administrador.";
					StartCoroutine(Corrotina_Status_Cadastro("txt_status_info"));

				}else{
					dados.Close();
					comando.Dispose();

					conexao.Sql = "SELECT USER_ESTUDANTE_CPF FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_CPF='"+cpf+"';";
					comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
					dados = comando.ExecuteReader();

					if(dados.HasRows){
						dados.Close();
						comando.Dispose();

						StartCoroutine(Corrotina_Status_Cadastro("txt_status_info"));

					}else{
						player.Nome = nome;
						player.Email = email;
						player.Cpf = cpf;

						dados.Close();
						comando.Dispose();

						pnl_cadastro_info.SetActive(false);
						pnl_cadastro_player.SetActive(true);
						//Text sala = GameObject.Find("txt_sala_player").GetComponent<Text>();
						//sala.text = player.NomeSala;
					}
				}
						conexao.fecharBanco();
						}
						catch(MySqlException e){
						dados.Close();
						comando.Dispose();
						conexao.fecharBanco();
						print(e);
						//reconexao.realizarReconexao();
			}
		}
	}
	// -------------------- Validar Informações ---------------------

	// -------------------- Validar Player ---------------------
	public void Metodo_Validar_Player(){
		Banco_Conexao conexao = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();

		InputField txt_login = GameObject.Find("txt_login").GetComponent<InputField>();
		InputField txt_senha = GameObject.Find("txt_senha").GetComponent<InputField>();
		InputField txt_status_login = GameObject.Find("txt_status_login").GetComponent<InputField>();
		InputField txt_status_senha = GameObject.Find("txt_status_senha").GetComponent<InputField>();

		Toggle tgl_masculino = GameObject.Find("tgl_masculino").GetComponent<Toggle>();
		Toggle tgl_feminino = GameObject.Find("tgl_feminino").GetComponent<Toggle>();

		if(txt_login.text == ""){
			txt_status_login.text = "Não pode estar em branco!";
			StartCoroutine(Corrotina_Status_Cadastro("txt_status_login"));
		}else if(txt_senha.text == ""){
			txt_status_senha.text = "Não pode estar em branco!";
			StartCoroutine(Corrotina_Status_Cadastro("txt_status_senha"));
		}else{
			try{
				conexao.conectarBanco();
				conexao.Sql = "SELECT USER_ESTUDANTE_CPF FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_LOGIN='"+txt_login.text+"';";
				comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
				dados = comando.ExecuteReader();

				if(dados.HasRows){
					comando.Dispose();
					dados.Close();

					txt_status_login.text = "Login não disponível! Tente outro.";
					StartCoroutine(Corrotina_Status_Cadastro("txt_status_login"));
				}else{
					comando.Dispose();
					dados.Close();

					if(tgl_masculino.isOn){
						player.Pele = "B_M";
						player.Roupa = "R_M_Escritorio_1";
						player.Cabelo = "C_M_1";
					}
					else if(tgl_feminino.isOn){
							player.Pele = "B_F";
							player.Roupa = "R_F_Escritorio_1";
							player.Cabelo = "C_F_1";
					}
					else{
						return;
					}

					comando.Dispose();
					dados.Close();

					pnl_loading.SetActive(true);

					player.Acessorio = "NULL";
					player.Nivel = 1;
					player.Patuais = 0;
					player.Psemestre = 0;
					player.Ptotais = 0;

					conexao.Sql = "INSERT INTO TB_USER_ESTUDANTE (USER_ESTUDANTE_CPF, USER_ESTUDANTE_NOME, USER_ESTUDANTE_LOGIN, "
					+ "USER_ESTUDANTE_SENHA, USER_ESTUDANTE_EMAIL, USER_ESTUDANTE_NIVEL, USER_ESTUDANTE_PTOTAIS, USER_ESTUDANTE_PSEMESTRE, "
					+ "USER_ESTUDANTE_PATUAIS,  USER_ESTUDANTE_PELE, USER_ESTUDANTE_ROUPA, USER_ESTUDANTE_CABELO, USER_ESTUDANTE_ACESSORIO) VALUES ("
					+ player.Cpf+", "
					+"'"+player.Nome+"', "
					+"'"+txt_login.text+"', "
					+"'"+txt_senha.text+"', "
					+"'"+player.Email+"', "
					+player.Nivel+", "
					+player.Ptotais+", "
					+player.Psemestre+", "
					+player.Patuais+", "
					+"'"+player.Pele+"', "
					+"'"+player.Roupa+"', "
					+"'"+player.Cabelo+"', "
					+"'"+player.Acessorio
					+"');";
					comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
					comando.ExecuteNonQuery();

					comando.Dispose();

					List<int> cod_atividades = new List<int>();

					conexao.Sql = "SELECT ATIVIDADE_ID FROM TB_ATIVIDADES;";
					comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
					dados = comando.ExecuteReader();
					if (dados.HasRows)
					{
						while (dados.Read())
						{
							cod_atividades.Add(System.Convert.ToInt32(dados["ATIVIDADE_ID"]));
						}
					}

					comando.Dispose();
					dados.Close();

					for (int i = 0; i < cod_atividades.ToArray().Length; i++)
					{
							conexao.Sql = "INSERT INTO TB_NIVEL_ATIVIDADE(COD_ESTUDANTE, COD_ATIVIDADE, NIVEL_ATIVIDADE) VALUES " +
									"(" + player.Cpf + ", " + cod_atividades[i] + ", 1);";
							comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
							comando.ExecuteNonQuery();

							comando.Dispose();
					}

					comando.Dispose();

					conexao.fecharBanco();

					UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
			}

			}catch(MySqlException e){
				dados.Close();
				comando.Dispose();
				conexao.fecharBanco();
				print(e);
				//reconexao.realizarReconexao();
			}
		}
	}
	// -------------------- Validar Player ---------------------

	IEnumerator Corrotina_Status_Cadastro(string text)
	{
		Text txt_status_cadastro;
		txt_status_cadastro = GameObject.Find(text).GetComponent<Text>();

		txt_status_cadastro.color = new Color(0, 0, 0, 1);

		float fadeOutTime = 3;
		Color originalColor = txt_status_cadastro.color;
		for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
		{
			txt_status_cadastro.color = Color.Lerp(originalColor, new Color(0, 0, 0, 0), Mathf.Min(1, t/fadeOutTime));
			yield return null;
		}
	}

	public void Metodo_Retornar(){

		if(pnl_cadastro_info.activeSelf){
			UnityEngine.SceneManagement.SceneManager.LoadScene("telaLogin");
		}

		if(pnl_cadastro_player.activeSelf){
			pnl_cadastro_player.SetActive(false);
			pnl_cadastro_info.SetActive(true);
		}
	}
}

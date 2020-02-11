using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Classe_Login : MonoBehaviour {

	public GameObject pnl_erro, pnl_loading;
	public Text txt_erro;
	public void Metodo_Logar(){

		pnl_loading.SetActive(true);

		InputField txt_login, txt_senha;

		txt_login = GameObject.Find("txt_login").GetComponent<InputField>();
		txt_senha = GameObject.Find("txt_senha").GetComponent<InputField>();

		Banco_Conexao conexao = new Banco_Conexao();

		Objeto_Player player = new Objeto_Player();

		MySqlCommand comando;
		MySqlDataReader dados;

		try{

			conexao.conectarBanco();

			/* --------------------------------- ESTUDANTE --------------------------------- */	
			conexao.Sql = "SELECT * FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_LOGIN='"+txt_login.text+"' AND USER_ESTUDANTE_SENHA='"+txt_senha.text+"';";
			comando = new MySqlCommand("set net_write_timeout=99999; set net_read_timeout=99999", conexao.ConexaoBanco );
			comando.ExecuteNonQuery();
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){

				while(dados.Read()){
					player.Cpf = System.Convert.ToInt64(dados["USER_ESTUDANTE_CPF"]);
					player.Nome = System.Convert.ToString(dados["USER_ESTUDANTE_NOME"]);
					player.Email = System.Convert.ToString(dados["USER_ESTUDANTE_EMAIL"]);
					player.Nivel = System.Convert.ToInt32(dados["USER_ESTUDANTE_NIVEL"]);
					player.Ptotais = System.Convert.ToInt32(dados["USER_ESTUDANTE_PTOTAIS"]);
					player.Psemestre = System.Convert.ToInt32(dados["USER_ESTUDANTE_PSEMESTRE"]);
					player.Patuais = System.Convert.ToInt32(dados["USER_ESTUDANTE_PATUAIS"]);
					player.Pele = System.Convert.ToString(dados["USER_ESTUDANTE_PELE"]);
					player.Roupa = System.Convert.ToString(dados["USER_ESTUDANTE_ROUPA"]);
					player.Cabelo = System.Convert.ToString(dados["USER_ESTUDANTE_CABELO"]);
					player.Acessorio = System.Convert.ToString(dados["USER_ESTUDANTE_ACESSORIO"]);

					player.TipoLogin = "ESTUDANTE";

					UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
				}

			}else{
				dados.Close();
				comando.Dispose();

				/* --------------------------------- PROFESSOR --------------------------------- */
				conexao.Sql = "SELECT * FROM TB_USER_PROFESSOR WHERE USER_PROFESSOR_LOGIN='"+txt_login.text+"' AND USER_PROFESSOR_SENHA='"+txt_senha.text+"';";
				comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
				dados = comando.ExecuteReader();

				if(dados.HasRows){

					while(dados.Read()){
						player.Cpf = System.Convert.ToInt64(dados["USER_PROFESSOR_CPF"]);
						player.Nome = System.Convert.ToString(dados["USER_PROFESSOR_NOME"]);
						player.Email = System.Convert.ToString(dados["USER_PROFESSOR_EMAIL"]);
						player.Nivel = 0;
						player.Ptotais = 0;
						player.Psemestre = 0;
						player.Patuais = 0;
                        player.Pele = null;
						player.Roupa = null;
                        player.Cabelo = null;
                        player.Acessorio = null;

						player.TipoLogin = "PROFESSOR";
					}

				}else{
					dados.Close();
					comando.Dispose();

					/* --------------------------------- MONITOR --------------------------------- */	
					conexao.Sql = "SELECT * FROM TB_USER_MONITOR WHERE USER_MONITOR_LOGIN='"+txt_login.text+"' AND USER_MONITOR_SENHA='"+txt_senha.text+"';";
					comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
					dados = comando.ExecuteReader();

					if(dados.HasRows){

						while(dados.Read()){
							player.Cpf = System.Convert.ToInt64(dados["USER_MONITOR_CPF"]);
							player.Nome = System.Convert.ToString(dados["USER_MONITOR_NOME"]);
							player.Email = System.Convert.ToString(dados["USER_MONITOR_EMAIL"]);
							player.Nivel = 0;
							player.Ptotais = 0;
							player.Psemestre = 0;
							player.Patuais = 0;
							player.Pele = null;
							player.Roupa = null;
							player.Cabelo = null;
							player.Acessorio = null;

							player.TipoLogin = "MONITOR";
						}

					}else{
						dados.Close();
						comando.Dispose();

						/* --------------------------------- ADM --------------------------------- */
						conexao.Sql = "SELECT * FROM TB_USER_ADM WHERE USER_ADM_LOGIN='"+txt_login.text+"' AND USER_ADM_SENHA='"+txt_senha.text+"';";
						comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
						dados = comando.ExecuteReader();

						if(dados.HasRows){

							while(dados.Read()){
								player.Cpf = System.Convert.ToInt64(dados["USER_ADM_CPF"]);
								player.Nome = System.Convert.ToString(dados["USER_ADM_NOME"]);
								player.Email = System.Convert.ToString(dados["USER_ADM_EMAIL"]);
								player.Nivel = 0;
								player.Ptotais = 0;
								player.Psemestre = 0;
								player.Patuais = 0;
								player.Pele = null;
								player.Roupa = null;
								player.Cabelo = null;
								player.Acessorio = null;

								player.TipoLogin = "ADM";
							}

						}else{
							dados.Close();
							comando.Dispose();

							StartCoroutine(Corrotina_Status_Login());

							player.TipoLogin = "FALSO";
						}
					}
				}

			}

			conexao.fecharBanco();

		}catch(MySqlException e){
            pnl_erro.SetActive(true);
            txt_erro.text = e.ToString();
			Debug.Log(e.ToString());
			//reconexao.realizarReconexao();
		}

	}

	IEnumerator Corrotina_Status_Login()
	{
		Text txt_status_login;
		txt_status_login = GameObject.Find("txt_status_login").GetComponent<Text>();

		txt_status_login.color = new Color(1, 1, 1, 1);

		float fadeOutTime = 3;
		Color originalColor = txt_status_login.color;
		for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
		{
			txt_status_login.color = Color.Lerp(originalColor, new Color(1, 1, 1, 0), Mathf.Min(1, t/fadeOutTime));
			yield return null;
		}
	}

	public void Metodo_Cadastrar(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("telaCadastro");
	}

}

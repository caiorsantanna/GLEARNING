using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Classe_Login : MonoBehaviour {

	public GameObject pnl_reconexao;
	public void Metodo_Logar(){		

		InputField txt_login, txt_senha;
			
		txt_login = GameObject.Find("txt_login").GetComponent<InputField>();
		txt_senha = GameObject.Find("txt_senha").GetComponent<InputField>();								
		
		Banco_Conexao conexao = new Banco_Conexao();
		Banco_Reconexao reconexao = pnl_reconexao.GetComponent<Banco_Reconexao>();	
		Objeto_Player player = new Objeto_Player();

		MySqlCommand comando;
		MySqlDataReader dados;

		try{

			conexao.conectarBanco();

			/* --------------------------------- ESTUDANTE --------------------------------- */	
			conexao.Sql = "SELECT * FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_LOGIN='"+txt_login.text+"' AND USER_ESTUDANTE_SENHA='"+txt_senha.text+"';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();		

			if(dados.HasRows){

				while(dados.Read()){
					player.Id = dados["USER_ESTUDANTE_ID"].ToString();
					player.Cpf = dados["USER_ESTUDANTE_CPF"].ToString();
					player.Nome = dados["USER_ESTUDANTE_NOME"].ToString();
					player.Email = dados["USER_ESTUDANTE_EMAIL"].ToString();
					player.Nivel = (int) dados["USER_ESTUDANTE_NIVEL"];
					player.Ptotais = (int) dados["USER_ESTUDANTE_PTOTAIS"];
					player.Psemestre = (int) dados["USER_ESTUDANTE_PSEMESTRE"];
					player.Patuais = (int) dados["USER_ESTUDANTE_PATUAIS"];
					player.Roupa = (int) dados["COD_ROUPA"];
					player.Acessorio = (int) dados["COD_ACESSORIO"];

					player.TipoLogin = "ESTUDANTE";
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
						player.Id = dados["USER_PROFESSOR_ID"].ToString();
						player.Cpf = dados["USER_PROFESSOR_CPF"].ToString();
						player.Nome = dados["USER_PROFESSOR_NOME"].ToString();
						player.Email = dados["USER_PROFESSOR_EMAIL"].ToString();
						player.Nivel = 0;
						player.Ptotais = 0;
						player.Psemestre = 0;
						player.Patuais = 0;
						player.Roupa = 0;
						player.Acessorio = 0;			

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
							player.Id = dados["USER_MONITOR_ID"].ToString();
							player.Cpf = dados["USER_MONITOR_CPF"].ToString();
							player.Nome = dados["USER_MONITOR_NOME"].ToString();
							player.Email = dados["USER_MONITOR_EMAIL"].ToString();
							player.Nivel = 0;
							player.Ptotais = 0;
							player.Psemestre = 0;
							player.Patuais = 0;				
							player.Roupa = 0;
							player.Acessorio = 0;

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
								player.Id = dados["USER_ADM_ID"].ToString();
								player.Cpf = dados["USER_ADM_CPF"].ToString();
								player.Nome = dados["USER_ADM_NOME"].ToString();
								player.Email = dados["USER_ADM_EMAIL"].ToString();
								player.Nivel = 0;
								player.Ptotais = 0;
								player.Psemestre = 0;
								player.Patuais = 0;
								player.Roupa = 0;
							player.Acessorio = 0;					

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

		}catch{
			reconexao.realizarReconexao();			
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

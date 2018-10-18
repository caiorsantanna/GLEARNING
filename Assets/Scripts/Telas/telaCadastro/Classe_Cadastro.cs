using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Classe_Cadastro : MonoBehaviour {	
	public GameObject pnl_codigo, pnl_cadastro_info, pnl_cadastro_player, pnl_reconexao;

	MySqlDataReader dados;
	MySqlCommand comando;

	// -------------------- Verificar a sala ---------------------	
	public void Metodo_Selecionar_Sala(){		
		Banco_Conexao conexao = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();
		Banco_Reconexao reconexao = pnl_reconexao.GetComponent<Banco_Reconexao>();

		InputField txt_codigo = GameObject.Find("txt_codigo").GetComponent<InputField>();

		string codigo = txt_codigo.text;

		try{
			conexao.conectarBanco();
			conexao.Sql = "SELECT * FROM TB_SALA WHERE SALA_CODIGO='"+codigo+"';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){
				while(dados.Read()){
					player.CodSala = (int) dados["SALA_ID"];
					player.NomeSala = dados["SALA_NOME"].ToString();
				}
				dados.Close();           
				comando.Dispose();
				conexao.fecharBanco();

				pnl_codigo.SetActive(false);
				pnl_cadastro_info.SetActive(true);

			}else{
				dados.Close();           
				comando.Dispose();				
				conexao.fecharBanco();	

				StartCoroutine(Corrotina_Status_Cadastro("txt_status_codigo"));
			}
		}catch{
			dados.Close();           
			comando.Dispose();			
			reconexao.realizarReconexao();			
		}	
	}
	// -------------------- Verificar o codigo ---------------------

	// -------------------- Validar Informações ---------------------
	public void Metodo_Validar_Info(){
		Banco_Conexao conexao = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();
		Banco_Reconexao reconexao = pnl_reconexao.GetComponent<Banco_Reconexao>();
		

		InputField txt_nome = GameObject.Find("txt_nome").GetComponent<InputField>();
		InputField txt_email = GameObject.Find("txt_email").GetComponent<InputField>();
		InputField txt_cpf = GameObject.Find("txt_cpf").GetComponent<InputField>();
		
		string nome = txt_nome.text;
		string email = txt_email.text;
		string cpf = txt_cpf.text;

		try{
			conexao.conectarBanco();
			conexao.Sql = "SELECT USER_ESTUDANTE_ID FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_EMAIL='"+email+"';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){
				dados.Close();
				comando.Dispose();
				conexao.fecharBanco();
				
				StartCoroutine(Corrotina_Status_Cadastro("txt_status_info"));

			}else{
				conexao.conectarBanco();
				conexao.Sql = "SELECT USER_ESTUDANTE_ID FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_CPF='"+cpf+"';";
				comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
				dados = comando.ExecuteReader();

				if(dados.HasRows){
					dados.Close();
					comando.Dispose();
					conexao.fecharBanco();

					StartCoroutine(Corrotina_Status_Cadastro("txt_status_info"));

				}else{					
					player.Nome = nome;
					player.Email = email;
					player.Cpf = cpf;

					dados.Close();
					comando.Dispose();
					conexao.fecharBanco();

					pnl_cadastro_info.SetActive(false);
					pnl_cadastro_player.SetActive(true);
				}
			}
		}catch{
			dados.Close();           
			comando.Dispose();			
			reconexao.realizarReconexao();			
		}		
	}
	// -------------------- Validar Informações ---------------------

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

	public void retornaTelaLogin(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("telaLogin");
	}
}

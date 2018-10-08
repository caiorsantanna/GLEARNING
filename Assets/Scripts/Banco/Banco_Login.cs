using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;

public class Banco_Login {

	MySqlDataReader dados;
	MySqlCommand comando;

	Banco_Conexao conexao;
	Banco_Reconexao reconexao;
	Objeto_Player player;	

	public string retornaLogin(string login, string senha){

		/*
			METODO RETORNA LOGIN

			Metodo responsavel por verificar e retornar o login requisitado

			Ordem executada:
			> Conectar com o banco
			> Criar variaveis responsaveis por requisitar os dados do banco
			> Executar comandos de conexao e requisição
			> Se retornou algo, pegar dados, se não, reiniciar variaveis e veririficar proxima tabela, até acabar
			> Se passou por todas as tabelas e não encontrou nada então login invalido
			> Fechar conexao

			Créditos: Caio Roman Sant'anna
		
		 */
		conexao = new Banco_Conexao();
		reconexao = GameObject.Find("scripts").GetComponent<Banco_Reconexao>();

		player = new Objeto_Player();
		player.TipoLogin = "ERRO";		
								
		try{

			conexao.conectarBanco();

			/* --------------------------------- ESTUDANTE --------------------------------- */	
			conexao.Sql = "SELECT * FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_LOGIN='"+login+"' AND USER_ESTUDANTE_SENHA='"+senha+"';";
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

					player.TipoLogin = "ESTUDANTE";
				}
				
			}else{
				dados.Close();           
				comando.Dispose();
					
				/* --------------------------------- PROFESSOR --------------------------------- */							
				conexao.Sql = "SELECT * FROM TB_USER_PROFESSOR WHERE USER_PROFESSOR_LOGIN='"+login+"' AND USER_PROFESSOR_SENHA='"+senha+"';";
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

						player.TipoLogin = "PROFESSOR";					
					}
					
				}else{
					dados.Close();
					comando.Dispose();
					
					/* --------------------------------- MONITOR --------------------------------- */	
					conexao.Sql = "SELECT * FROM TB_USER_MONITOR WHERE USER_MONITOR_LOGIN='"+login+"' AND USER_MONITOR_SENHA='"+senha+"';";
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

							player.TipoLogin = "MONITOR";
						}
						
					}else{
						dados.Close();
						comando.Dispose();
						
						/* --------------------------------- ADM --------------------------------- */					
						conexao.Sql = "SELECT * FROM TB_USER_ADM WHERE USER_ADM_LOGIN='"+login+"' AND USER_ADM_SENHA='"+senha+"';";
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

								player.TipoLogin = "ADM";
							}
							
						}else{
							dados.Close();
							comando.Dispose();							

							player.TipoLogin = "FALSO";
						}
					}
				}
				
			}		

			conexao.fecharBanco();

		}catch{
			reconexao.realizarReconexao();			
		}
		return player.TipoLogin;
	}
	
}

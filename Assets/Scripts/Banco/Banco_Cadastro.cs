using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class Banco_Cadastro {

	MySqlDataReader dados;
	MySqlCommand comando;
	Banco_Conexao conexao;
	Banco_Reconexao reconexao;
	Objeto_Player player;
	public bool verificaCodigo(string codigo){

		/*
			METODO VERIFICA CODIGO

			Metodo responsavel por verificar se o codigo da sala é valído

			Ordem executada:
			> Conectar com o banco
			> Realizar comando
			> Se existe, transportar dados para o objeto player e retornar true
			> Se não existe, retornar false
			> Fechar banco

			Créditos: Caio Roman Sant'anna
		
		 */

		conexao = new Banco_Conexao();
		player = new Objeto_Player();
		reconexao = GameObject.Find("scripts").GetComponent<Banco_Reconexao>();

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
				return true;
			}else{
				dados.Close();           
				comando.Dispose();
				conexao.fecharBanco();
				return false;
			}
		}catch{
			dados.Close();           
			comando.Dispose();			
			reconexao.realizarReconexao();
			return false;
		}	
	}

	public bool verificaInfo(string nome, string email, string cpf){

		/*
			METODO VERIFICA INFO

			Metodo responsavel por verificar se as informações básicas já existem no banco

			Ordem executada:
			> Conectar com o banco
			> Realizar comando com email
			> Se existe, retornar false
			> Se não existe, testar comando com cpf
			> Se existe, retornar false
			> Se não existe, transpotar informações para a classe objeto player e retornar true
			> Fechar banco

			Créditos: Caio Roman Sant'anna
		
		 */

		conexao = new Banco_Conexao();
		reconexao = new Banco_Reconexao();
		player = new Objeto_Player();

		try{
			conexao.conectarBanco();
			conexao.Sql = "SELECT USER_ESTUDANTE_ID FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_EMAIL='"+email+"';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){
				dados.Close();
				comando.Dispose();
				conexao.fecharBanco();
				return false;
			}else{
				conexao.conectarBanco();
				conexao.Sql = "SELECT USER_ESTUDANTE_ID FROM TB_USER_ESTUDANTE WHERE USER_ESTUDANTE_CPF='"+cpf+"';";
				comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
				dados = comando.ExecuteReader();

				if(dados.HasRows){
					dados.Close();
					comando.Dispose();
					conexao.fecharBanco();
					return false;
				}else{					
					player.Nome = nome;
					player.Email = email;
					player.Cpf = cpf;

					dados.Close();
					comando.Dispose();
					conexao.fecharBanco();
					return true;
				}
			}
		}catch{
			dados.Close();           
			comando.Dispose();			
			reconexao.realizarReconexao();
			return false;
		}	
	}
}

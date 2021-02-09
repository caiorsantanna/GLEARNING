using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Banco_Conexao {

	private string linhaConexao = @"
	Server=157.230.62.123;
	Port=3306;
	Database=glearning;
	Uid=glearning_user;
	Pwd=glearning123456;
	Pooling=false;
	old guids=true;
	AllowUserVariables=True;
	CharSet=utf8";

  private static MySqlConnection conexaoBanco;
	private static string sql;

	public string Sql
	{
		get
		{
			return sql;
		}
		set
		{
			sql = value;
		}
	}

	public MySqlConnection ConexaoBanco
	{
		get
		{
			return conexaoBanco;
		}
		set
		{
			conexaoBanco = value;
		}
	}

	public void conectarBanco(){
		ConexaoBanco = new MySqlConnection(this.linhaConexao);
		ConexaoBanco.Open();
	}

	public void fecharBanco(){
		ConexaoBanco.Close();
	}
}
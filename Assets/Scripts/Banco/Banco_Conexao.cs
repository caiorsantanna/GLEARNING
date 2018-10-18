using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Banco_Conexao {

	private string linhaConexao = ""+
		"Server=localhost;"+
		"Database=glearning;"+
		"User ID=root;"+
		"Password=;"+
		"Pooling=false;";
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
		//print("Conexao Banco: "+conexaoBanco.State);
	}

	public void fecharBanco(){
		ConexaoBanco.Close();
		//print("Conexao Banco: "+conexaoBanco.State);
	}
}
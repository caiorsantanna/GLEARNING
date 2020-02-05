using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Classe_Controle_Licao8_Atv1 : MonoBehaviour
{
  public GameObject pnl_main, pnl_acertos;
  public List<string> nomes = new List<string>();
  MySqlCommand command;
  MySqlDataReader data;
  Banco_Conexao connection = new Banco_Conexao();
  Objeto_Player player = new Objeto_Player();
  int level = 1;
  int correctAnswers = 0;


  void Start()
  {
    	try
      {
        connection.conectarBanco();

        connection.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=8";
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

        List<string> banco_nomes = new List<string>();
        List<string> banco_sobrenomes = new List<string>();

        //Nomes
        connection.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Nome' AND (CONTEUDO_TAG1 = 'Masculino' OR CONTEUDO_TAG1 = 'Feminino');";
				command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
				data = command.ExecuteReader();

				if(data.HasRows){
					while(data.Read()){
						banco_nomes.Add(data["CONTEUDO_TEXTO"].ToString());
					}
				}

				data.Close();
				command.Dispose();

        //Sobrenomes
        connection.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Sobrenome';";
				command = new MySqlCommand(connection.Sql, connection.ConexaoBanco);
				data = command.ExecuteReader();

				if(data.HasRows){
					while(data.Read()){
						banco_sobrenomes.Add(data["CONTEUDO_TEXTO"].ToString());
					}
				}

				data.Close();
				command.Dispose();

        for(int i = 0; i < banco_nomes.Count; i++) {
          nomes.Add($"{banco_nomes[i]} {banco_sobrenomes[i]}");
        }

				connection.fecharBanco();
			} catch (MySqlException e) {
				Debug.LogError(e);
			}

  }
}

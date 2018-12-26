using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Classe_Controle_Licao2_Atv1 : MonoBehaviour
{
    public int nivel = 1;

    MySqlCommand comando;
    MySqlDataReader dados;
    
    void Start()
    {
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();
    }
}

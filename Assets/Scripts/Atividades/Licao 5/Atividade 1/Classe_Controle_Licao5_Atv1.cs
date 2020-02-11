using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Controle_Licao5_Atv1 : MonoBehaviour
{
    public GameObject pnl_loading;
    public Text txt_dialogo, txt_dialogo_waiter;
    public Dropdown dpw_comidas, dpw_bebidas;
    public GameObject pnl_dialogo_inicial, pnl_acertos;
    public Image img_estrela_1, img_estrela_2, img_estrela_3;
    public Sprite estrela_sim, estrela_nao;

    public List<string> comidas_entrada = new List<string>();
    public List<string> bebidas_entrada = new List<string>();
    public List<string> comidas_almoco = new List<string>();
    public List<string> bebidas_almoco = new List<string>();

    public List<string> comidas_entrada_traducao = new List<string>();
    public List<string> bebidas_entrada_traducao = new List<string>();
    public List<string> comidas_almoco_traducao = new List<string>();
    public List<string> bebidas_almoco_traducao = new List<string>();

    //VARIVAVEL DO NIVEL DA ATIVIDADE
    int qtdAMenos = 4;
    public int acertos = 0;

    public string comida_escolhida, bebida_escolhida;
    public string horario = "Entrada";

    //VARIAVEIS DE BANCO
    MySqlCommand comando;
    MySqlDataReader dados;

    void Start()
    {
        //INICIALIZA AS CLASSES 'CONEXAO' E 'PLAYER'
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        //CONTATO COM O BANCO DE DADOS
        try
        {
            conexao.conectarBanco();

            comidas_entrada_traducao.Add("Choose");
            comidas_almoco_traducao.Add("Choose");
            bebidas_entrada_traducao.Add("Choose");
            bebidas_almoco_traducao.Add("Choose");

            //COMANDO SQL NIVEL 2
            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=5";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    int n = (int)dados["NIVEL_ATIVIDADE"];
                    if (n <= 10)
                    {
                        qtdAMenos = 4;
                    }
                    else if (n <= 20)
                    {
                        qtdAMenos = 2;
                    }
                    else
                    {
                        qtdAMenos = 0;
                    }
                }
            }

            dados.Close();
            comando.Dispose();

            // PEGAR CONTEUDO LICAO 5 ATIVIDADE 1
            conexao.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TIPO, CONTEUDO_TAG2, CONTEUDO_TAG3 FROM TB_CONTEUDOS WHERE CONTEUDO_TAG1 = 'Licao5_Atv1';";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    switch (dados["CONTEUDO_TIPO"].ToString())
                    {
                        case "Comida":
                            switch (dados["CONTEUDO_TAG3"].ToString())
                            {
                                case "Almoco":
                                    comidas_almoco.Add(dados["CONTEUDO_TEXTO"].ToString());
                                    comidas_almoco_traducao.Add(dados["CONTEUDO_TAG2"].ToString());
                                    break;
                                case "Entrada":
                                    comidas_entrada.Add(dados["CONTEUDO_TEXTO"].ToString());
                                    comidas_entrada_traducao.Add(dados["CONTEUDO_TAG2"].ToString());
                                    break;
                            }
                            break;
                        case "Bebida":
                            switch (dados["CONTEUDO_TAG3"].ToString())
                            {
                                case "Almoco":
                                    bebidas_almoco.Add(dados["CONTEUDO_TEXTO"].ToString());
                                    bebidas_almoco_traducao.Add(dados["CONTEUDO_TAG2"].ToString());
                                    break;
                                case "Entrada":
                                    bebidas_entrada.Add(dados["CONTEUDO_TEXTO"].ToString());
                                    bebidas_entrada_traducao.Add(dados["CONTEUDO_TAG2"].ToString());
                                    break;
                            }
                            break;
                    }
                }
            }

            dados.Close();
            comando.Dispose();

            txt_dialogo.text = "Olá " + player.Nome + ", obrigado por almoçar comigo, eu sou novo aqui e eu não sei falar em inglês, você poderia fazer o pedido por mim?";

            conexao.fecharBanco();
        }
        catch(MySqlException e)
        {
            print(e);
        }
    }  
    
    public void Metodo_Continuar_Inicio()
    {
        int rNComida, rNBebida;
        switch (horario)
        {
            case "Entrada":                
                rNComida = Random.Range(0, comidas_entrada.ToArray().Length - qtdAMenos);
                rNBebida = Random.Range(0, bebidas_entrada.ToArray().Length - qtdAMenos);

                comida_escolhida = comidas_entrada_traducao[rNComida+1];
                bebida_escolhida = bebidas_entrada_traducao[rNBebida+1];

                txt_dialogo.text = "Muito Obrigado! Bom, de entrada, eu gostaria de " + comidas_entrada[rNComida] +
                    " para comer, e para beber eu gostaria de " + bebidas_entrada[rNBebida] + ".";
                txt_dialogo_waiter.text = "Hello sir, what would you like for an appetizer?";

                dpw_comidas.ClearOptions();
                dpw_bebidas.ClearOptions();
                dpw_comidas.AddOptions(comidas_entrada_traducao);
                dpw_bebidas.AddOptions(bebidas_entrada_traducao);

                break;
            case "Almoco":
                rNComida = Random.Range(0, comidas_almoco.ToArray().Length - qtdAMenos);
                rNBebida = Random.Range(0, bebidas_almoco.ToArray().Length - qtdAMenos);

                comida_escolhida = comidas_almoco_traducao[rNComida+1];
                bebida_escolhida = bebidas_almoco_traducao[rNBebida+1];

                txt_dialogo.text = "Agora, de almoço, eu gostaria de " + comidas_almoco[rNComida] +
                    " para comer, e para beber eu gostaria de " + bebidas_almoco[rNBebida] + ".";

                txt_dialogo_waiter.text = "Hi sir, what would you like for lunch ?";

                dpw_comidas.ClearOptions();
                dpw_bebidas.ClearOptions();
                dpw_comidas.AddOptions(comidas_almoco_traducao);
                dpw_bebidas.AddOptions(bebidas_almoco_traducao);
                break;
        }
    }

    public void Metodo_Conferir_Cardapio()
    {
        if (dpw_comidas.captionText.text == comida_escolhida)
        {
            acertos++;
        }
        if (dpw_bebidas.captionText.text == bebida_escolhida)
        {
            acertos++;
        }

        switch (horario)
        {
            case "Entrada":
                horario = "Almoco";
                pnl_dialogo_inicial.SetActive(true);                
                Metodo_Continuar_Inicio();
                break;
            case "Almoco":
                pnl_acertos.SetActive(true);

                //CALCULANDO PORCENTAGEM DE ACERTOS
                decimal porcentagem_acertos = (acertos * 100) / 4;

                print(porcentagem_acertos);

                //DEFININDO PONTOS FINAIS
                if (porcentagem_acertos < 50)
                {
                    img_estrela_1.sprite = estrela_nao;
                    img_estrela_2.sprite = estrela_nao;
                    img_estrela_3.sprite = estrela_nao;
                }
                else if (porcentagem_acertos < 75)
                {
                    img_estrela_1.sprite = estrela_sim;
                    img_estrela_2.sprite = estrela_nao;
                    img_estrela_3.sprite = estrela_nao;
                }
                else if (porcentagem_acertos < 100)
                {
                    img_estrela_1.sprite = estrela_sim;
                    img_estrela_2.sprite = estrela_sim;
                    img_estrela_3.sprite = estrela_nao;
                }
                else //SE ACERTOU TUDO, ALMENTAR UM NIVEL DA ATIVIDADE
                {
                    img_estrela_1.sprite = estrela_sim;
                    img_estrela_2.sprite = estrela_sim;
                    img_estrela_3.sprite = estrela_sim;
                    Metodo_Adicionar_Pontos_Licao5_Atv1();
                }


                break;
        }
    }

    public void Metodo_Adicionar_Pontos_Licao5_Atv1()
    {
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        try
        {
            //PEGA NIVEL ATUAL DA ATIVIDADE
            conexao.conectarBanco();

            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=5";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();
            int n = 0;
            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    n = (int)dados["NIVEL_ATIVIDADE"];
                    print(n);
                }
            }

            dados.Close();
            comando.Dispose();

            //ALMENTA UM NIVEL
            conexao.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=5";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            comando.ExecuteNonQuery();

            conexao.fecharBanco();
        }
        catch
        {

        }
    }

    public void Metodo_Voltar_Menu_Principal()
    {
        pnl_loading.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
    }


}

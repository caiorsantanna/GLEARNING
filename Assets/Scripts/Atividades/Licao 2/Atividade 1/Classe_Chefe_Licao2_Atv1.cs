using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Chefe_Licao2_Atv1 : MonoBehaviour
{
    //LISTA DE EMPRESAS PARA VERIFICAÇÃO
    List<List<string>> empresas = new List<List<string>>();
    public List<string> empresa1 = new List<string>();
    public List<string> empresa2 = new List<string>();
    public List<string> empresa3 = new List<string>();
    public List<string> empresa4 = new List<string>();
    public List<string> empresa5 = new List<string>();
    public List<string> empresa6 = new List<string>();

    //PAINEIS DAS EMPRESAS NO RELATORIO
    public GameObject pnl_empresa_1, pnl_empresa_2, pnl_empresa_3, pnl_empresa_4, pnl_empresa_5, pnl_empresa_6;
    //DROPDOWN DAS EMPRESAS
    public Dropdown dpw_nomes, dpw_descricoes;

    //LOGOS DAS EMPRESAS NO RELATÓRIO
    public Image logo_1_1, logo_2_1, logo_3_1;
    public Image logo_1_2, logo_2_2, logo_3_2;
    public Image logo_1_3, logo_2_3, logo_3_3;
    public Image logo_1_4, logo_2_4, logo_3_4;
    public Image logo_1_5, logo_2_5, logo_3_5;
    public Image logo_1_6, logo_2_6, logo_3_6;

    //LOGO DO PAINEL DO RELATORIO
    public Image logo_1_info, logo_2_info, logo_3_info;

    //ESTRELAS DE CONCLUSÃO
    public Image img_estrela_1, img_estrela_2, img_estrela_3;

    //IMAGEM DA ESTRELA
    public Sprite estrela_sim, estrela_nao;

    //OBJETO DO BOTÃO DE CONVERSAÇÃO
    public GameObject canvas_dialogo_começo;
    public GameObject canvas_botao_chefe;

    int nEmpresa;

    void Start()
    {                
        //INICIALIZANDO VETORES PARA VERIFICAÇÃO FINAL
        empresa1.Add(null);
        empresa1.Add(null);

        empresa2.Add(null);
        empresa2.Add(null);

        empresa3.Add(null);
        empresa3.Add(null);

        empresa4.Add(null);
        empresa4.Add(null);

        empresa5.Add(null);
        empresa5.Add(null);

        empresa6.Add(null);
        empresa6.Add(null);

        empresas.Add(empresa1);
        empresas.Add(empresa2);
        empresas.Add(empresa3);
        empresas.Add(empresa4);
        empresas.Add(empresa5);
        empresas.Add(empresa6);


        canvas_dialogo_começo.SetActive(true);        
    }

    public void Metodo_Iniciar_Relatorio()
    {
        Classe_Controle_Licao2_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao2_Atv1>();

        //DEFININDO QUANTOS RELATÓRIOS TERAM DE ACORDO COM O NÍVEL
        switch (controle.nivel)
        {
            case 1:
                pnl_empresa_1.SetActive(true);
                logo_1_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_2.SetActive(true);
                logo_1_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                break;
            case 2:
                pnl_empresa_1.SetActive(true);
                logo_1_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_2.SetActive(true);
                logo_1_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_3.SetActive(true);
                logo_1_3.sprite = controle.npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_3.sprite = controle.npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_3.sprite = controle.npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_4.SetActive(true);
                logo_1_4.sprite = controle.npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_4.sprite = controle.npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_4.sprite = controle.npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                break;
            case 3:
                pnl_empresa_1.SetActive(true);
                logo_1_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_2.SetActive(true);
                logo_1_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_3.SetActive(true);
                logo_1_3.sprite = controle.npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_3.sprite = controle.npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_3.sprite = controle.npcs[2].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_4.SetActive(true);
                logo_1_4.sprite = controle.npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_4.sprite = controle.npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_4.sprite = controle.npcs[3].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_5.SetActive(true);
                logo_1_5.sprite = controle.npcs[4].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_5.sprite = controle.npcs[4].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_5.sprite = controle.npcs[4].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                pnl_empresa_6.SetActive(true);
                logo_1_6.sprite = controle.npcs[5].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_6.sprite = controle.npcs[5].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_6.sprite = controle.npcs[5].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                break;
            default:
                logo_1_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_1.sprite = controle.npcs[0].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;

                logo_1_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
                logo_2_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
                logo_3_2.sprite = controle.npcs[1].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;
                break;
        }
    }

    public void Metodo_Selecionar_InfoEmpresa(int nEmpresa)
    {
        Classe_Controle_Licao2_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao2_Atv1>();

        int index;

        //DEFININDO VARIAVEIS
        dpw_nomes.value = 0;
        this.nEmpresa = nEmpresa;

        //DEFININDO LOGO EM QUESTÃO
        logo_1_info.sprite = controle.npcs[nEmpresa].GetComponent<Classe_NPC_Licao2_Atv1>().logo_1;
        logo_2_info.sprite = controle.npcs[nEmpresa].GetComponent<Classe_NPC_Licao2_Atv1>().logo_2;
        logo_3_info.sprite = controle.npcs[nEmpresa].GetComponent<Classe_NPC_Licao2_Atv1>().logo_3;

        //PEGANDO VALORES SALVOS
        if (empresas[nEmpresa][0] != null)
        {
            index = dpw_nomes.options.FindIndex((i) => { return i.text.Equals(empresas[nEmpresa][0]); });
            dpw_nomes.value = index;
        }
        else{ dpw_nomes.value = 0; }

        if (empresas[nEmpresa][1] != null)
        {
            index = dpw_descricoes.options.FindIndex((i) => { return i.text.Equals(empresas[nEmpresa][1]); });
        dpw_descricoes.value = index;
         }
        else { dpw_descricoes.value = 0; }


    }

    public void Metodo_OK_Empresas()
    {
        //SALVANDO ALTERAÇÕES
        empresas[nEmpresa][0] = dpw_nomes.captionText.text;
        empresas[nEmpresa][1] = dpw_descricoes.captionText.text;
    }

    public void Metodo_Terminar_Licao2_Atv1()
    {
        Classe_Controle_Licao2_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao2_Atv1>();

        int acertos = 0;

        //CRIANDO LISTAS
        List<string> nomes = new List<string>();
        nomes.Add(empresas[0][0]);
        nomes.Add(empresas[1][0]);
        nomes.Add(empresas[2][0]);
        nomes.Add(empresas[3][0]);
        nomes.Add(empresas[4][0]);
        nomes.Add(empresas[5][0]);

        List<string> descricoes = new List<string>();
        descricoes.Add(empresas[0][1]);
        descricoes.Add(empresas[1][1]);
        descricoes.Add(empresas[2][1]);
        descricoes.Add(empresas[3][1]);
        descricoes.Add(empresas[4][1]);
        descricoes.Add(empresas[5][1]);

        //IDENTIFICANDO ACERTOS
        for (int i = 0; i < controle.nivel * 2; i++)
        {
            GameObject objNpc = controle.npcs[i].gameObject;
            Classe_NPC_Licao2_Atv1 npc = objNpc.GetComponent<Classe_NPC_Licao2_Atv1>();

            if (npc.nome.Equals(nomes[i]))
            {
                acertos++;
            }
            if (npc.descricao.Equals(descricoes[i]))
            {
                acertos++;
            }
        }

        //CALCULANDO PORCENTAGEM DE ACERTOS
        decimal porcentagem_acertos = (acertos * 100) / ((controle.nivel * 2) * 2);

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
            controle.Metodo_Adicionar_Pontos_Licao2_Atv1();
        }

    }


    //METODOS PARA APARECER BOTÃO
    void OnTriggerEnter2D()
    {
        canvas_botao_chefe.SetActive(true);
    }

    void OnTriggerExit2D()
    {
        canvas_botao_chefe.SetActive(false);
    }
}

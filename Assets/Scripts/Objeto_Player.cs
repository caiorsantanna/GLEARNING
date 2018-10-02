using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto_Player : MonoBehaviour {

	private static string id;
    private static string cpf;
    private static string nome;    
    private static string email;
    private static int nivel;
    private static int ptotais;
    private static int psemestre;
    private static int patuais;
    private static string tipoLogin;   

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Cpf
    {
        get
        {
            return cpf;
        }

        set
        {
            cpf = value;
        }
    }

    public string Nome
    {
        get
        {
            return nome;
        }

        set
        {
            nome = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }

        set
        {
            email = value;
        }
    }

    public int Nivel
    {
        get
        {
            return nivel;
        }

        set
        {
            nivel = value;
        }
    }

    public int Ptotais
    {
        get
        {
            return ptotais;
        }

        set
        {
            ptotais = value;
        }
    }

    public int Psemestre
    {
        get
        {
            return psemestre;
        }

        set
        {
            psemestre = value;
        }
    }

    public int Patuais
    {
        get
        {
            return patuais;
        }

        set
        {
            patuais = value;
        }
    }

    public string TipoLogin
    {
        get
        {
            return tipoLogin;
        }

        set
        {
            tipoLogin = value;
        }
    }
}

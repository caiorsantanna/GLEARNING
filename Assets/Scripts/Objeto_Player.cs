using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto_Player {	
    private static long cpf;
    private static string nome;    
    private static string email;
    private static int nivel;
    private static int ptotais;
    private static int psemestre;
    private static int patuais;
    private static string nomeSala;
    private static long codSala;
    private static string pele;
    private static string roupa;
    private static string cabelo;
    private static string acessorio;
    private static string tipoLogin;

    public long Cpf { get => cpf; set => cpf = value; }
    public string Nome { get => nome; set => nome = value; }
    public string Email { get => email; set => email = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Ptotais { get => ptotais; set => ptotais = value; }
    public int Psemestre { get => psemestre; set => psemestre = value; }
    public int Patuais { get => patuais; set => patuais = value; }
    public string NomeSala { get => nomeSala; set => nomeSala = value; }
    public long CodSala { get => codSala; set => codSala = value; }
    public string Pele { get => pele; set => pele = value; }
    public string Roupa { get => roupa; set => roupa = value; }
    public string Cabelo { get => cabelo; set => cabelo = value; }
    public string Acessorio { get => acessorio; set => acessorio = value; }
    public string TipoLogin { get => tipoLogin; set => tipoLogin = value; }
}

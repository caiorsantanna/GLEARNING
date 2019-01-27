using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Player : MonoBehaviour
{
    public SpriteRenderer base_sprite, roupa_sprite, cabelo_sprite; //acessorio_sprite;
    public Animator base_animator, roupa_animator, cabelo_animator; //acessorio_animator;

    void Start()
    {
        Objeto_Player player = new Objeto_Player();        
        base_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/"+player.Pele)[1];
        roupa_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Roupa)[1];
        cabelo_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Cabelo)[1];
        //acessorio_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Acessorio)[1];

        string sexo = player.Pele.Substring(2,1);
        switch (sexo)
        {
            case "M":
                base_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animações/Masculino/Base/"+player.Pele+"_controller");
                roupa_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animações/Masculino/Roupas/" + player.Roupa + "/" + player.Roupa + "_controller");
                cabelo_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animações/Masculino/Cabelos/" + player.Cabelo + "/" + player.Cabelo + "_controller");
                //acessorio_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animações/Masculino/Acessorios/" + player.Acessorio + "/" + player.Acessorio + "_controller");
                break;
            case "F":
                base_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animações/Feminino/Base/" + player.Pele + "_controller");
                roupa_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animações/Feminino/Roupas/" + player.Roupa + "/" + player.Roupa + "_controller");
                cabelo_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animações/Feminino/Cabelos/" + player.Cabelo + "/" + player.Cabelo + "_controller");
                //acessorio_animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animações/Masculino/Acessorios/" + player.Acessorio + "/" + player.Acessorio + "_controller");
                break;
            default:
                print("ERRO 27.CLASSE_PLAYER");
                break;
        }
        
    }    
}

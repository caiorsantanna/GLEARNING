using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Player_Licao6_Atv1 : MonoBehaviour
{
    public SpriteRenderer base_sprite, roupa_sprite, cabelo_sprite; //acessorio_sprite; 
    void Start()
    {
        Objeto_Player player = new Objeto_Player();
        base_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Pele)[1];
        roupa_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Roupa)[1];
        cabelo_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Cabelo)[1];
        //acessorio_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Acessorio)[1];
    }


}

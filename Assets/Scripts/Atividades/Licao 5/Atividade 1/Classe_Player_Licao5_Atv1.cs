using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Player_Licao5_Atv1 : MonoBehaviour
{
    public SpriteRenderer base_sprite, roupa_sprite, cabelo_sprite; //acessorio_sprite; 
    void Start()
    {
        Objeto_Player player = new Objeto_Player();
        base_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Pele)[7];
        roupa_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Roupa)[7];
        cabelo_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Cabelo)[7];
        //acessorio_sprite.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Acessorio)[1];
    }


}

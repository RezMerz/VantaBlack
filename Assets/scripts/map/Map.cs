using UnityEngine;
using System.Collections;

public class Map
{
    Player player;
    public Map(Player player)
    {
        this.player = player;
    }


    

    public void Fountain(int num)
    {
        player.ability.numberofuse += num;
    }

    
}

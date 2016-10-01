using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Toolkit{

    public static Vector2 VectorSum(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    public static Ability BlinkLvlUp(Ability playerAbility, Ability blockAbility)
    {
        foreach (Direction dir2 in playerAbility.direction)
        {
            bool b = true;
            foreach (Direction dir in blockAbility.direction)   
                if (dir == dir2)
                    b = false;
            if (b)
                playerAbility.direction.Add(dir2);
        }
        playerAbility.numberofuse += blockAbility.numberofuse;

        return playerAbility;
    }

    public static Ability JumpLvlUp(Ability playerAbility, Ability blockAbility)
    {
        playerAbility.numberofuse += blockAbility.numberofuse;
        playerAbility.function += blockAbility.function;
        return playerAbility;
    }

    public static Ability RopeLvlUp(Ability playerAbility, Ability blockAbility)
    {
        playerAbility.numberofuse += blockAbility.numberofuse;
        playerAbility.function += blockAbility.function;
        return playerAbility;
    }
    
    public static Ability GravityLvlUp(Ability playerAbility, Ability blockAbility)
    {
        foreach (Direction dir2 in playerAbility.direction)
        {
            bool b = true;
            foreach (Direction dir in blockAbility.direction)
                if (dir == dir2)
                    b = false;
            if (b)
                playerAbility.direction.Add(dir2);
        }
        playerAbility.numberofuse += blockAbility.numberofuse;

        return playerAbility;
    }
}


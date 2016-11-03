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

    public static Direction VectorToDirection(Vector2 vector)
    {
        if (vector.x == 1 && vector.y == 0)
            return Direction.Right;

        else if (vector.x == 0 && vector.y == 1)
            return Direction.Up;

        else if (vector.x == -1 && vector.y == 0)
            return Direction.Left;

        else if (vector.x == 0 && vector.y == -1)
            return Direction.Down;

        return Direction.Down;
    }

    public static bool IsWallOnTheWay(Wall wall, Direction movingdirection)
    {
        switch (wall.direction)
        {
            case Direction.Up: if (movingdirection == Direction.Down) return true; return false;
            case Direction.Down: if (movingdirection == Direction.Up) return true; return false;
            case Direction.Left: if (movingdirection == Direction.Right) return true; return false;
            case Direction.Right: if (movingdirection == Direction.Left) return true; return false;
            default: return true;
        }
    }

    public static bool IsWallOnTheWay(Vector2 position, Direction dir)
    {
        if(dir == Direction.Right)
        {
            foreach(Unit u in Database.database.units[(int)position.x, (int)position.y])
            {
                if(u.unitType == UnitType.Wall)
                {
                    if (((Wall)u).direction == Direction.Right)
                        return true;
                }
            }
            return false;
        }
        else if (dir == Direction.Left)
        {
            foreach (Unit u in Database.database.units[(int)position.x, (int)position.y])
            {
                if (u.unitType == UnitType.Wall)
                {
                    if (((Wall)u).direction == Direction.Left)
                        return true;
                }
            }
            return false;
        }
        else if (dir == Direction.Up)
        {
            foreach (Unit u in Database.database.units[(int)position.x, (int)position.y])
            {
                if (u.unitType == UnitType.Wall)
                {
                    if (((Wall)u).direction == Direction.Up)
                        return true;
                }
            }
            return false;
        }
        else if (dir == Direction.Down)
        {
            foreach (Unit u in Database.database.units[(int)position.x, (int)position.y])
            {
                if (u.unitType == UnitType.Wall)
                {
                    if (((Wall)u).direction == Direction.Down)
                        return true;
                }
            }
            return false;
        }
        return false;
    }

    public static bool IsDoorOnTheway(Vector2 position, Direction dir)
    {
        if (dir == Direction.Right)
        {
            foreach (Unit u in Database.database.units[(int)position.x, (int)position.y])
            {
                if (u.unitType == UnitType.Door)
                {
                    if (((Door)u).direction == Direction.Right)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
        else if (dir == Direction.Left)
        {
            foreach (Unit u in Database.database.units[(int)position.x, (int)position.y])
            {
                if (u.unitType == UnitType.Door)
                {
                    if (((Door)u).direction == Direction.Left)
                        return false;
                }
                return true;
            }
            return false;
        }
        else if (dir == Direction.Up)
        {
            foreach (Unit u in Database.database.units[(int)position.x, (int)position.y])
            {
                if (u.unitType == UnitType.Door)
                {
                    if (((Door)u).direction == Direction.Up)
                        return false;
                }
                return true;
            }
            return false;
        }
        else if (dir == Direction.Down)
        {
            foreach (Unit u in Database.database.units[(int)position.x, (int)position.y])
            {
                if (u.unitType == UnitType.Door)
                {
                    if (((Door)u).direction == Direction.Down)
                        return false;
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    public static Direction ReverseDirection(Direction d)
    {
        switch (d)
        {
            case Direction.Up: return Direction.Down;
            case Direction.Down: return Direction.Up;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
            default: return Direction.Up;
        }
    }

    public static Door GetDoor(GameObject door)
    {
        InternalDoor d1 = door.GetComponent<InternalDoor>();
        if (d1 != null)
            return d1;
        ExternalDoor d2 = door.GetComponent<ExternalDoor>();
        if (d2 != null)
            return d2;
        return null;
    }

    public static bool IsEmptySpace(Vector2 position,  Direction d)
    {
        if (IsDoorOnTheway(position, d) || IsWallOnTheWay(position, d))
            return false;
        Wall.print(position);
        foreach (Unit u in Database.database.units[(int)position.x, (int)position.y])
        {
            if (u.unitType == UnitType.Wall || u.unitType == UnitType.Door || u.unitType == UnitType.Switch || u.unitType == UnitType.Pipe)
                continue;
        }
        return true;
    }
}


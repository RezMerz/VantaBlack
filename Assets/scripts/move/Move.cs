using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class Move{
    /// <summary>
    /// dec
    /// </summary>

    public GraphicalEngine Gengine;
    public Player player;
    public Database database;
    LogicalEngine engine;

    
    public Move(LogicalEngine engine)
    {
        this.Gengine = engine.Gengine;
        this.player = engine.player;
        this.database = engine.database;
        this.engine = engine;
    }

    
    public void move(Direction dir)
    {
        database.units[(int)player.transform.position.x, (int)player.transform.position.y].Remove(player);
        for (int i = 0; i < database.units[(int)player.transform.position.x, (int)player.transform.position.y].Count; i++)
        {
            Unit u = database.units[(int)player.transform.position.x, (int)player.transform.position.y][i];
            if (u.unitType == UnitType.Switch && ((Switch)u).isAutomatic && ((Switch)u).isOn)
                ((Switch)u).Run();
        }
        engine.action.CheckAutomaticSwitch(player.obj.transform.position);
        Gengine._move(dir);
        player.position = database.player.transform.position;
        database.units[(int)player.transform.position.x, (int)player.transform.position.y].Add(player);
        engine.action.CheckAutomaticSwitch(player.obj.transform.position);
        
        //engine.NextTurn();
    }
    class t{

    }
    private bool MoveObjects(Unit unit, Direction d)
    {
        Wall.print(unit);
        Vector2 temp;
        
        if(unit.unitType != UnitType.Wall)
            if (Toolkit.IsWallOnTheWay(unit.transform.position, d))
                return false;
        switch (d)
        {
            case Direction.Down: temp = Toolkit.VectorSum(unit.transform.position, new Vector2(0, -1)); break;
            case Direction.Up: temp = Toolkit.VectorSum(unit.transform.position, new Vector2(0, 1)); break;
            case Direction.Left: temp = Toolkit.VectorSum(unit.transform.position, new Vector2(-1, 0)); break;
            case Direction.Right: temp = Toolkit.VectorSum(unit.transform.position, new Vector2(1, 0)); break;
            default: temp = new Vector2(0, 0); break;
        }
        for(int i =0; i< database.units[(int)temp.x, (int)temp.y].Count; i++)
        {
            Unit u = database.units[(int)temp.x, (int)temp.y][i];
            Wall.print(u.unitType);
            if (u.layer == 2)
                continue;
            if (u.unitType == UnitType.Wall)
                continue;
            
            if (u.movable)
            {
                if (unit.CanMove(u.unitType) || u.CanBeMoved)
                {
                    if (!MoveObjects(u, d))
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        database.units[(int)unit.transform.position.x, (int)unit.transform.position.y].Remove(unit);
        for(int i=0; i< database.units[(int)unit.transform.position.x, (int)unit.transform.position.y].Count; i++)
        {
            Unit u = database.units[(int)unit.transform.position.x, (int)unit.transform.position.y][i];
            if (u.unitType == UnitType.Switch && ((Switch)u).isAutomatic && ((Switch)u).isOn)
                ((Switch)u).Run();
        }
        if (unit.unitType == UnitType.Wall)
        {
            switch (((Wall)unit).direction)
            {
                case Direction.Right:
                    database.units[(int)unit.transform.position.x + 1, (int)unit.transform.position.y].Remove(unit.gameObject.GetComponents<Wall>()[1]);
                    break;
                case Direction.Left:
                    database.units[(int)unit.transform.position.x - 1, (int)unit.transform.position.y].Remove(unit.gameObject.GetComponents<Wall>()[0]);
                    break;
                case Direction.Up:
                    database.units[(int)unit.transform.position.x, (int)unit.transform.position.y + 1].Remove(unit.gameObject.GetComponents<Wall>()[1]);
                    break;
                case Direction.Down:
                    database.units[(int)unit.transform.position.x, (int)unit.transform.position.y - 1].Remove(unit.gameObject.GetComponents<Wall>()[0]);
                    break;
            }
        }
        engine.action.CheckAutomaticSwitch(unit.obj.transform.position);
        GraphicalEngine.MoveObject(unit.obj, temp);
        database.units[(int)temp.x, (int)temp.y].Add(unit);
        if (unit.unitType == UnitType.Wall)
        {
            switch (((Wall)unit).direction)
            {
                case Direction.Right:
                    database.units[(int)temp.x + 1, (int)temp.y].Add(unit.gameObject.GetComponents<Wall>()[1]);
                    break;
                case Direction.Left:
                    database.units[(int)temp.x - 1, (int)temp.y].Add(unit.gameObject.GetComponents<Wall>()[0]);
                    break;
                case Direction.Up:
                    database.units[(int)temp.x, (int)temp.y + 1].Add(unit.gameObject.GetComponents<Wall>()[1]);
                    break;
                case Direction.Down:
                    database.units[(int)temp.x, (int)temp.y - 1].Add(unit.gameObject.GetComponents<Wall>()[0]);
                    break;
            }
        }
        else if (unit.unitType == UnitType.Rock)
        {
            for (int i = 0; i < ((Rock)unit).connectedUnits.Count; i++)
            {
                database.units[(int)((Rock)unit).connectedUnits[i].obj.transform.position.x, (int)((Rock)unit).connectedUnits[i].obj.transform.position.y].Remove((Switch)((Rock)unit).connectedUnits[i]);

                Wall.print((Toolkit.DirectiontoVector(Toolkit.ReverseDirection(((Switch)((Rock)unit).connectedUnits[i]).direction))));
                Vector2 temppos = Toolkit.VectorSum((Toolkit.DirectiontoVector(Toolkit.ReverseDirection(((Switch)((Rock)unit).connectedUnits[i]).direction))), unit.gameObject.transform.position);
                database.units[(int)temppos.x, (int)temppos.y].Add((Switch)((Rock)unit).connectedUnits[i]);
                Wall.print(temppos);
                GraphicalEngine.MoveObject(((Rock)unit).connectedUnits[i].obj, temppos);
                
            }
        }
        unit.position = unit.gameObject.transform.position;
        engine.action.CheckAutomaticSwitch(unit.obj.transform.position);
        return true;

    }
        

    public int MoveObjects(Unit unit, Direction d, int distance)
    {
        int counter = 0;
        for (int i = 0; i < distance; i++){
            if (MoveObjects(unit, d))
            {
                lock(database.units){
                    engine.ApplyGravity();
                }
                counter++;
            }
        }
        return counter;
        
    }
    public Unit CheckMovableItem(Vector2 position)
    {
        foreach (Unit u in database.units[(int)position.x, (int)position.y])
        {
            if (u.unitType == UnitType.Box || u.unitType == UnitType.Player)
            {

                return u;
            }
        }
        return null;
    }


    public void jump()
    {
        for (int i = 1; i < player.ability.function; i++)
        {
            switch (database.gravity_direction)
            {
                case Direction.Down: if (!CheckJump(Toolkit.VectorSum(player.position, new Vector2(0, i)))) Gengine._jump(i - 1); break;
                case Direction.Up: if (!CheckJump(Toolkit.VectorSum(player.position, new Vector2(0, -i)))) Gengine._jump(i - 1); break;
                case Direction.Right: if (!CheckJump(Toolkit.VectorSum(player.position, new Vector2(-i, 0)))) Gengine._jump(i - 1); break;
                case Direction.Left: if (!CheckJump(Toolkit.VectorSum(player.position, new Vector2(i, 0)))) Gengine._jump(i - 1); break;
            }
        }
        player.position = player.gameObject.transform.position;
        engine.CheckPointCheck();
        engine.NextTurn();
    }

    /// <summary>
    /// jump
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool CheckJump(Vector2 position)
    {
        foreach (Unit u in database.units[(int)position.x, (int)position.y])
        {
            if (u.unitType == UnitType.Block || u.unitType == UnitType.Container || u.unitType == UnitType.Container)
                return false;
            if (u.unitType == UnitType.Wall)
            {
                switch (database.gravity_direction)
                {
                    case Direction.Down: if (u.GetComponent<Wall>().direction == Direction.Up) return false; break;
                    case Direction.Up: if (u.GetComponent<Wall>().direction == Direction.Down) return false; break;
                    case Direction.Right: if (u.GetComponent<Wall>().direction == Direction.Left) return false; break;
                    case Direction.Left: if (u.GetComponent<Wall>().direction == Direction.Right) return false; break;
                }
            }
        }
        return true;
    }

    private bool CheckBlockandContainer(Vector2 position)
    {
        foreach (Unit u in database.units[(int)position.x, (int)position.y])
        {
            if (u.unitType == UnitType.Block || u.unitType == UnitType.Container)
                return false;
        }

        return true;
    }
}

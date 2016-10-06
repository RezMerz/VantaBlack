using UnityEngine;
using System.Collections;

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
        Gengine._move(dir);
        player.position = database.player.transform.position;
        
        //engine.NextTurn();
    }

    public void MoveObjects(Unit unit, Direction d, int distance) {
        Vector2 temp;
        switch (d)
        {
            case Direction.Down: temp = Toolkit.VectorSum(unit.transform.position, new Vector2(0, -distance)); break;
            case Direction.Up: temp = Toolkit.VectorSum(unit.transform.position, new Vector2(0, distance)); break;
            case Direction.Left: temp = Toolkit.VectorSum(unit.transform.position, new Vector2(-distance, 0)); break;
            case Direction.Right: temp = Toolkit.VectorSum(unit.transform.position, new Vector2(distance, 0)); break;
            default: temp = new Vector2(0,0);  break;
        }
         
        if (CheckBlockandContainer(temp))
        {
            database.units[(int)temp.x, (int)temp.y].Remove(unit);  
            GraphicalEngine.MoveObject(unit.obj , temp);
            database.units[(int)unit.transform.position.x, (int)unit.transform.position.y].Add(unit);

        }
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
            if (u.unitType == UnitType.Block || u   .unitType == UnitType.Container)
                return false;
        }

        return true;
    }
}

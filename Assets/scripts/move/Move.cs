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

    /// <summary>
    /// init
    /// </summary>
    /// <param name="Graphicalengine"></param>
    /// <param name="player"></param>
    /// <param name="database"></param>
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
        engine.NextTurn();
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
            if (u.type == UnitType.Block || u.type == UnitType.Container || u.type == UnitType.Container)
                return false;
            if (u.type == UnitType.Wall)
            {
                switch (database.gravity_direction)
                {
                    case Direction.Down: if (((Wall)u.component).direction == Direction.Up) return false; break;
                    case Direction.Up: if (((Wall)u.component).direction == Direction.Down) return false; break;
                    case Direction.Right: if (((Wall)u.component).direction == Direction.Left) return false; break;
                    case Direction.Left: if (((Wall)u.component).direction == Direction.Right) return false; break;
                }
            }
        }
        return true;
    }

}

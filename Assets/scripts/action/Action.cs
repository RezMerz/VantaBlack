using UnityEngine;
using System.Collections.Generic;

public class Action{
    Player player;
    Database database;
    GraphicalEngine Gengine;
    Move move;
    LogicalEngine engine;
    public Action(LogicalEngine engine)
    {
        this.player = engine.player;
        this.database = engine.database;
        this.Gengine = engine.Gengine;
        this.move = engine.moveObject;
        this.engine = engine;
    }

    public void Act()
    {
        if (player.ability == null)
            return;

        if (player.ability.abilitytype == AbilityType.Fuel)
            return;

        switch (player.ability.abilitytype)
        {
            case AbilityType.Direction: ChangeDirection(); break;
            case AbilityType.Jump: move.jump(); break;
            case AbilityType.Rope:Rope(); break;
        }
    }
    public void Act(Direction dir)
    {
        switch (player.ability.abilitytype)
        {
            case AbilityType.Blink: Blink(dir); break;
            case AbilityType.Gravity: ChangeGravity(dir); break;
        }
    }
    //private methods
    private void ChangeGravity(Direction direction)
    {
        for (int i = 0; i < player.ability.direction.Count; i++)
        {
            if (direction == player.ability.direction[i])
            {
                Direction temp = database.gravity_direction;
                database.gravity_direction = direction;
                player.ability.direction[i] = temp;
            }
        }
    }
    public void Blink(Direction dir)
    {
        if (CheckBlink(dir))
        {
            Gengine._blink(dir);
            engine.NextTurn();
        }
    }
    public void Teleport(Vector2 position)
    {
        Database.database.player.transform.position = position;
        engine.NextTurn();
    }
    public void Rope()
    {
        database.timeLaps.Add(new TimeLaps(player.ability.function, database.player));
    }

    public void SwitchActionPressed()
    {
        List<Unit> tlist = new List<Unit>();
        for (int i = 0; i < database.units[(int)player.transform.position.x, (int)player.transform.position.y].Count; i++)
        {
            Unit u = database.units[(int)player.transform.position.x, (int)player.transform.position.y][i];
            if (u.unitType == UnitType.Switch && !((Switch)u).isAutomatic && ((Switch)u).direction == database.gravity_direction && !((Switch)u).disabled)
            {
                tlist.Add(u);
                SwitchAction(u);
                return;
            }
            if (u.unitType == UnitType.BlockSwitch && ((BlockSwitch)u).isManual)
            {
                BlockSwitchAction((BlockSwitch)u);
                return;
            }
        }
        for(int i=0; i<tlist.Count; i++)
        {
            database.units[(int)tlist[i].obj.transform.position.x, (int)tlist[i].obj.transform.position.y].Remove(tlist[i]);
        }
        for (int i = tlist.Count - 1; i >= 0; i--)
        {
            database.units[(int)tlist[i].obj.transform.position.x, (int)tlist[i].obj.transform.position.y].Add(tlist[i]);
        }
        Vector2 temp = Toolkit.DirectiontoVector(database.gravity_direction);
        foreach (Unit u in database.units[(int)Toolkit.VectorSum(temp, player.transform.position).x, (int)Toolkit.VectorSum(temp, player.transform.position).y])
        {
            if (u.unitType == UnitType.BlockSwitch && ((BlockSwitch)u).isManual)
            {
                BlockSwitchAction(((BlockSwitch)u));
            }
        }
    }

    public void SwitchActionPressed(Direction d)
    {
        List<Unit> tlist = new List<Unit>();
        for (int i = 0; i < database.units[(int)player.transform.position.x, (int)player.transform.position.y].Count; i++)
        {
            Unit u = database.units[(int)player.transform.position.x, (int)player.transform.position.y][i];
            if (u.unitType == UnitType.Switch && d == ((Switch)u).direction && !((Switch)u).isAutomatic && !((Switch)u).disabled)
            {
                tlist.Add(u);
                SwitchAction(u);
                return;
            }
        }
        for (int i = 0; i < tlist.Count; i++)
        {
            database.units[(int)tlist[i].obj.transform.position.x, (int)tlist[i].obj.transform.position.y].Remove(tlist[i]);
        }
        for (int i = tlist.Count - 1; i >= 0; i--)
        {
            database.units[(int)tlist[i].obj.transform.position.x, (int)tlist[i].obj.transform.position.y].Add(tlist[i]);
        }
        Vector2 temp = Toolkit.VectorSum(Toolkit.DirectiontoVector(d), player.transform.position);
        for (int i = 0; i < database.units[(int)temp.x, (int)temp.y].Count; i++)
        {
            Unit u = database.units[(int)temp.x, (int)temp.y][i];
            if(u.unitType == UnitType.BlockSwitch && ((BlockSwitch)u).isManual)
            {
                BlockSwitchAction((BlockSwitch)u);
                return;
            }
        }
    }
    private void SwitchAction(Unit sw)
    {
        MovingSwitch t1 = null;
        DoorSwitch t2 = null;
        SwitchControler t3 = null;
        try
        {
             t1 = (MovingSwitch)sw;
        }catch { }

        if (t1 == null)
        {
            try
            {
                t2 = (DoorSwitch)sw;
            }
            catch { }
            if (t2 == null)
            {
                try
                {
                    t3 = (SwitchControler)sw;
                    if (t3 == null)
                    {

                    }
                    else
                    {
                        t3.Run();
                    }
                }
                catch { }
            }
            else
            {
                t2.Run();
            }
        }
        else
        {
            t1.Run();
        }

    }

    public void BlockSwitchAction(BlockSwitch block)
    {
        if (block.ability.abilitytype == AbilityType.Direction)
        {
            ChangeDirection();
        }
    }
    private void ChangeDirection()
    {
        switch (player.move_direction[0])
        {
            case Direction.Down: player.move_direction[0] = Direction.Up; break;
            case Direction.Up: player.move_direction[0] = Direction.Down; break;
            case Direction.Right: player.move_direction[0] = Direction.Left; break;
            case Direction.Left: player.move_direction[0] = Direction.Right; break;
        }
    }
    private bool CheckBlink(Direction direction)
    {
        bool value = false;
        foreach (Direction d in player.move_direction)
            if (direction == d)
                value = true;
        if (!value)
            return false;

        switch (direction)
        {
            case Direction.Up: return isvoid1(0, 2);
            case Direction.Down: return isvoid1(0, -2);
            case Direction.Right: return isvoid1(2, 0);
            case Direction.Left: return isvoid1(-2, 0);
            default: return false;
        }

    }
    private bool isvoid1(int x, int y)
    {
        foreach (Unit u in database.units[(int)player.position.x + x, (int)player.position.y + y])
        {
            if (u.unitType == UnitType.Block || u.unitType == UnitType.Container)
                return false;
        }
        return true;
    }

    public void RunContainer(Container container)
    {
        container.Run();
    }

    public void CheckAutomaticSwitch(Vector2 position)
    {
        int isempty = 0;
        for (int i = 0; i < database.units[(int)position.x, (int)position.y].Count; i++)
        {
            Unit u = database.units[(int)position.x, (int)position.y][i];
            if (u.unitType == UnitType.Block || u.unitType == UnitType.BlockSwitch || u.unitType == UnitType.Container || u.unitType == UnitType.Rock)
                isempty = 1;
            else if(u.unitType == UnitType.Player || u.unitType == UnitType.Box)
            {
                isempty = 2;
            }
        }
        for (int i=0; i<database.units[(int)position.x, (int)position.y].Count; i++)
        {
            
            Unit u = database.units[(int)position.x, (int)position.y][i];
            if (u.unitType == UnitType.Switch && ((Switch)u).isAutomatic)
            {
                if(isempty == 0 || isempty == 1)
                {
                    ((Switch)u).Run();
                }
                else
                {
                    if (database.gravity_direction == ((Switch)u).direction)
                    {
                        ((Switch)u).Run();
                    }
                }
            }
        }
    }
}

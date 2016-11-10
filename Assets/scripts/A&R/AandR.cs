using UnityEngine;
using System.Collections;

public class AandR {
    GraphicalEngine Gengine;
    Player player;
    Database database;
    LogicalEngine engine;

	public AandR(LogicalEngine engine)
    {
        this.Gengine = engine.Gengine;
        this.player = engine.player;
        this.database = engine.database;
        this.engine = engine;
    }
    

    public void Absorb(Direction dir)
    {
        if (Toolkit.IsWallOnTheWay(player.transform.position, dir))
            return;
        Unit unit = null;
        switch (dir)
        {
            case Direction.Down: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(0, -1))); break;
            case Direction.Up: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(0, 1))); break;
            case Direction.Right: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(1, 0))); break;
            case Direction.Left: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(-1, 0))); break;
        }
        if (unit != null)
        {
            if (unit.unitType == UnitType.Block)
                _absorb(((Block)unit).GetComponent<Block>());
            else if (unit.unitType == UnitType.Container)
            {
                DoContainer(unit);
            }
        }
    }
    
    public void Absorb()
    {
        if (Toolkit.IsWallOnTheWay(player.transform.position, database.gravity_direction))
            return;
        Unit unit = null;
        switch (database.gravity_direction)
        {
            case Direction.Down: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(0, -1)));  break;
            case Direction.Up: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(0, 1))); break;
            case Direction.Right: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(1, 0))); break;
            case Direction.Left: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(-1, 0))); break;
        }

        if (unit != null)
        {
            if (unit.unitType == UnitType.Block)
                _absorb(((Block)unit).GetComponent<Block>());
            else if (unit.unitType == UnitType.Container)
            {

                DoContainer(unit);
            }
            
        }
    }

    private void DoContainer(Unit unit)
    {
        Container container = (Container)unit;
        if (container.IsEmpty())
        {
            if(player.ability != null)
                Release(container);
        }
        else if (container.IsAvailable())
        {
            if (player.ability != null)
                Swap(container);
            else
                Absorb(container);
        }
        else
        {
            if (player.ability != null)
                SafeSwap(container);
            else
                Absorb(container);
        }
    }

    public void Drain()
    {
        player.ability = null;
    }
    /// <summary>
    /// Drains the Block :
    /// 1. logical
    /// </summary>
    /// <param name="block"></param>
    public void Drain(Block block)
    {
        block.ability = null;
    }

    //parivate functions

    private void Swap(Block block)
    {
        Ability block_ability = block.ability;
        block.ability = player.ability;
        player.ability = block_ability;

    }

    private void Release(Container container)
    {
        //Wall.print("releasing");
        container.ability = player.ability;
        player.ability = null;
        container.state++;
        container.forward = true;
        engine.action.RunContainer(container);
    }

    private void Swap(Container container)
    {
        //Wall.print("swaping");
        Ability container_ability = container.ability;
        player.ability = container_ability;
        if(container.ability.abilitytype == AbilityType.Fuel)
        {
            if(container.state == 1)
            {
                container.ability = null;
                container.state = 0;
                container.forward = false;
                engine.action.RunContainer(container);
            }
            else
            {
                container.state--;
                container.forward = false;
                engine.action.RunContainer(container);
            }

        }
        else
        {
            Absorb(container);
        }
    }

    private void Absorb(Container container)
    {
        //Wall.print("absorbing");
        if (container.ability.abilitytype == AbilityType.Fuel)
        {
            player.ability = container.ability;
            container.state--;
            container.forward = false;
            if (container.state == 0)
                container.ability = null;
            engine.action.RunContainer(container);
        }
        else
        {
            player.ability = container.ability;
            container.state = 0;
            container.forward = false;
            engine.action.RunContainer(container);
        }
    }

    private void SafeSwap(Container container)
    {
        //Wall.print("safe swapping");
        Ability containerAbility = container.ability;
        container.ability = player.ability;
        player.ability = container.ability;
    }
    private void _absorb(Block block)
    {
        if (block.ability == null)
        {
            ReleaseAbility(block);
        }
        else if(player.ability == null)
        {
            Swap(block);
            block.CheckPipe();
        }
        else if (block.ability.abilitytype == player.ability.abilitytype)
        {
            LevelUpAbility(block);
            block.CheckPipe();
        }
        else
        {
            Swap(block);
        }
    }
    private void LevelUpAbility(Block block)
    {
        switch (player.ability.abilitytype)
        {
            case AbilityType.Blink: player.ability = Toolkit.BlinkLvlUp(player.ability, block.ability); break;
            case AbilityType.Gravity: player.ability = Toolkit.GravityLvlUp(player.ability, block.ability); break;
            case AbilityType.Jump: player.ability = Toolkit.JumpLvlUp(player.ability, block.ability); break;
            case AbilityType.Rope: player.ability = Toolkit.RopeLvlUp(player.ability, block.ability); break;
        }

        Drain(block);
    }

    private void ReleaseAbility(Block block)
    {
        block.ability = player.ability;
        Drain();
    }

    private Unit GetBlockandContainer(Vector2 position)
    {
        foreach (Unit u in database.units[(int)position.x, (int)position.y])
        {
            if (u.unitType == UnitType.Block || u.unitType == UnitType.Container)
                return u;
        }

        return null;
    }
}

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
        if(container.ability == null)
        {
            if (player.ability == null)
                return;
            container._lastAbility = null;
            container.ability = player.ability;
            player.ability = null;
            container.state = 1;
            container.forward = true;
            engine.action.RunContainer(container);
            return;
        }
        else if (container.IsAvailable())
        {
            if(player.ability == null)
            {
                if (container.state == 1)
                {
                    container._lastAbility = container.ability;
                    _absorb(container);
                    container.state = 0;
                    container.forward = false;
                    engine.action.RunContainer(container);
                    return;
                }
                else
                {
                    container._lastAbility = null;
                    player.ability = container.ability;
                    container.forward = false;
                    container.state--;
                    engine.action.RunContainer(container);
                    return;
                }
            }
            else
            {
                if(player.ability.abilitytype == AbilityType.Fuel)
                {
                    container.state++;
                    container.forward = true;
                    player.ability = null;
                    engine.action.RunContainer(container);
                    return;
                }
                else
                {
                    if (container.state == 1)
                    {
                        container._lastAbility = container.ability;
                        Ability abil = _absorb(container);
                        container.state = 0;
                        container.forward = false;
                        engine.action.RunContainer(container);
                        container.ability = abil;
                        container.state = 1;
                        container.forward = true;
                        engine.action.RunContainer(container);
                        return;
                    }
                    else
                        return;
                }
            }
        }
        else
        {
            if(player.ability == null)
            {
                if(container.state == 1)
                {
                    player.ability = container.ability;
                    container._lastAbility = container.ability;
                    container.ability = null;
                    container.state = 0;
                    container.forward = false;
                    engine.action.RunContainer(container);
                    return;
                }
                else
                {
                    container._lastAbility = null;
                    player.ability = container.ability;
                    container.state--;
                    container.forward = false;
                    engine.action.RunContainer(container);
                    return;
                }
            }
            else
            {
                if (container.ability.abilitytype == player.ability.abilitytype)
                {
                    if (container.ability.abilitytype == AbilityType.Fuel)
                        return;
                    Ability abil = container.ability;
                    container.ability = player.ability;
                    player.ability = abil;
                    return;
                }
                else
                {
                    if (container.state == 1)
                    {
                        Ability abil = player.ability;
                        player.ability = container.ability;
                        container.state = 0;
                        container.forward = false;
                        engine.action.RunContainer(container);
                        container.ability = abil;
                        container.state = 1;
                        container.forward = true;
                        engine.action.RunContainer(container);
                    }
                    else
                        return;
                }
            }
        }
    }

    private Ability _absorb(Container container)
    {
        Ability abil = player.ability;
        player.ability = container.ability;
        container.ability = null;       
        return abil;
    }

    private Ability _release(Container container)
    {
        Ability abil = container.ability;
        container.ability = player.ability;
        player.ability = null;
        return abil;
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
        Wall.print("releasing");
        container.ability = player.ability;
        player.ability = null;
        container._lastAbility = null;
        container.state++;
        container.forward = true;
        engine.action.RunContainer(container);
    }

    private void Swap(Container container)
    {
 
        Ability container_ability = container.ability;
        Ability player_ability = player.ability;
        if(container.ability.abilitytype == AbilityType.Fuel)
        {
            if(container.state == 1)
            {
                Wall.print("c1");
                container._lastAbility = container.ability;
                container.ability = null;
                container.state = 0;
                container.forward = false;
                player.ability = container_ability;
                engine.action.RunContainer(container);
            }
            else
            {
                Wall.print("c2");
                container._lastAbility = null;
                container.state--;
                container.forward = false;
                container.ability = null;
                engine.action.RunContainer(container);
                Release(container);
                player.ability = container_ability;
            }

        }
        else
        {
            Wall.print("c3");
            container.ability = null;
            container.state = 0;
            container.forward = false;
            engine.action.RunContainer(container);
            Release(container);
            player.ability = container_ability;

        }
    }

    

    private void Absorb(Container container)
    {
        Wall.print("absorbing");
        if (container.ability.abilitytype == AbilityType.Fuel)
        {
            player.ability = container.ability;
            container.state--;
            container.forward = false;
            if (container.state == 0)
            {
                container._lastAbility = container.ability;
                container.ability = null;
            }
            else
                container.ability = null;
            engine.action.RunContainer(container);
        }
        else
        {
            player.ability = container.ability;
            container._lastAbility = container.ability;
            container.ability = null;
            container.state = 0;
            container.forward = false;
            engine.action.RunContainer(container);
        }
    }

    private void SafeSwap(Container container)
    {
        Wall.print("safe swapping");
        bool flag = true;
        if (player.ability.abilitytype == container.ability.abilitytype)
            flag = false;
        Ability containerAbility = container.ability;
        container.ability = player.ability;
        player.ability = containerAbility;
        container._lastAbility = containerAbility;
        container.state = 0;
        if(flag)
            container.Run();
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

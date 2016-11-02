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
        Unit unit = null;
        switch (database.gravity_direction)
        {
            case Direction.Down: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(0, -1)));  break;
            case Direction.Up: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(0, 1))); break;
            case Direction.Right: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(1, 0))); break;
            case Direction.Left: unit = GetBlockandContainer(Toolkit.VectorSum(player.transform.position, new Vector2(-1, 0))); break;
        }
        Wall.print(unit);
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
        MovingContainer c1 = ((Container)unit).GetComponent<MovingContainer>();
        DoorOpener c2 = ((Container)unit).GetComponent<DoorOpener>();
        if(c1 != null)
        {
            if (c1.IsEmpty())
            {
                Release(c1);
            }
            else if (c1.IsAvailable())
            {
                if (c1.state == 1)
                {
                    Swap(c1);
                }
                else
                {
                    Swap(c1);
                }
            }
            else
            {
                if (c1.ability.abilitytype == AbilityType.Fuel)
                {
                    Swap(c1);
                }
            }
        }
        else if (c2 != null)
        {
            if (c2.IsEmpty())
            {
                Release(c2);
            }
            else if (c2.IsAvailable())
            {
                if (c2.state == 1)
                {
                    Swap(c2);
                }
                else
                {
                    Swap(c2);
                }
            }
            else
            {
                if (c2.ability.abilitytype == AbilityType.Fuel)
                {
                    Swap(c2);
                }
            }
            
        }
        if (!((Container)unit).IsEmpty() && ((Container)unit).Unlockable)
        {
            foreach (AbilityType t in ((Container)unit).UnlockerAbilities)
            {
                if (((Container)unit).ability.abilitytype == t)
                {
                    ((Container)unit).CanBeMoved = true;
                    break;
                }
            }
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
        if (player.ability == null)
            return;
        container.ability = player.ability;
        player.ability = null;
        container.state++;
        if (((MovingContainer)container) != null)
            ((MovingContainer)container).forward = true;
        container.Run();
    }

    private void Swap(Container container)
    {
        Ability container_ability = container.ability;
        try
        {
            container.ability = player.ability;
        }
        catch
        {

            container.ability = null;
        }
        
        player.ability = container_ability;
        try {
            if (container.ability.abilitytype == AbilityType.Fuel)
            {
                container.state++;
                if (((MovingContainer)container) != null)
                    ((MovingContainer)container).forward = true;
            }
            else
            {
                container.state--;
                if (((MovingContainer)container) != null)
                    ((MovingContainer)container).forward = false;
            }
        }
        catch
        { 
            container.state--;
            if (((MovingContainer)container) != null)
                ((MovingContainer)container).forward = false;
        }
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

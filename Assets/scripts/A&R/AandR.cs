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
            case Direction.Down: unit = GetBlock(Toolkit.VectorSum(player.position, new Vector2(0, -1))); break;
            case Direction.Up: unit = GetBlock(Toolkit.VectorSum(player.position, new Vector2(0, 1))); break;
            case Direction.Right: unit = GetBlock(Toolkit.VectorSum(player.position, new Vector2(1, 0))); break;
            case Direction.Left: unit = GetBlock(Toolkit.VectorSum(player.position, new Vector2(-1, 0))); break;
        }
        if (unit != null)
        {
            if (unit.type == UnitType.Block)
                _absorb((Block)unit.component);
            else if (unit.type == UnitType.Container)
            {
                ((Container)unit.component).Run();
            }
        }
    }
    
    public void Absorb()
    {
        Unit unit = null;
        switch (database.gravity_direction)
        {
            case Direction.Down: unit = GetBlock(Toolkit.VectorSum(player.position, new Vector2(0, -1)));  break;
            case Direction.Up: unit = GetBlock(Toolkit.VectorSum(player.position, new Vector2(0, 1))); break;
            case Direction.Right: unit = GetBlock(Toolkit.VectorSum(player.position, new Vector2(1, 0))); break;
            case Direction.Left: unit = GetBlock(Toolkit.VectorSum(player.position, new Vector2(-1, 0))); break;
        }

        if (unit != null)
        {
            if (unit.type == UnitType.Block)
                _absorb((Block)unit.component);
            else if (unit.type == UnitType.Container)
            {
                ((Container)unit.component).Run();
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

    private void _absorb(Block block)
    {
        if (block.ability == null)
        {
            ReleaseAbility(block);
        }
        else if (block.ability.abilitytype == player.ability.abilitytype)
        {
            AbsorbAbility(block);
        }
        else
        {
            Swap(block);
        }
    }

    private void AbsorbAbility(Block block)
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

    private Unit GetBlock(Vector2 position)
    {
        foreach (Unit u in database.units[(int)position.x, (int)position.y])
            if (u.type == UnitType.Block)
                return u;

        return null;
    }
}

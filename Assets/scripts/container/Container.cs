using UnityEngine;
using System.Collections.Generic;

public class Container : Unit{
    public int numberofStates, state;
    public Ability ability;
    public bool Unlockable;
    public List<AbilityType> UnlockerAbilities;
    LogicalEngine engine;
    // Use this for initialization

    public void Start()
    {
        unitType = UnitType.Container;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = true;
        layer = 1;
    }

    public virtual void Run() { }

    public virtual void Check() { }

    public bool IsAvailable()
    {
        if (state == numberofStates - 1)
            return false;

        return true;
    }

    public bool IsEmpty()
    {
        if (ability == null)
            return true;
        return false;
    }

    public override bool CanMove(UnitType unittype)
    {
        if (unittype == UnitType.Box || unittype == UnitType.Player)
            return true;
        return false;
    }
}

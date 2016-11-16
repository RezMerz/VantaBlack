using UnityEngine;
using System.Collections.Generic;

public class Container : Unit{
    public int numberofStates, state;
    public Ability ability;
    public Ability _lastAbility { get; set; }
    public bool Unlockable;
    public List<AbilityType> UnlockerAbilities;
    LogicalEngine engine;

    public bool forward { get; set; }
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
        forward = true;
    }

    public void Run()
    {
        MovingContainer[] mv = gameObject.GetComponents<MovingContainer>();
        for (int i = 0; i < mv.Length; i++)
            mv[i].Run();
        DoorOpener[] dooropener = gameObject.GetComponents<DoorOpener>();
        for (int i = 0; i < dooropener.Length; i++)
            dooropener[i].Run();
        ContainerControler[] containerControler = gameObject.GetComponents<ContainerControler>();
        for (int i = 0; i < containerControler.Length; i++)
            containerControler[i].Run();
        MakeMovableContainer[] mkc = gameObject.GetComponents<MakeMovableContainer>();
        for (int i = 0; i < mkc.Length; i++)
            mkc[i].Run();
    }

    public virtual void Check() { }

    public bool IsAvailable()
    {
        if (ability == null)
            return true;
        if (ability.abilitytype == AbilityType.Fuel)
        {
            if (state == numberofStates - 1)
                return false;
            return true;
        }
        return false;
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

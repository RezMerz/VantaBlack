using UnityEngine;
using System.Collections.Generic;

public class Container : Unit{
    public int numberofStates, state;
    public Ability ability;
    public Ability _lastAbility { get; set; }
    LogicalEngine engine;

    public bool forward { get; set; }
    // Use this for initialization
    int counter;

    public List<MonoBehaviour> _saved_containers;
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
        counter = 0;
    }

    public void Run()
    {
        MovingContainer[] mv = obj.GetComponents<MovingContainer>();
        if (counter == 0)
        {
            for (; counter < mv.Length; counter++)
                mv[counter].Run();
            counter--;
        }
        else
        {
            for (; counter >= 0; counter--)
                mv[counter].Run();
            counter++;
        }
        DoorOpener[] dooropener = obj.GetComponents<DoorOpener>();
        for (int i = 0; i < dooropener.Length; i++)
            dooropener[i].Run();
        ContainerControler[] containerControler = obj.GetComponents<ContainerControler>();
        for (int i = 0; i < containerControler.Length; i++)
            containerControler[i].Run();
        MakeMovableContainer[] mkc = obj.GetComponents<MakeMovableContainer>();
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

    public override Unit Clone()
    {
        Container u = new Container();
        u.unitType = UnitType.Block;
        u.obj = obj;
        u.position = obj.transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;
        u.numberofStates = numberofStates;
        u.state = state;
        u._lastAbility = _lastAbility;
        u.ability = ability;
        return u;
    }
}

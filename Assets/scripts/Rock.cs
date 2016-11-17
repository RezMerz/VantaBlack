using UnityEngine;
using System.Collections.Generic;

public class Rock : Unit
{

    public List<Unit> connectedUnits;

    void Start()
    {
        unitType = UnitType.Rock;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = true;
        layer = 1;
    }
    public override bool CanMove(UnitType unittype)
    {
        if (unittype == UnitType.Box || unittype == UnitType.Player)
            return true;
        return false;
    }

    public override Unit Clone()
    {
        Rock u = new Rock();
        u.unitType = unitType;
        u.obj = obj;
        u.position = obj.transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;

        return u;
    }
}
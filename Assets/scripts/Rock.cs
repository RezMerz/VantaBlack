using UnityEngine;
using System.Collections.Generic;

public class Rock : Unit {

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
}

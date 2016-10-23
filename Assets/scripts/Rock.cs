using UnityEngine;
using System.Collections;

public class Rock : Unit {

	void Start()
    {
        unitType = UnitType.Rock;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = true;
    }
    public override bool CanMove(UnitType unittype)
    {
        if (unittype == UnitType.Box || unittype == UnitType.Player)
            return true;
        return false;
    }
}

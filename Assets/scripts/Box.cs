using UnityEngine;
using System.Collections;

public class Box : Unit {

	// Use this for initialization
	void Start () {
        unitType = UnitType.Box;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = true;
        CanBeMoved = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override bool CanMove(UnitType unittype)
    {
        if (unittype == UnitType.Box)
            return true;
        return false;
    }

    public override Unit Clone()
    {
        Box u = new Box();
        u.unitType = UnitType.Block;
        u.obj = obj;
        u.position = transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;

        return u;
    }
}

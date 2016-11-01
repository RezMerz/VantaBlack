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
        else 
            return false;
    }
}

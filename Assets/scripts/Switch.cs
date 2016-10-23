using UnityEngine;
using System.Collections;

public class Switch : Unit {

	// Use this for initialization
	void Start () {
        unitType = UnitType.Switch;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override bool CanMove(UnitType unittype)
    {
       
        return false;
    }
}

using UnityEngine;
using System.Collections;
using System;

public class BlockSwitch : Unit {

    public bool isManual;
    public Ability ability;
    // Use this for initialization
    void Start () {
        unitType = UnitType.BlockSwitch;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = true;
        layer = 1;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override bool CanMove(UnitType unittype)
    {
        if (unittype == UnitType.Box || unittype == UnitType.Player)
            return true;
        return false;
    }
}


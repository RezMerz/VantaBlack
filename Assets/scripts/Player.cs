using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Unit {

    public Ability ability;
    public List<Direction> move_direction;

	// Use this for initialization
	void Start () {
        unitType = UnitType.Player;
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

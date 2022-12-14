using UnityEngine;
using System.Collections;
using System;

public class BlockSwitch : Unit {

    public bool isManual;
    public Ability ability;
    public Direction direction;
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

    public override Unit Clone()
    {
        BlockSwitch u = new BlockSwitch();
        u.unitType = unitType;
        u.obj = obj;
        u.position = obj.transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;
        u.isManual = isManual;
        u.ability = ability;
        u.direction = direction;
        return u;
    }
    }


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Block : Unit
{

    public Ability ability;
    public Block Pipedto, Pipedfrom;


    // Use this for initialization
    void Start()
    {
        unitType = UnitType.Block;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = true;
        layer = 1;
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void CheckPipe()
    {
        if (Pipedfrom == null)
            return;
        if (ability == null && Pipedfrom.ability != null)
        {
            Pomp();
            Pipedfrom.CheckPipe();
        }
    }

    private void Pomp()
    {
        ability = Pipedfrom.ability;
        Pipedfrom.ClearBlock();
    }

    public void undo(Ability ability)
    {
        this.ability = ability;
    }

    public void ClearBlock()
    {
        ability = null;
    }

    public override bool CanMove(UnitType unittype)
    {
        if (unittype == UnitType.Box || unittype == UnitType.Player)
            return true;
        return false;
    }

    public override Unit Clone()
    {
        Block u = new Block();
        u.unitType = unitType;
        u.obj = obj;
        u.position = obj.transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;
        u.ability = ability;
        u.Pipedfrom = Pipedfrom;
        u.Pipedto = Pipedto;
        return u;
    }
}



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Player : Unit
{

    public Ability ability;
    public List<Direction> move_direction;
    public string current_scene;
    public string current_room;
    // Use this for initialization
    void Start()
    {
        unitType = UnitType.Player;
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
    public override bool CanMove(UnitType unittype)
    {
        if (unittype == UnitType.Box || unittype == UnitType.Player)
            return true;
        return false;
    }

    public override Unit Clone()
    {
        Player u = new Player();
        u.unitType = unitType;
        u.obj = obj;
        u.position = obj.transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;
        u.ability = ability;
        u.current_room = current_room;
        u.current_scene = current_scene;
        u.move_direction = move_direction;
        return u;
    }
}

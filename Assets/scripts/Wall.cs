using UnityEngine;
using System.Collections.Generic;

public class Wall : Unit
{

    public Direction direction;
    public bool magnetic;
    public List<Unit> connectedUnits;

    // Use this for initialization
    void Start()
    {
        unitType = UnitType.Wall;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = false;
        layer = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override bool CanMove(UnitType unittype)
    {

        return false;
    }

    public override Unit Clone()
    {
        Wall u = new Wall();
        u.unitType = UnitType.Block;
        u.obj = obj;
        u.position = obj.transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;
        u.direction = direction;
        u.magnetic = magnetic;
        return u;
    }
}

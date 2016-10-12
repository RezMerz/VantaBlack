using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingContainer : Container{

    public List<Direction> MoveDirections;
    public GameObject Unit;
    public int distance;

    void Start()
    {
        unitType = UnitType.Container;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
    }

    public override void Run()
    {
        Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), MoveDirections[state], distance);
    }

    public override void Check()
    {
        
    }

    
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingContainer : Container{

    public List<Direction> MoveDirections;
    public GameObject Unit;
    public int distance;
    int moved;
    public bool forward { get; set; }

    void Start()
    {
        base.Start();
        forward = false;
    }

    public override void Run()
    {
        if (forward)
        {
            moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), MoveDirections[state], distance);
        }
        else
            moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), Toolkit.ReverseDirection(MoveDirections[state]), distance);
    }
    public void Run(int i)
    {
        if (forward)
            moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), MoveDirections[state], i);
        else
            moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), Toolkit.ReverseDirection(MoveDirections[state]), i);
    }

    public override void Check()
    {
        
    }

    
}

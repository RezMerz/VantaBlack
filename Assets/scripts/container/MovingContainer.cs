using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingContainer : Container{

    public List<Direction> MoveDirections;
    public GameObject Unit;
    public int distance;

    public MovingContainer(int numberofStates)
    {
        this.numberofStates = numberofStates;
        MoveDirections = new List<Direction>();
    }

    public override void Run()
    {
        print("fuck");
        GraphicalEngine.MoveObject(Unit, MoveDirections[state], distance);
    }

    public override void Check()
    {
        
    }

    
}

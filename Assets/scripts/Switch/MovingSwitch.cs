using UnityEngine;
using System.Collections;

public class MovingSwitch : Switch {

    int moved;
    public int distance;
    
    public Direction directionOfMove;
    public GameObject unit;
    void Start()
    {
        base.Start();
        
        moved = distance;
    }

    public override bool Run()
    {
        if (singlestate && isOn)
            return false;
        if (isOn)
        {
            if (moved == distance)
            {
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), Toolkit.ReverseDirection(directionOfMove), distance);
            }
            else
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), Toolkit.ReverseDirection(directionOfMove), moved);

            isOn = false;
            return moved != 0;
        }
        else
        {
            if (moved == distance)
            {
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), directionOfMove, distance);
                print(moved);
            }
            else
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), directionOfMove, moved);

            isOn = true;
            return moved != 0;
        }
    }
}

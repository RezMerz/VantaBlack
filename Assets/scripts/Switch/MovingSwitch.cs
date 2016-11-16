using UnityEngine;
using System.Collections;

public class MovingSwitch : Switch {

    int moved;
    public int distance;
    
    public Direction directionOfMove;
    public GameObject unit;
    void Start()
    {
        unitType = UnitType.Switch;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = false;
        layer = 2;

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
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), directionOfMove, distance);

            isOn = true;
            return moved != 0;
        }
    }
}

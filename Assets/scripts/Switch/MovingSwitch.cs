using UnityEngine;
using System.Collections;

public class MovingSwitch : Switch {

    int moved;
    public int distance;
    bool isOn;
    
    public Direction directionOfMove;
    public GameObject unit;

    void Start()
    {
        unitType = UnitType.Switch;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        isOn = false;
        moved = distance;
    }

    public override bool Run()
    {
        if (singlestate && isOn)
            return false;
        if (isOn)
        {
            
            if (moved == distance)
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), Toolkit.ReverseDirection(directionOfMove), distance);
            else
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), Toolkit.ReverseDirection(directionOfMove), moved);

            isOn = false;
            return moved != 0;
        }
        else
        {
            if(moved == distance)
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), directionOfMove, distance);
            else
                moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(unit), directionOfMove, moved);

            isOn = true;
            return moved != 0;
        }
    }
}

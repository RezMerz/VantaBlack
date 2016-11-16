using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingContainer : MonoBehaviour{

    public List<Direction> MoveDirections;
    public GameObject Unit;
    public int distance;
    int moved;
    Container container;
    public List<AbilityType> activatorAbility;
    void Start()
    {
        moved = distance;
        container = gameObject.GetComponent<Container>();
    }

    public void Run()
    {
        foreach (AbilityType ability in activatorAbility)
        {
            if(container.ability == null)
            {
                if (container.forward)
                {
                    if (moved == distance)
                        moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), MoveDirections[container.state], distance);
                    else
                        moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), MoveDirections[container.state], moved);
                }
                else
                {
                    if (moved == distance)
                        moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), Toolkit.ReverseDirection(MoveDirections[container.state]), distance);
                    else
                        moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), Toolkit.ReverseDirection(MoveDirections[container.state]), moved);
                }
                return;
            }
            if ((container._lastAbility != null && ability == container._lastAbility.abilitytype) || (container.ability != null && ability == container.ability.abilitytype))
            {
                if (container.forward)
                {
                    if (moved == distance)
                        moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), MoveDirections[container.state], distance);
                    else
                        moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), MoveDirections[container.state], moved);
                }
                else
                {
                    if (moved == distance)
                        moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), Toolkit.ReverseDirection(MoveDirections[container.state]), distance);
                    else
                        moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), Toolkit.ReverseDirection(MoveDirections[container.state]), moved);
                }
                return;
            }
        }
    }
    public void Run(int i)
    {
        if (container.forward)
            moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), MoveDirections[container.state], i);
        else
            moved = Interface.GetEngine().MoveObjects(Interface.GetEngine().GetUnit(Unit), Toolkit.ReverseDirection(MoveDirections[container.state]), i);
    }
}

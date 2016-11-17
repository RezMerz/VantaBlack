using UnityEngine;
using System.Collections.Generic;

public class DoorOpener : MonoBehaviour {
    public GameObject door;
    public List<AbilityType> activatorAbility;

    private Container container;

    // Use this for initialization
    void Start()
    {
        container = gameObject.GetComponent<Container>();

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Run()
    {
        if (container.ability == null)
        {
            foreach (AbilityType ability in activatorAbility)
            {
                if (ability == container._lastAbility.abilitytype)
                {
                    door.GetComponent<Door>().OpenClose();
                }
            }
            return;
        }
        foreach (AbilityType ability in activatorAbility)
        {
            if (ability == container.ability.abilitytype)
            {
                door.GetComponent<Door>().OpenClose();
            }
        }
    }

    public DoorOpener Clone(Container con)
    {
        DoorOpener u = new DoorOpener();
        u.door = door;
        u.activatorAbility = activatorAbility;]
        u.container = con;
        return u;
    }
}

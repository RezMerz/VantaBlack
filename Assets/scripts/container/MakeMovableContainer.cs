using UnityEngine;
using System.Collections.Generic;

public class MakeMovableContainer : MonoBehaviour {

    public Container container;
    public bool isReverse;
    public List<AbilityType> activatorAbility;
    // Use this for initialization
    void Start () {
        
        container = gameObject.GetComponent<Container>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Run()
    {
        if (isReverse)
        {
            if (container.IsEmpty())
            {
                foreach (AbilityType ability in activatorAbility)
                {
                    if (ability == container._lastAbility.abilitytype)
                    {
                        container.CanBeMoved = true;
                    }
                }
            }
            else
            {

                foreach (AbilityType ability in activatorAbility)
                {
                    if (ability == container.ability.abilitytype)
                    {
                        container.CanBeMoved = false;
                    }
                }
            }
        }
        else
        {
            if (container.IsEmpty())
            {
                foreach (AbilityType ability in activatorAbility)
                {
                    if (ability == container._lastAbility.abilitytype)
                    {
                        container.CanBeMoved = false;
                    }
                }
            }
            else
            {
                foreach (AbilityType ability in activatorAbility)
                {
                    if (ability == container.ability.abilitytype)
                    {
                        container.CanBeMoved = true;
                    }
                }
            }
        }
    }
}

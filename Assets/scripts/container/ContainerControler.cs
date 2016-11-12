using UnityEngine;
using System.Collections.Generic;

public class ContainerControler : MonoBehaviour{

    public Container othercontainer;

    public List<AbilityType> activatorAbility;
    private Container container;
    // Use this for initialization
    void Start () {
        container = gameObject.GetComponent<Container>();
    }
	
	// Update is called once per frame
	public void Run()
    {
        if(container.ability == null)
        {
            foreach (AbilityType ability in activatorAbility)
            {
                if (ability == container._lastAbility.abilitytype)
                {
                    othercontainer.Run();
                }
            }
            return;
        }
        foreach (AbilityType ability in activatorAbility)
        {
            if (ability == container.ability.abilitytype)
            {
                othercontainer.Run();
            }
        }
    }
}

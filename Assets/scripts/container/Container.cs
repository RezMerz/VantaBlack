using UnityEngine;
using System.Collections;

public class Container : Unit{
    public int numberofStates, state;
    public Ability ability;
	// Use this for initialization
	
    void Start()
    {
        unitType = UnitType.Container;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
    }

    public virtual void Run() { }

    public virtual void Check() { }

    public bool IsAvailable()
    {
        if (state == numberofStates - 1)
            return false;

        return true;
    }

    public bool IsEmpty()
    {
        if (ability == null)
            return true;
        return false;
    }
}

using UnityEngine;
using System.Collections;

public class SwitchMakeMovable : Switch {
	
	public Unit unit;

    // Use this for initialization
    void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public override bool Run()
    {
        unit.CanBeMoved = true;
        return true;
    }
	
	
}

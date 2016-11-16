using UnityEngine;
using System.Collections;

public class SwitchEnabler : Switch {

    public Switch otherswitch;

	// Use this for initialization
	void Start () {
        unitType = UnitType.Switch;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = false;
        layer = 2;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override bool Run()
    {
        otherswitch.disabled = false;
        return true;
    }
}

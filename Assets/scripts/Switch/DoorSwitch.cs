using UnityEngine;
using System.Collections;

public class DoorSwitch : Switch {

    public GameObject door;
    bool isOn;
    // Use this for initialization
    void Start () {
        base.Start();
        isOn = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override bool Run() {
        if (isOn && singlestate)
            return false;
        Door d = Toolkit.GetDoor(door);
        if (d != null)
        {
            d.OpenClose();
            isOn = true;
            return true;
        }
        return false;
    }

}

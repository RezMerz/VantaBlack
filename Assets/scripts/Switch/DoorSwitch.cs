using UnityEngine;
using System.Collections;

public class DoorSwitch : Switch {

    public GameObject door;
    bool isOn;
    // Use this for initialization
    void Start () {
        unitType = UnitType.Switch;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        isOn = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override bool Run() {
        if (isOn && singlestate)
            return false;
        door.GetComponent<Door>().OpenClose();
        isOn = true;
        return true;
    }

}

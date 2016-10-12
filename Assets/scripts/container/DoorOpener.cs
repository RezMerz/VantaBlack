using UnityEngine;
using System.Collections;

public class DoorOpener : Container {
    public GameObject door;

    // Use this for initialization
    void Start()
    {
        unitType = UnitType.Container;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
    }

    // Update is called once per frame
    void Update () {
	
	}

    public override void Run()
    {
        door.GetComponent<Door>().OpenClose();
    }
}

using UnityEngine;
using System.Collections;

public class DoorOpener : Container {
    public GameObject door;

    // Use this for initialization
    void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update () {
	
	}

    public override void Run()
    {
        door.GetComponent<Door>().OpenClose();
    }
}

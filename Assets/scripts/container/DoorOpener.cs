using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {
    public GameObject door;

    // Use this for initialization
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Run()
    {
        door.GetComponent<Door>().OpenClose();
    }
}

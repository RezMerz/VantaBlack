using UnityEngine;
using System.Collections;

public class MakeMovableContainer : MonoBehaviour {

    public Container container;
    public bool isReverse;
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
                container.CanBeMoved = true;
            else
                container.CanBeMoved = false;
        }
        else
        {
            if (container.IsEmpty())
                container.CanBeMoved = false;
            else
                container.CanBeMoved = true;
        }
    }
}

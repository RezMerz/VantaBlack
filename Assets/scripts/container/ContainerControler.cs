using UnityEngine;
using System.Collections;

public class ContainerControler : MonoBehaviour{

    public Container othercontainer;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	public void Run()
    {
        othercontainer.Run();
    }
}

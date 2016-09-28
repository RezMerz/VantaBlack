using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public Ability ability;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void undo(Ability ability)
    {
        this.ability = ability;
    }
}

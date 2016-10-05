using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Unit {

    public Ability ability;
    public List<Direction> move_direction;

	// Use this for initialization
	void Start () {
        position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
	} 
}

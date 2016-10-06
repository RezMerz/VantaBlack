using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Unit {

    public Ability ability;
    public List<Direction> move_direction;

	// Use this for initialization
	void Start () {
        unitType = UnitType.Player;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
	}
	
	// Update is called once per frame
	void Update () {
        
	} 
}

﻿using UnityEngine;
using System.Collections;

public class Wall : Unit {

    public Direction direction;
    public bool magnetic;

	// Use this for initialization
	void Start () {
        unitType = UnitType.Wall;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

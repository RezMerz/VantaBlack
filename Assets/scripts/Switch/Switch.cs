using UnityEngine;
using System.Collections;

public class Switch : Unit {

    public Direction direction;
    public bool singlestate;
    // Use this for initialization
    public void Start () {
        unitType = UnitType.Switch;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = false;
        layer = 2;
	}
    
	// Update is called once per frame
	void Update () {
	
	}
    public override bool CanMove(UnitType unittype)
    {
       
        return false;
    }
    public virtual bool Run()
    {
        return false;
    }
}

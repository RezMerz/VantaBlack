using UnityEngine;
using System.Collections.Generic;

public class Switch : Unit
{

    public Direction direction { get; set; }
    public bool singlestate { get; set; }
    public bool isOn { get; set; }

    public bool isAutomatic { get; set; }

    public bool disabled;

    // Use this for initialization
    public void Start()
    {
        unitType = UnitType.Switch;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        movable = false;
        layer = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init(SwitchConfig sc)
    {
        direction = sc.direction;
        singlestate = sc.singlestate;
        isOn = sc.isOn;
        isAutomatic = sc.isAutomatic;
        disabled = sc.disabled;
    }

    public override bool CanMove(UnitType unittype)
    {

        return false;
    }
    public virtual bool Run()
    {
        return false;
    }

    public override Unit Clone()
    {
        Switch u = new Switch();
        u.unitType = UnitType.Block;
        u.obj = obj;
        u.position = transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;

        return u;
    }
}
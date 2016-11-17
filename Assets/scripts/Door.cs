using UnityEngine;
using System.Collections;

public class Door : Unit
{


    public bool open;
    public Direction direction;
    protected Sprite sprite;
    // Use this for initialization
    void Start()
    {
        unitType = UnitType.Door;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        try
        {
            sprite = obj.GetComponent<SpriteRenderer>().sprite;
        }
        catch
        {
            sprite = null;
        }
        movable = false;
        layer = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Next()
    {

    }

    public virtual void OpenDoor()
    {

    }

    public virtual void CloseDoor()
    {

    }

    public virtual void OpenClose()
    {

    }
    public override bool CanMove(UnitType unittype)
    {

        return false;
    }

    public override Unit Clone()
    {
        Door u = new Door();
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

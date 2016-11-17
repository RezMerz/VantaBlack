using UnityEngine;
using System.Collections;

public class InternalDoor : Door
{

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
        if (open)
            obj.GetComponent<SpriteRenderer>().sprite = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Next()
    {

    }

    public override void OpenDoor()
    {
        open = true;
        obj.GetComponent<SpriteRenderer>().sprite = null;
    }

    public override void CloseDoor()
    {
        open = false;
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public override void OpenClose()
    {
        if (open)
            CloseDoor();
        else
            OpenDoor();
    }
    public override bool CanMove(UnitType unittype)
    {

        return false;
    }
    public override Unit Clone()
    {
        Wall u = new Wall();
        u.unitType = UnitType.Block;
        u.obj = obj;
        u.position = obj.transform.position;
        u.movable = movable;
        u.codeNumber = codeNumber;
        u.CanBeMoved = CanBeMoved;
        u.layer = layer;

        return u;
    }
}

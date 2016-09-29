using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Database : MonoBehaviour{
    public static Database database = new Database();
    private Database()
    {
        snapshots = new List<Snapshot>(numberOfSnapshot);
    }
    /// ///////////////////////
    public GameObject player;
    public readonly int numberOfSnapshot = 5;
    public Direction gravity_direction;
    public List<Unit>[,] units;

    private int Xsize, Ysize;
    private Direction GravityDirection;
    private List<Snapshot> snapshots;



    public void Setsize(int x, int y)
    {
        Xsize = x;
        Ysize = y;
    }

    public void takesnapshot()
    {
        //snapshots.Add(new Snapshot(units.Clone(), player.));
        
    }
}

public class Unit
{
    public Vector2 position;
    public UnitType type;
    public GameObject obj;
    public Component component;

    public Unit(UnitType type, GameObject obj, Component component)
    {
        this.type = type;
        this.obj = obj;
        this.position = obj.transform.position;
        this.component = component;
    }
}



public enum UnitType
{
    Block, Pipe, Box, Magnet, Switch
}

public enum State
{
    Idle, Busy
}

public enum Direction
{
    Right, Left, Up, Down
}

public enum AbilityType
{
    Direction, Jump, Gravity, Blink, Rope
}

public class Snapshot
{
    List<Unit> units;

    public Snapshot(List<Unit> units, Player player)
    {

        this.units = units;
        //this.player = player;
    }
}   
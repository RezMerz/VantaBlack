using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Database {
    public static Database database = new Database(0);
    private Database(long i)
    {
        snapshots = new List<Snapshot>(numberOfSnapshot);
        turn = i;
    }
    /// ///////////////////////
    public GameObject player;
    public readonly int numberOfSnapshot = 5;
    public int snapShotCount;
    public Direction gravity_direction;
    public List<Unit>[,] units;
    public List<Switch> AutomaticSwitches;
    public long turn;
    public List<TimeLaps> timeLaps;
    public State state;
    public int[,] checkPointPositions;
    public int Ysize { get; private set; }
    public int Xsize { get; private set; }

    private Direction GravityDirection;
    
    public List<Snapshot> snapshots;

    public bool ACTIVE_RED, ACTIVE_BLUE, ACTIVE_GREEN, ACTIVE_PURPLE, ACTIVE_LIGHTBLUE;


    public void Setsize(int x, int y)
    {
        Xsize = x;
        Ysize = y;
    }

    
}





public class TimeLaps
{
    public long lifetime;
    public Vector2 position;
    public long time;
    public GameObject gameobject;
    public TimeLaps(int lifetime, GameObject gameobject)
    {
        this.lifetime = lifetime + Database.database.turn;
        this.gameobject = gameobject;
        position = gameobject.transform.position;
        time = Database.database.turn;
    }
}

public class Pipe : Unit
{
    public GameObject source, sink;
    bool isOpen;

    void Start()
    {
        isOpen = true;
        movable = false;
        layer = 2;
    }
    public override bool CanMove(UnitType unittype)
    {

        return false;
    }

    public override Unit Clone()
    {
        Pipe u = new Pipe();
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

public enum UnitType
{
    Block, Pipe, Box, Magnet, Switch, Wall, Container, Player, Rock, Door, BlockSwitch
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
    Direction, Jump, Gravity, Blink, Rope, Fuel
}



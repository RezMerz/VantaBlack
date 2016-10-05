using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Database{
    public static Database database = new Database(0);
    private Database(long i)
    {
        snapshots = new List<Snapshot>(numberOfSnapshot);
        turn = i;
    }
    /// ///////////////////////
    public GameObject player;
    public readonly int numberOfSnapshot = 5;
    public Direction gravity_direction;
    public List<Unit>[,] units;
    public long turn;
    public List<TimeLaps> timeLaps;
    public State state;

    private int Xsize, Ysize;
    private Direction GravityDirection;
    public List<Snapshot> snapshots;



    public void Setsize(int x, int y)
    {
        Xsize = x;
        Ysize = y;
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
public enum UnitType
{
    Block, Pipe, Box, Magnet, Switch, Wall, Container, Player
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



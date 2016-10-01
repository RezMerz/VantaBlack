using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LogicalEngine {
    public Database database;
    public Player player;
    public GraphicalEngine Gengine;
    public Move moveObject;
    int x, y;
    Action action;
    AandR AR;
    Map map;
    
    SnapshotManager spManager;
    public LogicalEngine (int x, int y) {
        database = Database.database;
        player = database.player.GetComponent<Player>();
        Gengine = new GraphicalEngine();
        spManager = new SnapshotManager();
        database.units = new List<Unit>[x, y];
        database.timeLaps = new List<TimeLaps>();
        this.x = x;
        this.y = y;
        init();
        moveObject = new Move(this);
        action = new Action(this);
        map = new Map(this);
        AR = new AandR(this);
	}
    void init()
    {
        for(int i=0; i< x; i++)
        {
            for(int j=0; j< y; j++)
            {
                database.units[i, j] = new List<Unit>();
            }
        }

        List<GameObject> Gobjects = new List<GameObject>();
        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Wall"));
        foreach (GameObject g in Gobjects)
        {
            Wall[] wall = g.GetComponents<Wall>();
            if(wall[0].direction == Direction.Right)
            {
                database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(new Unit(UnitType.Wall, g, wall[0]));
                database.units[(int)g.transform.position.x + 1, (int)g.transform.position.y].Add(new Unit(UnitType.Wall, g, wall[1]));
            }
            else
            {
                database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(new Unit(UnitType.Wall, g, wall[0]));
                database.units[(int)g.transform.position.x, (int)g.transform.position.y + 1].Add(new Unit(UnitType.Wall, g, wall[1]));

            }
            
        }
        Gobjects.Clear();


        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Block"));
        foreach(GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(new Unit(UnitType.Block, g, g.GetComponent<Block>()));
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Pipe"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(new Unit(UnitType.Block, g, null));
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Container"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(new Unit(UnitType.Block, g, g.GetComponent<Container>()));
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Switch"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(new Unit(UnitType.Block, g, g.GetComponent<Switch>()));
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(new Unit(UnitType.Player, g, g.GetComponent<Player>()));
        Gobjects.Clear();
    }


    public void run()
    {
        CheckTimeLaps();
        database.state = State.Idle;
    }

    /// <summary>
    /// action for dir, jump, rope
    /// </summary>
    /// /// <summary>
    public void Act()
    {
        action.Act();
    }  
    /// action for blink, gravity
    /// </summary>
    /// <param name="direction"></param>
    public void Act(Direction dir)
    {
        action.Act(dir);
    }

    public void move(Direction direction)
    {
        moveObject.move(direction);
    }
    public void Absorb()
    {
        AR.Absorb();
    }

    public void Absorb(Direction direcion)
    {
        AR.Absorb(direcion);
    }

    public void NextTurn()
    {
        spManager.takesnapshot();
        
        database.turn++;
    }

    public void Undo()
    {
        database.state = State.Busy;
        Snapshot snapshot = spManager.Revese();
        database.units = snapshot.units;
        database.turn = snapshot.turn;
        Refresh();
        
    }

    public void Refresh()
    {
        Gengine.Refresh();
        database.state = State.Idle;
    }
    private void CheckTimeLaps()
    {
        foreach(TimeLaps t in database.timeLaps)
        {
            t.time++;
            if (t.time == t.lifetime)
            {
                database.timeLaps.Remove(t);
                action.Teleport(t.position);
            }
        }
    }
}



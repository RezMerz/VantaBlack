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
                database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponents<Wall>()[0]);
                database.units[(int)g.transform.position.x + 1, (int)g.transform.position.y].Add(g.GetComponents<Wall>()[1]);
            }
            else
            {
                database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponents<Wall>()[0]);
                database.units[(int)g.transform.position.x, (int)g.transform.position.y + 1].Add(g.GetComponents<Wall>()[1]);

            }

        }
        Gobjects.Clear();


        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Block"));
        foreach (GameObject g in Gobjects)
        {
            Block temp = g.GetComponent<Block>();
            temp.unitType = UnitType.Block;
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(temp);
        }
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Pipe"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<Pipe>());
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Container"));
        foreach (GameObject g in Gobjects)
        {
            //Wall.print(g.transform.position);
            //Wall.print(g.GetComponent<MovingContainer>());
            Container temp = g.GetComponent<MovingContainer>();
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<MovingContainer>());
        }
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Switch"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<Switch>());
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<Player>());
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Rock"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<Rock>());
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Door"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<Rock>());
        Gobjects.Clear();

        /*for(int i=0; i< x; i++)
        {
            for(int j=0; j< y; j++)
            {
                Wall.print(database.units[i,j].Count);
            }
            Wall.print(" ");
        }*/
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
        bool flag = false;
        foreach(Direction d in player.move_direction)
        {
            if(d == direction)
            {
                flag = true;
                break;  
            }
        }
        if(flag)
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

    public void MoveObjects(Unit unit, Direction d, int distance)
    {
        moveObject.MoveObjects(unit, d, distance);
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

    public Unit GetUnit(GameObject gameobject)
    {   
        for (int i=0; i<x; i++)
        {
            for(int j=0; j<y; j++)
            {
                foreach(Unit u in database.units[i, j])
                {
                    if (u.gameObject == gameobject)
                        return u;
                }
            }
        }

        return null;
    }
}



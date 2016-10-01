using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LogicalEngine {
    Database database;
    Player player;
    GraphicalEngine Gengine;
    int x, y;
    Action action;
    AandR AR;
    Map map;
    Move moveObject;
    public LogicalEngine (int x, int y) {
        database = Database.database;
        player = database.player.GetComponent<Player>();
        Gengine = new GraphicalEngine();
        database.units = new List<Unit>[x, y];
        this.x = x;
        this.y = y;
        init();
        moveObject = new Move(Gengine, player, database);
        action = new Action(player, database, Gengine, moveObject);
        map = new Map(player);
        AR = new AandR(Gengine, player, database);
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
    /// <summary>
    /// absorb from other sides (including gravity side):
    /// 1. logic
    /// </summary>
    /// <param name="direction"></param>
    public void Absorb(Direction dir)
    {
        AR.Absorb(dir);
    }
    /// <summary>
    /// simple absorb
    /// </summary>
    public void Absorb()
    {
        AR.Absorb();
    }
    /// <summary>
    /// V Move
    /// </summary>
    /// <param name="direction"></param>
    public void move(Direction dir)
    {
        moveObject.move(dir);
    }
    /// <summary>
    /// jump
    /// </summary>
    public void jump()
    {
        moveObject.jump();

    }

    public void Blink(Direction dir)
    {
        action.Blink(dir);
    }

    public void Teleport(Vector2 position)
    {
        action.Teleport(position);
    }
    /// <summary>
    /// Drains the player:
    /// 1. logical
    /// </summary>
    public void Drain()
    {
        AR.Drain();
    }

    public void Fountain(int num)
    {
        map.Fountain(num);
    }
}



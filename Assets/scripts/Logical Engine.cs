using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LogicalEngine
{
    public Database database;
    public Player player;
    public GraphicalEngine Gengine;
    public Move moveObject;
    public AnimationControler animationControler;
    int x, y;
    public Action action;
    AandR AR;
    Map map;

    SnapshotManager spManager;

    public List<Unit> reserved;
    public LogicalEngine(int x, int y)
    {
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

        reserved = new List<Unit>();
    }
    void init()
    {
        database.AutomaticSwitches = new List<Switch>();
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                database.units[i, j] = new List<Unit>();
            }
        }

        List<GameObject> Gobjects = new List<GameObject>();
        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Wall"));

        foreach (GameObject g in Gobjects)
        {
            Wall[] wall = g.GetComponents<Wall>();

            if (wall[0].direction == Direction.Right)
            {
                database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(wall[0]);
                database.units[(int)g.transform.position.x + 1, (int)g.transform.position.y].Add(wall[1]);

            }
            else
            {
                database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(wall[0]);
                database.units[(int)g.transform.position.x, (int)g.transform.position.y + 1].Add(wall[1]);

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
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<Container>());
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Switch"));
        foreach (GameObject g in Gobjects)
        {
            SwitchConfig sc = g.GetComponent<SwitchConfig>();
            MovingSwitch[] t1 = g.GetComponents<MovingSwitch>();
            DoorSwitch[] t2 = g.GetComponents<DoorSwitch>();
            SwitchControler[] t3 = g.GetComponents<SwitchControler>();
            for(int i=0; i<t1.Length; i++)
            {
                t1[i].init(sc);
            }
            for(int i=0; i<t2.Length; i++)
            {
                t2[i].init(sc);
            }
            for (int i = 0; i < t3.Length; i++)
            {
                t3[i].init(sc);
            }
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].AddRange(t2);
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].AddRange(t1);
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].AddRange(t3);
            if (sc.isAutomatic)
            {
                database.AutomaticSwitches.AddRange(t1);
                database.AutomaticSwitches.AddRange(t2);
                database.AutomaticSwitches.AddRange(t3);
            }
        }
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
        {
            InternalDoor d1 = g.GetComponent<InternalDoor>();
            if (d1 != null)
            {
                database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(d1);

            }
            else
            {
                ExternalDoor d2 = g.GetComponent<ExternalDoor>();
                if (d2 != null)
                {
                    if (d2.direction == Direction.Down)
                    {
                        database.units[(int)g.transform.position.x, (int)g.transform.position.y + 1].Add(d2);
                    }
                    else if (d2.direction == Direction.Left)
                        database.units[(int)g.transform.position.x + 1, (int)g.transform.position.y].Add(d2);
                    else
                        database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(d2);
                }

            }
        }
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Box"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<Box>());
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("BlockSwitch"));
        foreach (GameObject g in Gobjects)
            database.units[(int)g.transform.position.x, (int)g.transform.position.y].Add(g.GetComponent<BlockSwitch>());
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
        foreach (Direction d in player.move_direction)
        {
            if (d == direction)
            {
                flag = true;
                break;
            }
        }
        if (flag)
        {
            moveObject.move(direction);
            ApplyGravity();
            CheckPointCheck();
        }
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
        /*spManager.takesnapshot();
        for(int i=0; i < database.snapSshots[database.snapShotCount-1].units.GetLength(0); i++)
        {
            for(int j=0; j< database.snapshots[database.snapShotCount - 1].units.GetLength(1); j++)
            {
                for(int k=0; k< database.snapshots[database.snapShotCount - 1].units[i,j].Count; k++)
                {
                    if(database.snapshots[database.snapShotCount - 1].units[i,j][k].unitType == UnitType.Player)
                        Wall.print(database.snapshots[database.snapShotCount - 1].units[i, j][k].transform.position);
                }
            }
        }*/
        database.turn++;
    }

    public void Undo()
    {
        Wall.print("undoing");
        database.state = State.Busy;
        Snapshot snapshot = spManager.Revese();
        for (int i = 0; i < database.snapshots[database.snapShotCount - 1].units.GetLength(0); i++)
        {
            for (int j = 0; j < database.snapshots[database.snapShotCount - 1].units.GetLength(1); j++)
            {
                database.units[i, j] = new List<Unit>();
                //for(int k=0; k< database.snapshots[database.snapShotCount - 1].units[i,j].Count; i++)
                //{
                    database.units[i, j].AddRange(database.snapshots[database.snapShotCount - 1].units[i, j]);
                //}
            }
        }
        for (int i = 0; i < database.snapshots[database.snapShotCount - 1].units.GetLength(0); i++)
        {
            for (int j = 0; j < database.snapshots[database.snapShotCount - 1].units.GetLength(1); j++)
            {
                for (int k = 0; k < database.snapshots[database.snapShotCount - 1].units[i, j].Count; k++)
                {
                    if (database.snapshots[database.snapShotCount - 1].units[i, j][k].unitType == UnitType.Player)
                        Wall.print(database.snapshots[database.snapShotCount - 1].units[i, j][k].transform.position);
                }
            }
        }
        //database.units = snapshot.units;
        database.turn = snapshot.turn;
        //Refresh();
    }

    public void SwitchAction()
    {
       
        action.SwitchActionPressed();
    }

    public void SwitchAction(Direction d)
    {
        action.SwitchActionPressed(d);
    }

    public void Refresh()
    {
        Gengine.Refresh();
        database.state = State.Idle;
    }

    public int MoveObjects(Unit unit, Direction d, int distance)
    {
        int i = moveObject.MoveObjects(unit, d, distance);
        if (unit.unitType == UnitType.Player && i != 0)
        {

            moveObject.MoveObjects(unit, d, distance);
        }
        return i;
    }

    private void CheckTimeLaps()
    {
        foreach (TimeLaps t in database.timeLaps)
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
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                foreach (Unit u in database.units[i, j])
                {
                    if (u.gameObject == gameobject)
                        return u;
                }
            }
        }

        return null;
    }

    public void EndTurn()
    {
        //CheckAutomaticSwitch();
        //ApplyGravity();
        CheckBlockSwitch();
        



        //spManager.takesnapshot();
        //Wall.print(database.snapShotCount);
    }

    public void ApplyGravity()
    {
        //ApplyGravity(player);
        //return;
        for(int i=0; i<database.units.GetLength(0); i++)
        {
            for(int j=0; j<database.units.GetLength(1); j++)
            {
                for(int k=0; k<database.units[i,j].Count; k++)
                {
                    Unit u = database.units[i,j][k];
                    if (u.CanBeMoved)
                    {
                        ApplyGravity(u);
                    }
                }
            }
        }
    }

    private void ApplyGravity(Unit unit)
    {
        int counter = 0;
        Vector2 pos1 = unit.transform.position;
        Vector2 pos2;
        switch (database.gravity_direction)
        {
            case Direction.Up: pos2 = new Vector2(0, 1); break;
            case Direction.Down: pos2 = new Vector2(0, -1); break;
            case Direction.Right: pos2 = new Vector2(1, 0); break;
            case Direction.Left: pos2 = new Vector2(-1, 0); break;
            default: pos2 = new Vector2(0, 0); break;
        }
        for (int i = 0; i < database.units[(int)unit.transform.position.x, (int)unit.transform.position.y].Count; i++)
        {
            Unit u = database.units[(int)unit.transform.position.x, (int)unit.transform.position.y][i];
            if (u.unitType == UnitType.Door)
            {
                if (u.unitType == UnitType.Player && ((Door)u).direction == database.gravity_direction && ((Door)u).open)
                    ((Door)u).Next();
                else if (((Door)u).direction == database.gravity_direction && !((Door)u).open)
                    return;
            }
        }
        while (true)
        {
            if (Toolkit.IsEmptySpace(Toolkit.VectorSum(pos1, pos2), database.gravity_direction))
            {
                counter++;
                pos1 = Toolkit.VectorSum(pos1, pos2);
            }
            else
                break;
        }
        database.units[(int)unit.transform.position.x, (int)unit.transform.position.y].Remove(unit);
        for (int i = 0; i < counter; i++)
            Gengine._Move_Object(unit.obj, Toolkit.VectorSum(unit.transform.position, Toolkit.DirectiontoVector(database.gravity_direction)));
        unit.position = unit.transform.position;
        database.units[(int)unit.transform.position.x, (int)unit.transform.position.y].Add(unit);
    }
    public void CheckBlockSwitch()
    {
        try
        {
            Vector2 temp = Toolkit.DirectiontoVector(database.gravity_direction);
            foreach (Unit u in database.units[(int)Toolkit.VectorSum(temp, player.transform.position).x, (int)Toolkit.VectorSum(temp, player.transform.position).y])
            {
                if (u.unitType == UnitType.BlockSwitch && !((BlockSwitch)u).isManual)
                {
                    action.BlockSwitchAction(((BlockSwitch)u));
                }
            }
        }
        catch { }
    }
    public void CheckAutomaticSwitch()
    {
        Wall.print("s300");
        for(int i=0; i<database.AutomaticSwitches.Count; i++)
        {
            Vector2 temp;
            bool tempbool = true;
            for(int j=0; j<database.units[(int)database.AutomaticSwitches[i].transform.position.x, (int)database.AutomaticSwitches[i].transform.position.y].Count; j++)
            {
                temp = database.AutomaticSwitches[i].transform.position;
                if (database.units[(int)temp.x, (int)temp.y][j].unitType == UnitType.Box || database.units[(int)temp.x, (int)temp.y][j].unitType == UnitType.Block || database.units[(int)temp.x, (int)temp.y][j].unitType == UnitType.BlockSwitch || database.units[(int)temp.x, (int)temp.y][j].unitType == UnitType.Container || database.units[(int)temp.x, (int)temp.y][j].unitType == UnitType.Player || database.units[(int)temp.x, (int)temp.y][j].unitType == UnitType.Rock)
                {
                    if (!database.AutomaticSwitches[i].isOn)
                    {
                        database.AutomaticSwitches[i].Run();
                        tempbool = false;
                        break;
                    }
                }
            }
            if (tempbool && database.AutomaticSwitches[i].isOn)
            {
                Wall.print("s7");
                database.AutomaticSwitches[i].Run();
            }
        }

    }
    public void CheckPointCheck()
    {
        return;
        for (int i = 0; i < database.checkPointPositions.Length; i++)
        {
            if (database.checkPointPositions[i, 0] == (int)player.transform.position.x && database.checkPointPositions[i, 1] == (int)player.transform.position.y) {

            }
        }
    }
}


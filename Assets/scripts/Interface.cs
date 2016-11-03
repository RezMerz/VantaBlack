using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
    public GameObject player;
    private LogicalEngine engine;
    private static LogicalEngine staticengine;
    public int x, y;
    Database database;
    public Direction Gravity_Directin;
    private float rotate;
    private Direction lean;
	// Use this for initialization
	void Start () {
        Database.database.player = player;
        database = Database.database;
        database.gravity_direction = Gravity_Directin;
        database.state = State.Busy;
        engine = new LogicalEngine(x, y);
        staticengine = engine;
        engine.run();
    }
	
	// Update is called once per frame
	void Update () {
        //engine.Gengine._gravity();
        if (database.state == State.Idle)
        {
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (isEmpty(new Vector2(1, 0)))
                    {
                        engine.Gengine._right_light();
                        engine.move(Direction.Right);
                        engine.ApplyGravity();
                    }
                    else
                    {
                        engine.Gengine._lean_right();
                        rotate = -10;
                        lean = Direction.Right;
                    }

                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                  
                    if (isEmpty(new Vector2(-1, 0)))
                    {
                        engine.move(Direction.Left);
                        engine.ApplyGravity();
                    }
                    else
                    {
                        engine.Gengine._lean_left();
                        rotate = 10;
                        lean = Direction.Left;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (isEmpty(new Vector2(0, -1)))
                    {
                        engine.move(Direction.Down);
                        engine.ApplyGravity();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    engine.Gengine._lean_top();
                    lean = Direction.Up;
                    rotate = 10;
                   /* if (isEmpty(new Vector2(0, 1)))
                    {
                        engine.move(Direction.Up);
                        engine.ApplyGravity();
                    } */
                }

                else if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    if (rotate != 0)
                    {
                        engine.Gengine._lean_top_undo();
                        rotate = 0;
                    }
                }
                /// if released it should undo the lean
                else if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    if (rotate != 0)
                    {
                        engine.Gengine._lean_right_undo();
                        rotate = 0;
                    }
                }

                else if (Input.GetKeyUp(KeyCode.LeftArrow))
                    if (rotate != 0)
                    {
                        engine.Gengine._lean_left_undo();
                        rotate = 0;
                    }
            }
         
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    print("right space");
                    engine.Act(Direction.Right);
                    engine.ApplyGravity();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    engine.Act(Direction.Left);
                    engine.ApplyGravity();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    engine.Act(Direction.Down);
                    engine.ApplyGravity();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    engine.Act(Direction.Up);
                    engine.ApplyGravity();
                }
                else
                {
                    engine.Act();
                    engine.ApplyGravity();
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if(!_lean_absorb())
                    engine.Absorb();
                engine.ApplyGravity();
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                engine.Undo();
                engine.ApplyGravity();
            }
            else if (Input.GetKeyUp(KeyCode.I))
            {
                engine.Absorb(Direction.Up);
                engine.ApplyGravity();
            }
            else if (Input.GetKeyUp(KeyCode.J))
            {
                engine.Absorb(Direction.Left);
                engine.ApplyGravity();
            }
            else if (Input.GetKeyUp(KeyCode.K))
            {
                engine.Absorb(Direction.Down);
                engine.ApplyGravity();
            }
            else if (Input.GetKeyUp(KeyCode.L))
            {
                engine.Absorb(Direction.Right);
                engine.ApplyGravity();
            }
            else if ((Input.GetKeyUp(KeyCode.Q)))
            {
                engine.SwitchAction();
                engine.ApplyGravity();
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                GameObject.Find("Map").GetComponent<MapController>()._click();
            }
        }
    }

    private bool _lean_absorb()
    {
        if(rotate!=0)
        {
            engine.Absorb(lean);
            return true;
        }
        return false;
    }
    private bool isEmpty(Vector2 dir)
    {
        try {
            if (! CheckDoor(dir))
                return false;
            int x1 = (int)Mathf.Ceil(player.transform.position.x) + (int)dir.x;
            int y1 = (int)Mathf.Ceil(player.transform.position.y) + (int)dir.y;
            if(Toolkit.IsWallOnTheWay(player.transform.position, Toolkit.VectorToDirection(dir)))
            {
                return false;
            }
            foreach (Unit unit in Database.database.units[x1, y1])
            {
                if (unit.unitType == UnitType.Block || unit.unitType == UnitType.Container || unit.unitType == UnitType.Rock)
                {
                    return false;
                }
                if(unit.unitType == UnitType.Box)
                {
                    if (engine.MoveObjects(unit, Toolkit.VectorToDirection(dir), 1) == 1)
                            return true;
                }
                
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool CheckDoor(Vector2 dir)
    {

        int x1 = (int)player.transform.position.x;
        int y1 = (int)player.transform.position.y;
        foreach(Unit unit in database.units[x1, y1])
        {
            if(unit.unitType == UnitType.Door)
            {
                InternalDoor d1 = unit.GetComponent<InternalDoor>();
                if(d1 != null)
                {
                    return true;
                    if (d1.open && d1.direction == Toolkit.VectorToDirection(dir))
                        return true;
                    else
                        return false;
                }
                ExternalDoor d2 = unit.GetComponent<ExternalDoor>();
                if (d2 != null)
                {
                    if (d2.open && d2.direction == Toolkit.VectorToDirection(dir))
                    {
                        unit.obj.GetComponent<Door>().Next();
                    }
                    else return false;
                }
            }
        }
        return true;
    }
    public bool WallDetector(Unit unit, Direction dir)
    {
        switch (dir)
        {
            case Direction.Right: return !(((Wall)unit).direction == Direction.Left);
            case Direction.Left: return !(((Wall)unit).direction == Direction.Right);
            case Direction.Up: return !(((Wall)unit).direction == Direction.Down);
            case Direction.Down: return !(((Wall)unit).direction == Direction.Up);
            default: return true;
        }

    }
    public static LogicalEngine GetEngine()
    {
        return staticengine;
    }
}

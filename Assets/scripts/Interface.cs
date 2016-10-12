using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
    public GameObject player;
    private LogicalEngine engine;
    private static LogicalEngine staticengine;
    public int x, y;
    Database database;
    public Direction Gravity_Directin;
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
        if (database.state == State.Idle)
        {
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if(isEmpty(new Vector2(1, 0)))
                    {
                        engine.move(Direction.Right);
                    }
                    
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if(isEmpty(new Vector2(-1, 0)))
                    {
                        engine.move(Direction.Left);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if(isEmpty(new Vector2(0, -1)))
                    {
                        engine.move(Direction.Down);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if(isEmpty(new Vector2(0, 1)))
                    {
                        engine.move(Direction.Up);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    engine.Act(Direction.Right);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    engine.Act(Direction.Left);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    engine.Act(Direction.Down);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    engine.Act(Direction.Up);
                }
                else
                {
                    engine.Act();
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                engine.Absorb();
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                engine.Undo();
            }
        }
    }
    private bool isEmpty(Vector2 dir)
    {
        int x1 = (int)Mathf.Ceil(player.transform.position.x)+(int)dir.x;
        int y1 = (int)Mathf.Ceil(player.transform.position.y)+(int)dir.y;
        foreach(Unit unit  in Database.database.units[x1, y1])
        {
            if (unit.unitType == UnitType.Block || unit.unitType == UnitType.Container || unit.unitType== UnitType.Rock)
                return false;
            else if ( unit.unitType == UnitType.Wall)
            {
                return WallDetector(unit, Toolkit.VectorToDirection(dir));
            }
            else if (unit.unitType == UnitType.Door)
            {
                if (((Door)unit).open)
                {
                    ((Door)unit).NextScene();
                }
                else
                    return false;
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

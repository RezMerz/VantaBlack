using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
    public GameObject player;
    private LogicalEngine engine;
    public int x, y;
    Database database;
	// Use this for initialization
	void Start () {
        database.state = State.Busy;
        Database.database.player = player;
        database = Database.database;
        engine = new LogicalEngine(x, y);
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
        int x = (int)Mathf.Ceil(player.transform.position.x)+(int)dir.x;
        int y = (int)Mathf.Ceil(player.transform.position.y)+(int)dir.y;
        foreach(Unit unit  in Database.database.units[x, y])
        {
            if (unit.obj.tag == "Container" || unit.obj.tag == "Block" || unit.obj.tag == "wall")
                return false;
        }
       
        return true;
    }
}

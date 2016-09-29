using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
    public GameObject player;
    private LogicalEngine engine;
    public int x, y;
    State state;
	// Use this for initialization
	void Start () {
        Database.database.player = player;
        engine = new LogicalEngine(x, y);
        state = State.Idle;
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Idle)
        {
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    print("right");
                    
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    engine.move(Direction.Left);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    engine.move(Direction.Down);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    engine.move(Direction.Up);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    engine.Action(Direction.Right);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    engine.Action(Direction.Left);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    engine.Action(Direction.Down);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    engine.Action(Direction.Up);
                }
                else
                {
                    engine.Action();
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                engine.Absorb();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                
            }
        }
    }
    private bool isEmpty()
    {
        return false;
    }
}

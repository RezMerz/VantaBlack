using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
    public GameObject player;
    private LogicalEngine engine;
    State state;
	// Use this for initialization
	void Start () {
        engine = new LogicalEngine();
        Database.database.player = player;
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
                    engine.move(Direction.Right);
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

                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {

                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {

                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                }
                else
                {

                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {

            }
        }
    }
}

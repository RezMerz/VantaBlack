using UnityEngine;
using System.Collections;

public class GraphicalEngine : MonoBehaviour {

    Database database;

    public GraphicalEngine()
    {
        database = Database.database;
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void _blink(Direction dir)
    {
        switch (dir)
        {
            case Direction.Down: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(0, -2)); break;
            case Direction.Up: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(0, 2)); break;
            case Direction.Right: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(2, 0)); break;
            case Direction.Left: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(-2, 0)); break;
        }
    }

    public void _jump(int height)
    {
        switch (database.gravity_direction)
        {
            case Direction.Down: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(0, height)); break;
            case Direction.Up: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(0, -height)); break;
            case Direction.Right: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(-height, 0)); break;
            case Direction.Left: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(2, height)); break;
        }
    }

    public void _move(Direction dir)
    {
        switch (dir)
        {
            case Direction.Down: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(0, -1)); break;
            case Direction.Up: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(0, 1)); break;
            case Direction.Right: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(1, 0)); break;
            case Direction.Left: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(-1, 0)); break;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Threading;
using System.Runtime.CompilerServices;
public class GraphicalEngine {

    Database database;
    private UI ui;
    private bool fall = false;
    private int fall_pos;
    private float gravity = 9.8f;
    private float velocity = 5;
    private float fall_distance;
    private Vector2 player_pos;
    private float lean_offset = 0.2f;
    public float rotate_value;
    private float top_rotate;
    public GraphicalEngine()
    {
        database = Database.database;
        rotate_value = 35;
        top_rotate = 75;
        //ui = GameObject.Find("Canvas").GetComponent<UI>();
    }

	// Use this for initialization


    public void Refresh()
    {
        for(int i=0; i<database.units.GetLength(0); i++)
        {
            for(int j=0; j < database.units.GetLength(1); j++)
            {
                for(int k=0; k<database.units[i,j].Count; k++)
                {
                    database.units[i, j][k].obj.transform.position = database.units[i, j][k].position;
                }
            }
        }
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public  void _move(Direction dir)
    {
        
        switch (dir)
        {
            case Direction.Down: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(0, -1)); break;
            case Direction.Up: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(0, 1)); break;
            case Direction.Right: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(1, 0)); break;
            case Direction.Left: Database.database.player.transform.position = Toolkit.VectorSum(Database.database.player.transform.position, new Vector2(-1, 0)); break;
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static void MoveObject(GameObject obj, Vector2 position)
    {
        obj.transform.position = position;

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool _Move_Object(GameObject obj, Vector2 pos)
    {
        obj.transform.position = pos;
        return true;
    }

    public void _lean_right()
    {

        database.player.transform.Rotate(new Vector3(0, 0, -rotate_value));
    }

    public void _lean_right_undo()
    {
        database.player.transform.Rotate(new Vector3(0, 0, rotate_value));
    }

    public void _lean_top()
    {
        database.player.transform.Rotate(new Vector3(0, 0, top_rotate));
    }

    public void _lean_top_undo()
    {
        database.player.transform.Rotate(new Vector3(0, 0, -top_rotate));
    }
    public void _lean_left()
    {
        database.player.transform.Rotate(new Vector3(0, 0, rotate_value));
    }

    public void _lean_left_undo()
    {
        database.player.transform.Rotate(new Vector3(0, 0, -rotate_value));
    }

    public void _right_light()
    {
        //ui._right_light();
    }

    public void _gravity()
    {
        if (fall)
        {
            Vector2 cal = Time.deltaTime * _direction_to_vector(database.gravity_direction) * velocity ;
            fall_distance += Time.deltaTime * velocity;
            velocity += Time.deltaTime * gravity;
            database.player.transform.position = Toolkit.VectorSum(database.player.transform.position, cal);
            if (fall_distance > fall_pos)
            {
                fall = false;
                database.player.transform.position = Toolkit.VectorSum(player_pos, _direction_to_vector(database.gravity_direction) * fall_pos);
            }
        }
    }

    
    private Vector2 _direction_to_vector(Direction dir)
    {
        if (dir == Direction.Down)
            return new Vector2(0, -1);
        else if (dir == Direction.Left)
            return new Vector2(-1, 0);
        else if (dir == Direction.Right)
            return new Vector2(1, 0);
        else if (dir == Direction.Down)
            return new Vector2(0, 1);
        return new Vector2(0, 0);
    }

    public void _fall_activate(int pos)
    {
        fall_distance = 0;
        fall = true;
        fall_pos = pos;
        player_pos = database.player.transform.position;
    }

    public void _Player_Change_Ability(Ability ability)
    {
        string path = @"player\";
            switch (ability.abilitytype)
            {
                case AbilityType.Fuel: path += "player-green"; break;
                case AbilityType.Direction: path += "player-red"; break;
            }      
        database.player.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(path, typeof(Sprite));
    }

    public void _Player_Change_Direction(Direction dir)
    {
        float rot = 0;
        switch (dir)
        {
            case Direction.Left: rot = 180; break;
            case Direction.Right: rot = 0; break;
        }
        Database.database.player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
    }

    public void _Container_Change_Sprite(Unit unit,int fill,Ability ability)
    {
        string path = @"block\";
        switch (ability.abilitytype)
        {
            case AbilityType.Fuel: path += "green-light-middle"; break;
            case AbilityType.Direction: path += "red-light-middle"; break;
        }
        unit.obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(path, typeof(Sprite));
    }

    public void _Block_Change_Sprite(Unit unit, Ability ability)
    {
        string path = @"blocks\";
        switch (ability.abilitytype)
        {
            case AbilityType.Fuel: path += "block-green"; break;
            case AbilityType.Direction: path += "block-red"; break;
        }

        unit.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(path,typeof(Sprite));
    }

}

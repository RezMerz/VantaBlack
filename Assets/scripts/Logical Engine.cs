using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LogicalEngine {
    Database database;
    Player player;
    GraphicalEngine Gengine;
    int x, y;

    public LogicalEngine (int x, int y) {
        database = Database.database;
        player = database.player.GetComponent<Player>();
        Gengine = new GraphicalEngine();
        database.units = new List<Unit>[x, y];
        this.x = x;
        this.y = y;
        init();
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

    public void Action()
    {
        switch (player.ability.abilitytype)
        {
            case AbilityType.Direction: ChangeDirection(); break;
            case AbilityType.Jump: jump();  break;
            case AbilityType.Rope: break;
        }
    }

    public void Action(Direction dir)
    {
        switch (player.ability.abilitytype)
        {
            case AbilityType.Blink: Blink(dir); break;
            case AbilityType.Gravity: ChangeGravity(dir); break;
        }
    }

    public void Absorb(Direction dir)
    {
        
    }

    public void Absorb()
    {
        switch (database.gravity_direction)
        {
            case Direction.Down:GetBlock(Toolkit.VectorSum(player.position, new Vector2(0, -1))); break;
            case Direction.Up: break;
            case Direction.Right: break;
            case Direction.Left: break;
        }
    }



    public void Swap(GameObject g)
    {
        Ability block_ability = g.GetComponent<Block>().ability;
        g.GetComponent<Block>().ability = player.ability;
        player.ability = block_ability;

    }

    public void move(Direction dir)
    {
        Gengine._move(dir);
    }

    public void jump()
    {
        if (CheckJump())
            Gengine._jump(player.ability.function);
    }

    public void ChangeGravity(Direction direction)
    {
        for(int i=0; i<player.ability.direction.Count; i++)
        {
            if (direction == player.ability.direction[i])
            {
                Direction temp = database.gravity_direction;
                database.gravity_direction = direction;
                player.ability.direction[i] = temp;
            }
        }
    }

    public void Blink(Direction dir)
    {
        if (CheckBlink(dir))
            Gengine._blink(dir);
    }

    public void Teleport(Vector2 position)
    {
        Database.database.player.transform.position = position;
    }

    public void Drain()
    {
        player.ability = null;
    }

    public void Fountain(int num)
    {
        player.ability.numberofuse += num;
    }



                            /// private methods ///

    /*private bool CheckMove(Direction dir)
    {
        foreach (Direction d in player.move_direction)
        {
            if (dir == d)
            {
                switch (dir)
                {
                    
                }
                return true;
            }
        }
        return false;
    }*/

    

    private void ChangeDirection()
    {
        switch (player.move_direction[0])
        {
            case Direction.Down: player.move_direction[0] = Direction.Up; break;
            case Direction.Up: player.move_direction[0] = Direction.Down; break;
            case Direction.Right: player.move_direction[0] = Direction.Left; break;
            case Direction.Left: player. move_direction[0] = Direction.Right; break;
        }
    }

    private bool CheckJump()
    {
        return true;
    }

    

    private bool CheckBlink(Direction direction)
    {
        foreach(Direction d in player.move_direction)
            if (direction == d)
                return true;
        return false;
    }

    private Unit GetBlock(Vector2 position)
    {
        foreach(Unit u in database.units[(int)position.x, (int)position.y])
            if (u.type == UnitType.Block)
                return u;

        return null;
    }

}

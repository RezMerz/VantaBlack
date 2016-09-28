using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LogicalEngine {
    Database database;
    Player player;
    GraphicalEngine Gengine;


    public LogicalEngine () {
        database = Database.database;
        player = database.player.GetComponent<Player>();
        Gengine = new GraphicalEngine();
        init();
	}

    void init()
    {
        List<GameObject> Gobjects = new List<GameObject>();
        List<Unit> units = new List<Unit>();
        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Block"));
        foreach(GameObject g in Gobjects)
            units.Add(new Unit(UnitType.Block, g, g.GetComponent<Block>()));
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Pipe"));
        foreach (GameObject g in Gobjects)
            units.Add(new Unit(UnitType.Block, g, null));
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Container"));
        foreach (GameObject g in Gobjects)
            units.Add(new Unit(UnitType.Block, g, g.GetComponent<Container>()));
        Gobjects.Clear();

        Gobjects.AddRange(GameObject.FindGameObjectsWithTag("Switch"));
        foreach (GameObject g in Gobjects)
            units.Add(new Unit(UnitType.Block, g, g.GetComponent<Switch>()));
        Gobjects.Clear();


    }


    public void Swap(GameObject g)
    {
        Ability block_ability = g.GetComponent<Block>().ability;
        g.GetComponent<Block>().ability = player.ability;
        player.ability = block_ability;

    }

    public void move(Direction dir)
    {
        if (CheckMove(dir))
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

    private bool CheckMove(Direction dir)
    {
        foreach (Direction d in player.move_direction)
        {
            if (dir == d)
            {

                return true;
            }
        }
        return false;
    }

    

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

    

}

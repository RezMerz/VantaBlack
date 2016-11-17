using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnapshotManager{

    public SnapshotManager()
    {

    }

    public void takesnapshot()
    {
        Snapshot snp = new Snapshot(Database.database.units);
        List<Unit>[,] units = new List<Unit>[Database.database.units.GetLength(0), Database.database.units.GetLength(1)];
        for (int i = 0; i < Database.database.units.GetLength(0); i++)
        {
            for (int j = 0; j < Database.database.units.GetLength(1); j++)
            {
                units[i, j] = new List<Unit>();
                for (int k = 0; k < Database.database.units[i, j].Count; k++)
                {
                    units[i, j].Add(Database.database.units[i, j][k].Clone());
                }
            }
        }
        snp.units = units;
        Database.database.snapshots.Add(snp);
        Wall.print(snp.units);
        Database.database.snapShotCount++;
    }

    public Snapshot Revese()
    {
        if (Database.database.snapShotCount != 0)
        {
            Snapshot snapshot = Database.database.snapshots[Database.database.snapShotCount - 1];
            Database.database.snapshots.RemoveAt(Database.database.snapShotCount - 1);
            Database.database.snapShotCount--;
            return snapshot;
        }
        return null;
    }
}

public class Snapshot
{
    public List<Unit>[,] units;
    public long turn;
    
    public Snapshot(List<Unit>[,] units)
    {
        
        units = new List<Unit>[Database.database.units.GetLength(0), Database.database.units.GetLength(1)];
        for (int i = 0; i < Database.database.units.GetLength(0); i++)
        {
            for (int j = 0; j < Database.database.units.GetLength(1); j++)
            {
                units[i, j] = new List<Unit>();
                for(int k=0; k< Database.database.units[i,j].Count; k++)
                {
                    units[i, j].Add(Database.database.units[i, j][k].Clone());
                }
            }
        }
        Wall.print(units);
        turn = 0;
        //turn = Database.database.turn;
        //this.player = player;
    }
}


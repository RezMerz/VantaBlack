using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnapshotManager {

    public SnapshotManager()
    {

    }

    public void takesnapshot()
    {
        if(Database.database.snapShotCount == Database.database.numberOfSnapshot)
            Database.database.snapshots.RemoveAt(0);
        Database.database.snapshots.Add(new Snapshot((List<Unit>[,])Database.database.units));
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
        for (int i = 0; i < Database.database.units.GetLength(0); i++)
        {
            for (int j = 0; j < Database.database.units.GetLength(1); j++)
            {
                units[i, j] = new List<Unit>();
                units[i, j].AddRange(Database.database.units[i, j]);
            }
        }
        turn = Database.database.turn;
        //this.player = player;
    }
}


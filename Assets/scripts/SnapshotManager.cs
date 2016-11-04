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
        Database.database.snapshots.Add(new Snapshot((List<Unit>[,])Database.database.units.Clone()));
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

        this.units = units;
        turn = Database.database.turn;
        //this.player = player;
    }
}


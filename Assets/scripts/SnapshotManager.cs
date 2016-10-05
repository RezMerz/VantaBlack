using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnapshotManager {

    public SnapshotManager()
    {

    }

    public void takesnapshot()
    {
        Database.database.snapshots.RemoveAt(0);
        Database.database.snapshots.Add(new Snapshot((List<Unit>[,])Database.database.units.Clone()));

    }

    public Snapshot Revese()
    {
        Snapshot snapshot = Database.database.snapshots[Database.database.snapshots.Count - 1];
        Database.database.snapshots.RemoveAt(Database.database.snapshots.Count - 1);
        return snapshot;
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


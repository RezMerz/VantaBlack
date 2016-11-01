using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour {
    public Vector2 position { get; set; }
    public UnitType unitType { get; set; }
    public GameObject obj { get; set; }
    public long codeNumber { get; set; }
    public bool movable { get; set; }

    public static int Code = 0;

    public bool CanBeMoved;

    public int layer { get; set; }

    public  abstract bool CanMove(UnitType unittype);
   
}

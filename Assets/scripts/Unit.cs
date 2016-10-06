using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    public Vector2 position { get; set; }
    public UnitType unitType { get; set; }
    public GameObject obj { get; set; }
    public long codeNumber { get; set; }

    public static int Code = 0;

}

using UnityEngine;
using System.Collections;

public sealed class Toolkit{

    public static Vector2 VectorSum(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }
}


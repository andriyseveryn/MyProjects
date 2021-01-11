using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vectour
{
    public static Vector2 VectorRound(Vector2 Input)
    {
        return new Vector2(Mathf.Round(Input.x), Mathf.Round(Input.y));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoardState
{
    public float score;
    public int x;
    public int rot;
    public int slowFall;

    public int hangs;
    public int clears;

    public BoardState(float newScore, int newX, int newRot, int newSlowFall)
    {
        score = newScore;
        x = newX;
        rot = newRot;
        slowFall = newSlowFall;

        hangs = 0;
        clears = 0;
    }
}

using System;
using UnityEngine;

[Serializable]
public class Obstacle
{
    public GameObject obsObject;
    public int side;
    public int sideTo;
    public int length;

    public Obstacle(GameObject obsObject, int side, int sideTo, int length)
    {
        this.obsObject = obsObject;
        this.side = side;
        this.sideTo = sideTo;
        this.length = length;
    }

    public GameObject O
    {
        get => obsObject;
        set => obsObject = value;
    }

    public int Side
    {
        get => side;
        set => side = value;
    }

    public int SideTo
    {
        get => sideTo;
        set => sideTo = value;
    }

    public int Length
    {
        get => length;
        set => length = value;
    }
}
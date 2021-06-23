using System;
using UnityEngine;

[Serializable]
public class Obstacle
{
    public GameObject obsObject;
    public int side;
    public int[] length;

    public Obstacle(GameObject obsObject, int side, int[] length)
    {
        this.obsObject = obsObject;
        this.side = side;
        this.length = length;
    }

    public GameObject ObsObject
    {
        get => obsObject;
        set => obsObject = value;
    }

    public int Side
    {
        get => side;
        set => side = value;
    }

    public int[] Length
    {
        get => length;
        set => length = value;
    }
}
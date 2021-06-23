using System;
using UnityEngine;

[Serializable]
public class Language
{
    public string name;
    public Sprite flag;
    public string play;
    public string pause;
    public string credits;
    public string fail;
    public string score;
    public string distance;

    public Language(string name, Sprite flag, string play)
    {
        this.name = name;
        this.flag = flag;
        this.play = play;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public Sprite Flag
    {
        get => flag;
        set => flag = value;
    }

    public string Play
    {
        get => play;
        set => play = value;
    }
}
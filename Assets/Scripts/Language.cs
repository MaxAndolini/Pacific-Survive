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
    public string high;
    public string people;
    public string score;
    public string distance;
    public string time;
    public string highScore;

    public Language(string name, Sprite flag, string play, string pause, string credits, string fail, string high,
        string people, string score, string distance, string time, string highScore)
    {
        this.name = name;
        this.flag = flag;
        this.play = play;
        this.pause = pause;
        this.credits = credits;
        this.fail = fail;
        this.high = high;
        this.people = people;
        this.score = score;
        this.distance = distance;
        this.time = time;
        this.highScore = highScore;
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

    public string Pause
    {
        get => pause;
        set => pause = value;
    }

    public string Credits
    {
        get => credits;
        set => credits = value;
    }

    public string Fail
    {
        get => fail;
        set => fail = value;
    }

    public string High
    {
        get => high;
        set => high = value;
    }

    public string People
    {
        get => people;
        set => people = value;
    }

    public string Score
    {
        get => score;
        set => score = value;
    }

    public string Distance
    {
        get => distance;
        set => distance = value;
    }

    public string Time
    {
        get => time;
        set => time = value;
    }

    public string HighScore
    {
        get => highScore;
        set => highScore = value;
    }
}
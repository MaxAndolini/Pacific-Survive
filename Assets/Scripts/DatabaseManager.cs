using System;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private const string DSound = "sound";
    private const string DVibration = "vibration";
    private const string DLanguage = "language";
    private const string DLifebuoy = "lifebuoy";
    private const string DPeople = "people_";
    private const string DDistance = "distance_";
    private const string DTime = "time_";
    public static DatabaseManager Instance { get; private set; }

    public int Language
    {
        get => PlayerPrefs.GetInt(DLanguage, 0);
        set => PlayerPrefs.SetInt(DLanguage, value);
    }

    public int Lifebuoy
    {
        get => PlayerPrefs.GetInt(DLifebuoy, 0);
        set => PlayerPrefs.SetInt(DLifebuoy, value);
    }

    public bool Sound
    {
        get => PlayerPrefs.GetInt(DSound, 1) == 1;
        set => PlayerPrefs.SetInt(DSound, value ? 1 : 0);
    }

    public bool Vibration
    {
        get => PlayerPrefs.GetInt(DVibration, 1) == 1;
        set => PlayerPrefs.SetInt(DVibration, value ? 1 : 0);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public string[] GetHighScore(int line)
    {
        if (line >= 1 && line <= 10)
            return new[]
            {
                PlayerPrefs.GetInt(DPeople + line, 0).ToString("D12"),
                PlayerPrefs.GetInt(DDistance + line, 0).ToString("D12"), PlayerPrefs.GetString(DTime + line, "-")
            };

        return null;
    }

    public bool SetHighScore(int score, int distance)
    {
        for (var i = 1; i <= 10; i++)
            if (score > PlayerPrefs.GetInt(DPeople + i, 0))
            {
                for (var x = 10; x > i; x--)
                {
                    var tempScore = PlayerPrefs.GetInt(DPeople + (x - 1), 0);
                    var tempDistance = PlayerPrefs.GetInt(DDistance + (x - 1), 0);
                    var tempTime = PlayerPrefs.GetString(DTime + (x - 1), "-");

                    PlayerPrefs.SetInt(DPeople + x, tempScore);
                    PlayerPrefs.SetInt(DDistance + x, tempDistance);
                    PlayerPrefs.SetString(DTime + x, tempTime);
                }

                PlayerPrefs.SetInt(DPeople + i, score);
                PlayerPrefs.SetInt(DDistance + i, distance);
                PlayerPrefs.SetString(DTime + i, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                return true;
            }

        return false;
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
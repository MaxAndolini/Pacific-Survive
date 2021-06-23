using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private const string DSound = "sound";
    private const string DVibration = "vibration";
    private const string DLanguage = "language";
    public static DatabaseManager Instance { get; private set; }

    public int Langugage
    {
        get => PlayerPrefs.GetInt(DLanguage, 0);
        set => PlayerPrefs.SetInt(DLanguage, value);
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

    public void Save()
    {
        PlayerPrefs.Save();
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
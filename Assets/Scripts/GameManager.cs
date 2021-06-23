using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float left = -8.5f;
    public float right = 8.5f;
    public bool isPaused;
    public bool isSoundActive;
    public bool isVibrationActive;
    public static GameManager Instance { get; private set; }

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

    private void Start()
    {
        GUIManager.Instance.ShowMain();
    }

    public void StartGame()
    {
        GUIManager.Instance.ShowResume();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public void RestartGame()
    {
    }

    public void ChangeSound()
    {
        isSoundActive = !isSoundActive;
        Debug.Log("Sound Active " + isSoundActive);
    }

    public void ChangeVibration()
    {
        isVibrationActive = !isVibrationActive;
        Debug.Log("Vibration Active " + isVibrationActive);
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    // Update is called once per frame
    private void Update()
    {
#if UNITY_EDITOR
        if (GUIManager.Instance.activeScreen == GUIManager.Screen.Main && Input.GetMouseButtonDown(0))
            GUIManager.Instance.ShowResume();
#else
        if (GUIManager.Instance.activeScreen == GUIManager.Screen.Main && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GUIManager.Instance.ShowResume();
        }
#endif
    }

    public void StartGame()
    {
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
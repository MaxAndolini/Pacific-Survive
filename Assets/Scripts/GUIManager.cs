using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject main;
    public GameObject inGame;
    public GameObject pause;
    public GameObject fail;
    public static GUIManager Instance { get; private set; }

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
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ShowMain()
    {
        main.SetActive(true);
        inGame.SetActive(false);
        pause.SetActive(false);
        fail.SetActive(false);
    }
    
    public void ShowPause()
    {
        main.SetActive(false);
        inGame.SetActive(true);
        pause.SetActive(true);
        fail.SetActive(false);
        GameManager.Instance.PauseGame();
    }
    
    public void ShowResume()
    {
        main.SetActive(false);
        inGame.SetActive(true);
        pause.SetActive(false);
        fail.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
    
    public void ShowFail()
    {
        main.SetActive(false);
        inGame.SetActive(true);
        pause.SetActive(false);
        fail.SetActive(true);
        GameManager.Instance.PauseGame();
    }
}
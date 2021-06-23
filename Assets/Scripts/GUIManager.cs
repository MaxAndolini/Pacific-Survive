using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public enum Screen
    {
        Main,
        InGame,
        Pause,
        Fail
    }

    public GameObject main;
    public GameObject inGame;
    public GameObject pause;
    public GameObject fail;
    public GameObject credits;
    public Screen activeScreen = Screen.Main;
    public int lastScreen;
    public bool creditsActive;
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
        activeScreen = Screen.Main;
        main.SetActive(true);
        inGame.SetActive(false);
        pause.SetActive(false);
        fail.SetActive(false);
        lastScreen = 2;
        GameManager.Instance.PauseGame();
    }

    public void ShowPause()
    {
        activeScreen = Screen.Pause;
        main.SetActive(false);
        inGame.SetActive(true);
        inGame.transform.GetChild(1).gameObject.SetActive(false);
        pause.SetActive(true);
        fail.SetActive(false);
        GameManager.Instance.PauseGame();
    }

    public void ShowResume()
    {
        activeScreen = Screen.InGame;
        main.SetActive(false);
        inGame.SetActive(true);
        inGame.transform.GetChild(1).gameObject.SetActive(true);
        pause.SetActive(false);
        fail.SetActive(false);
        lastScreen = 2;
        GameManager.Instance.ResumeGame();
    }

    public void ShowFail()
    {
        activeScreen = Screen.Fail;
        main.SetActive(false);
        inGame.SetActive(true);
        pause.SetActive(false);
        fail.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    private void ShowCredits()
    {
        creditsActive = !creditsActive;
        for (var i = 0; i < main.transform.childCount; i++)
            if (main.transform.GetChild(i).name != "Credits" &&
                main.transform.GetChild(i).GetComponent<Button>() != null)
            {
                main.transform.GetChild(i).GetComponent<Button>().enabled = !creditsActive;
                credits.SetActive(creditsActive);
            }
    }
}
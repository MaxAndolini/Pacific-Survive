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
    public GameObject top10;
    public GameObject credits;
    public GameObject sound;
    public GameObject vibration;
    public GameObject soundInMenu;
    public GameObject vibrationInMenu;
    public GameObject revive;
    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite vibrationOn;
    public Sprite vibrationOff;
    public Screen activeScreen = Screen.Main;
    public int lastScreen;
    public bool top10Active;
    public bool creditsActive;
    public Text lifebuoy;
    public Text score;
    public Text distance;
    public Text failed;
    public Text lifebuoyInMenu;
    public Text scoreInMenu;
    public Text distanceInMenu;
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

    private void Update()
    {
        if (activeScreen == Screen.InGame) Distance(GameManager.Instance.distance);
    }

    public void ShowMain()
    {
        activeScreen = Screen.Main;
        main.SetActive(true);
        inGame.SetActive(false);
        pause.SetActive(false);
        fail.SetActive(false);
        lastScreen = 2;
        GameManager.Instance.SpecialTime();
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
        SoundManager.Instance.Play("Click");
    }

    public void ShowResume(bool soundStatus)
    {
        activeScreen = Screen.InGame;
        main.SetActive(false);
        inGame.SetActive(true);
        inGame.transform.GetChild(1).gameObject.SetActive(true);
        pause.SetActive(false);
        fail.SetActive(false);
        lastScreen = 2;
        GameManager.Instance.ResumeGame();
        if (soundStatus) SoundManager.Instance.Play("Click");
    }

    public void ShowFail()
    {
        activeScreen = Screen.Fail;
        main.SetActive(false);
        inGame.SetActive(true);
        inGame.transform.GetChild(1).gameObject.SetActive(false);
        pause.SetActive(false);

        var lf = GameManager.Instance.lifebuoy;
        var sc = GameManager.Instance.score;
        var ds = GameManager.Instance.distance;

        failed.text = DatabaseManager.Instance.SetHighScore(sc, ds)
            ? LanguageManager.Instance.Get().High
            : LanguageManager.Instance.Get().Fail;

        var rv = ds / 5;
        if (lf > 0 && rv > 0)
        {
            revive.SetActive(true);
            lifebuoyInMenu.gameObject.SetActive(true);
            lifebuoyInMenu.text = rv.ToString("D12");
        }
        else
        {
            revive.SetActive(false);
            lifebuoyInMenu.gameObject.SetActive(false);
        }

        scoreInMenu.text = sc.ToString("D12");
        distanceInMenu.text = ds.ToString("D12");


        fail.SetActive(true);
        SoundManager.Instance.Play("Fail");
        GameManager.Instance.PauseGame();
    }

    public void ShowTop10()
    {
        top10Active = !top10Active;
        for (var i = 0; i < main.transform.childCount; i++)
            if (main.transform.GetChild(i).name != "Top 10" &&
                main.transform.GetChild(i).GetComponent<Button>() != null)
            {
                main.transform.GetChild(i).GetComponent<Button>().enabled = !top10Active;
                top10.SetActive(top10Active);
            }

        SoundManager.Instance.Play("Click");
    }

    public void ShowCredits()
    {
        creditsActive = !creditsActive;
        for (var i = 0; i < main.transform.childCount; i++)
            if (main.transform.GetChild(i).name != "Credits" &&
                main.transform.GetChild(i).GetComponent<Button>() != null)
            {
                main.transform.GetChild(i).GetComponent<Button>().enabled = !creditsActive;
                credits.SetActive(creditsActive);
            }

        SoundManager.Instance.Play("Click");
    }

    public void Sound(bool status)
    {
        if (status)
        {
            sound.GetComponent<Image>().sprite = soundOn;
            soundInMenu.GetComponent<Image>().sprite = soundOn;
        }
        else
        {
            sound.GetComponent<Image>().sprite = soundOff;
            soundInMenu.GetComponent<Image>().sprite = soundOff;
        }

        SoundManager.Instance.Mute(!status);
    }

    public void Vibration(bool status)
    {
        if (status)
        {
            vibration.GetComponent<Image>().sprite = vibrationOn;
            vibrationInMenu.GetComponent<Image>().sprite = vibrationOn;
        }
        else
        {
            vibration.GetComponent<Image>().sprite = vibrationOff;
            vibrationInMenu.GetComponent<Image>().sprite = vibrationOff;
        }
    }

    public void Lifebuoy(int number)
    {
        lifebuoy.text = number.ToString("D12");
    }

    public void Score(int number)
    {
        score.text = number.ToString("D12");
    }

    public void Distance(int number)
    {
        distance.text = number.ToString("D12");
    }

    public void Clean()
    {
        DatabaseManager.Instance.DeleteAll();
        for (var i = 0; i < top10.transform.childCount; i++)
            if (top10.transform.GetChild(i).name != "Clean" &&
                top10.transform.GetChild(i).GetComponent<Top10>() != null)
                top10.transform.GetChild(i).GetComponent<Top10>().GetHighScore();
    }
}
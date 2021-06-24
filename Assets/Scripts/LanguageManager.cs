using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public Language[] languages;
    public Button button;
    public Text play;
    public Text pause;
    public Text credits;
    public Text score;
    public Text distance;
    public Text highScore;
    public Text peopleH;
    public Text distanceH;
    public Text timeH;
    private int _index;
    public static LanguageManager Instance { get; private set; }

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
        _index = DatabaseManager.Instance.Language;
        ChangeLanguage(_index);
    }

    public void ChangeLanguage(int index)
    {
        play.text = languages[index].Play;
        pause.text = languages[index].Pause;
        credits.text = languages[index].Credits;
        score.text = languages[index].Score;
        distance.text = languages[index].Distance;
        highScore.text = languages[index].HighScore;
        peopleH.text = languages[index].People;
        distanceH.text = languages[index].Distance;
        timeH.text = languages[index].Time;
        button.GetComponent<Image>().sprite = languages[index].Flag;
        if (index != DatabaseManager.Instance.Language) DatabaseManager.Instance.Language = index;
        _index = index;
    }

    public Language Get()
    {
        return languages[_index];
    }

    public void LanguageButton()
    {
        if (languages.Length > _index + 1) _index++;
        else _index = 0;

        ChangeLanguage(_index);
        SoundManager.Instance.Play("Click");
    }
}
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public Language[] languages;
    public Button button;
    public Text play;
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
        _index = DatabaseManager.Instance.Langugage;
        ChangeLanguage(_index);
    }

    public void ChangeLanguage(int index)
    {
        play.text = languages[index].Play;
        button.GetComponent<Image>().sprite = languages[index].Flag;
        if (index != DatabaseManager.Instance.Langugage) DatabaseManager.Instance.Langugage = index;
        _index = index;
    }

    public void LanguageButton()
    {
        if (languages.Length > _index + 1) _index++;
        else _index = 0;

        ChangeLanguage(_index);
    }
}
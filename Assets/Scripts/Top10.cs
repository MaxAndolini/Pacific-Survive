using UnityEngine;
using UnityEngine.UI;

public class Top10 : MonoBehaviour
{
    public int line;
    public Text nth;
    public Text people;
    public Text distance;
    public Text time;

    private void Start()
    {
        GetHighScore();
    }

    private void OnEnable()
    {
        GetHighScore();
    }

    public void GetHighScore()
    {
        var top = DatabaseManager.Instance.GetHighScore(line);
        nth.text = line + ".";
        people.text = top[0];
        distance.text = top[1];
        time.text = top[2];
    }
}
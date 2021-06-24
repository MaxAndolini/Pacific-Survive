using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject seas;
    public float left = -8.5f;
    public float right = 8.5f;
    public bool isPaused;
    public bool isSoundActive;
    public bool isVibrationActive;
    public int lifebuoy;
    public int score;
    public int distance;
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
        isSoundActive = DatabaseManager.Instance.Sound;
        GUIManager.Instance.Sound(isSoundActive);
        isVibrationActive = DatabaseManager.Instance.Vibration;
        GUIManager.Instance.Vibration(isVibrationActive);
        lifebuoy = DatabaseManager.Instance.Lifebuoy;
        GUIManager.Instance.Lifebuoy(lifebuoy);
    }

    private void Update()
    {
        distance = (int) (ship.transform.position.z / 10);
    }

    public void StartGame()
    {
        score = 0;
        distance = 0;
        GUIManager.Instance.Score(score);
        GUIManager.Instance.Distance(distance);
        GUIManager.Instance.ShowResume(false);
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

    public void MainMenu()
    {
        Restart();
        GUIManager.Instance.ShowMain();
        SoundManager.Instance.Play("Click");
    }

    public void RestartGame()
    {
        Restart();
        GUIManager.Instance.ShowResume(true);
    }

    public void ReviveGame()
    {
        var hitColliders = Physics.OverlapSphere(ship.transform.position, 70);
        foreach (var hitCollider in hitColliders)
            if (hitCollider.gameObject.CompareTag("Obstacle"))
                Destroy(hitCollider.gameObject);

        DatabaseManager.Instance.Lifebuoy -= distance / 5;
        GUIManager.Instance.Lifebuoy(DatabaseManager.Instance.Lifebuoy);
        GUIManager.Instance.ShowResume(true);
        SoundManager.Instance.Play("LifebuoyActivate");
    }

    private void Restart()
    {
        ship.transform.position = new Vector3(0, 0, 0);
        score = 0;
        distance = 0;
        GUIManager.Instance.Score(score);
        GUIManager.Instance.Distance(distance);
        Destroy(GameObject.FindGameObjectWithTag("Seas"));
        var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obs in obstacles)
            Destroy(obs);

        var lifebuoys = GameObject.FindGameObjectsWithTag("Lifebuoy");
        foreach (var lf in lifebuoys)
            Destroy(lf);

        var people = GameObject.FindGameObjectsWithTag("People");
        foreach (var pp in people)
            Destroy(pp);

        Instantiate(seas);
        ship.GetComponent<ShipController>().ResetSpeed();
    }

    public void ChangeSound()
    {
        isSoundActive = !isSoundActive;
        GUIManager.Instance.Sound(isSoundActive);
        DatabaseManager.Instance.Sound = isSoundActive;
        Debug.Log("Sound Active " + isSoundActive);
        if (isSoundActive) SoundManager.Instance.Play("SoundOn");
    }

    public void ChangeVibration()
    {
        isVibrationActive = !isVibrationActive;
        GUIManager.Instance.Vibration(isVibrationActive);
        DatabaseManager.Instance.Vibration = isVibrationActive;
        Debug.Log("Vibration Active " + isVibrationActive);
        SoundManager.Instance.Play("Click");
    }
}
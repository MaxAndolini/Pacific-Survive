using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private const float ChangeDuration = 0.1f;
    public int pixelDistance = 20;
    public float minSpeed = 18f;
    public float maxSpeed = 25f;
    public float speed;
    private float _changeTime;
    private bool _fingerDown;
    private Rigidbody _rb;
    private Vector2 _start;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        speed = minSpeed;
    }

    private void Update()
    {
        if (GUIManager.Instance.activeScreen != GUIManager.Screen.InGame) return;
        if (GUIManager.Instance.lastScreen > 0)
        {
            GUIManager.Instance.lastScreen--;
            _fingerDown = false;
            return;
        }

#if UNITY_ANDROID
        if (!_fingerDown && Input.GetMouseButtonDown(0))
        {
            _start = Input.mousePosition;
            _fingerDown = true;
        }

        if (_fingerDown)
        {
            if (Input.mousePosition.x <= _start.x - pixelDistance) // Swipe Left
            {
                _fingerDown = false;

                var error = false;
                if (Physics.Raycast(transform.position,
                    -transform.right, out var hit, 5f))
                    if (hit.transform.gameObject.CompareTag("Obstacle"))
                        error = true;

                if (!error) StartCoroutine(Move(0));
            }
            else if (Input.mousePosition.x >= _start.x + pixelDistance) // Swipe Right
            {
                _fingerDown = false;

                var error = false;
                if (Physics.Raycast(transform.position,
                    transform.right, out var hit, 5f))
                    if (hit.transform.gameObject.CompareTag("Obstacle"))
                        error = true;

                if (!error) StartCoroutine(Move(1));
            }
        }

        if (_fingerDown && Input.GetMouseButtonUp(0))
            _fingerDown = false;
#else
        if (!_fingerDown && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _start = Input.GetTouch(0).position;
            _fingerDown = true;
        }

        if (!_fingerDown) return;
        if (Input.GetTouch(0).position.x <= _start.x - pixelDistance) // Swipe Left
        {
            _fingerDown = false;

            var error = false;
            if (Physics.Raycast(transform.position,
                -transform.right, out var hit, 5f))
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                    error = true;

            if (!error) StartCoroutine(Move(0));
        }
        else if (Input.GetTouch(0).position.x >= _start.x + pixelDistance) // Swipe Right
        {
            _fingerDown = false;

            var error = false;
            if (Physics.Raycast(transform.position,
                transform.right, out var hit, 5f))
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                    error = true;

            if (!error) StartCoroutine(Move(1));
        }
#endif
    }

    private void FixedUpdate()
    {
        if (GUIManager.Instance.activeScreen == GUIManager.Screen.InGame && !GameManager.Instance.isPaused)
        {
            if (speed < maxSpeed) speed += 0.1f * Time.deltaTime;

            _rb.velocity = new Vector3(0, 0, speed);
        }
        else
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            SoundManager.Instance.Play("Collision");
            gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            if (GameManager.Instance.isVibrationActive) Handheld.Vibrate();
            GUIManager.Instance.ShowFail();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lifebuoy"))
        {
            SoundManager.Instance.Play("LifebuoyCollect");
            if (GameManager.Instance.isVibrationActive) Handheld.Vibrate();
            GameManager.Instance.lifebuoy++;
            GUIManager.Instance.Lifebuoy(GameManager.Instance.lifebuoy);
            DatabaseManager.Instance.Lifebuoy = GameManager.Instance.lifebuoy;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("People"))
        {
            SoundManager.Instance.Play("PeopleCollect");
            if (GameManager.Instance.isVibrationActive) Handheld.Vibrate();
            GameManager.Instance.score++;
            GUIManager.Instance.Score(GameManager.Instance.score);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Move(int direction)
    {
        var error = false;
        if (transform.position.x < GameManager.Instance.left + 0.50f && direction == 0) error = true;
        else if (transform.position.x > GameManager.Instance.right - 0.50f && direction == 1) error = true;
        if (error) yield break;

        var direct = direction == 0 ? GameManager.Instance.left : GameManager.Instance.right;

        _changeTime = 0f;
        var startShip = transform.position.x;

        SoundManager.Instance.Play("Swipe");

        while (_changeTime < ChangeDuration)
        {
            _changeTime += Time.deltaTime;
            var position = transform.position;
            position = new Vector3(Mathf.Lerp(startShip, startShip + direct, _changeTime / ChangeDuration),
                position.y, position.z);
            transform.position = position;

            yield return null;
        }
    }

    public void ResetSpeed()
    {
        speed = minSpeed;
    }
}
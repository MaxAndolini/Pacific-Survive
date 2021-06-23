using System;
using System.Collections;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public int pixelDistance = 20;
    private const float ChangeDuration = 0.1f;
    private float _changeTime;
    private bool _fingerDown;
    private Rigidbody _rb;
    private Vector2 _start;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
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

        _rb.velocity = new Vector3(0, 0, 20f);

#if UNITY_EDITOR
        if (!_fingerDown && Input.GetMouseButtonDown(0))
        {
            _start = Input.mousePosition;
            _fingerDown = true;
        }

        if (_fingerDown)
        {
            if (Input.mousePosition.x <= _start.x - pixelDistance)
            {
                _fingerDown = false;
                Debug.Log("Swipe left");
                StartCoroutine(Move(0));
            }
            else if (Input.mousePosition.x >= _start.x + pixelDistance)
            {
                _fingerDown = false;
                Debug.Log("Swipe right");
                StartCoroutine(Move(1));
            }
        }

        if (_fingerDown && Input.GetMouseButtonUp(0))
            _fingerDown = false;
#else
        if (!fingerDown && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            start = Input.GetTouch(0).position;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.GetTouch(0).position.x <= start.x - pixelDistance)
            {
                fingerDown = false;
                Debug.Log("Swipe left");
                StartCoroutine(Move(0));
            }
            else if (Input.GetTouch(0).position.x >= start.x + pixelDistance)
            {
                fingerDown = false;
                Debug.Log("Swipe right");
                StartCoroutine(Move(1));
            }
        }
#endif
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isPaused) return;

        //rb.AddForce(0, 0, 0.3f, ForceMode.Impulse);
        _rb.velocity = new Vector3(0, 0, 20f);
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

        while (_changeTime < ChangeDuration)
        {
            _changeTime += Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(startShip, startShip + direct, _changeTime / ChangeDuration),
                transform.position.y, transform.position.z);

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("öldün");
        }
    }
}
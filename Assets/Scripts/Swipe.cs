using System.Collections;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public int pixelDistance = 20;
    private readonly float changeDuration = 0.1f;
    private float changeTime;
    private bool fingerDown;
    private Rigidbody rb;
    private Vector2 start;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GUIManager.Instance.activeScreen != GUIManager.Screen.InGame) return;
        if (GUIManager.Instance.lastScreen > 0)
        {
            GUIManager.Instance.lastScreen--;
            fingerDown = false;
            return;
        }

#if UNITY_EDITOR
        if (!fingerDown && Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.mousePosition.x <= start.x - pixelDistance)
            {
                fingerDown = false;
                Debug.Log("Swipe left");
                StartCoroutine(Move(0));
            }
            else if (Input.mousePosition.x >= start.x + pixelDistance)
            {
                fingerDown = false;
                Debug.Log("Swipe right");
                StartCoroutine(Move(1));
            }
        }

        if (fingerDown && Input.GetMouseButtonUp(0))
            fingerDown = false;
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

        rb.AddForce(0, 0, 0.3f, ForceMode.Impulse);
    }

    private IEnumerator Move(int direction)
    {
        var error = false;
        if (transform.position.x < -6.0f && direction == 0) error = true;
        else if (transform.position.x > 6.0f && direction == 1) error = true;
        if (error) yield break;
        var direct = direction == 0 ? -6.50f : 6.50f;
        changeTime = 0f;
        var startShip = transform.position.x;

        while (changeTime < changeDuration)
        {
            changeTime += Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(startShip, startShip + direct, changeTime / changeDuration),
                transform.position.y, transform.position.z);

            yield return null;
        }
    }
}
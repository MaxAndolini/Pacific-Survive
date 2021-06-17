using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject ship;

    private void Start()
    {
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, ship.transform.position.z - 50f);
    }
}
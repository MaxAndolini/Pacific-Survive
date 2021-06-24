using UnityEngine;

public class Lifebuoy : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, -90 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Lifebuoy")) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Lifebuoy")) Destroy(gameObject);
    }
}
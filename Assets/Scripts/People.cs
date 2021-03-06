using UnityEngine;

public class People : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Lifebuoy") ||
            other.gameObject.CompareTag("People"))
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Lifebuoy") ||
            other.gameObject.CompareTag("People"))
            Destroy(gameObject);
    }
}
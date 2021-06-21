using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!CompareTag("Untagged") && other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.tag = "Untagged";
            Destroy(other.gameObject);
        }
    }
}
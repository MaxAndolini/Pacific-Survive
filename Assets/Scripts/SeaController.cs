using UnityEngine;

public class SeaController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            transform.position += new Vector3(0, 0, transform.GetChild(0).GetComponent<Renderer>().bounds.size.z * 5);
            gameObject.GetComponent<ObstaclesSpawner>().RespawnObstacles();
        }
    }
}
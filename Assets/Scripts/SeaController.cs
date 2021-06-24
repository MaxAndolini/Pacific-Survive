using UnityEngine;

public class SeaController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ship")) return;
        transform.position += new Vector3(0, 0, transform.GetChild(0).GetComponent<Renderer>().bounds.size.z * 4);
        gameObject.GetComponent<ObstaclesSpawner>().RespawnObstacles();
        gameObject.GetComponent<LifebuoySpawner>().RespawnLifebuoys();
        gameObject.GetComponent<PeopleSpawner>().RespawnPeople();
    }
}
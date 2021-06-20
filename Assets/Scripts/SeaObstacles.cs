using System.Collections.Generic;
using UnityEngine;

public class SeaObstacles : MonoBehaviour
{
    public List<Obstacle> obstacles;
    private readonly List<GameObject> obs = new List<GameObject>();

    private void Start()
    {
        Spawn();
    }

    private void SpawnObstacle()
    {
        var obsRandom = Random.Range(0, obstacles.Count);
        Debug.Log(obstacles.Count);
        var posRandom = Random.Range(0, 3);
        var seaBound = transform.position.z + transform.GetChild(0).localScale.x - 25f;
        var posRandomZ = Random.Range(transform.position.z - 25f, seaBound);
        Debug.Log(posRandomZ);
        var temp = Instantiate(obstacles[obsRandom].O);
        temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, posRandomZ);
        obs.Add(temp);
    }

    public void RespawnObstacles()
    {
        for (var i = 0; i < obs.Count; i++) Destroy(obs[i]);
        obs.Clear();
        Spawn();
    }

    private void Spawn()
    {
        var range = Random.Range(30, 50);
        for (var i = 0; i < range; i++) SpawnObstacle();
    }
}
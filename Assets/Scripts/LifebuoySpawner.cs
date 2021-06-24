using System.Collections.Generic;
using UnityEngine;

public class LifebuoySpawner : MonoBehaviour
{
    public bool first;
    public GameObject lifebuoy;
    private readonly List<GameObject> _lb = new List<GameObject>();
    private float _startPos = -10f;

    private void Start()
    {
        Spawn();
    }

    private void SpawnLifebuoy()
    {
        var maxObject = Random.Range(0, 3);
        for (var i = 0; i < maxObject; i++)
        {
            var randX = Random.Range(0, 3);
            float posX = 0;
            var randZ = Random.Range(0, 11);
            var posZ = _startPos + randZ * 13;
            posX = randX switch
            {
                0 => GameManager.Instance.left,
                1 => 0,
                2 => GameManager.Instance.right,
                _ => posX
            };

            var temp = Instantiate(lifebuoy);
            temp.transform.position = new Vector3(posX, lifebuoy.transform.position.y, transform.position.z + posZ);
            _lb.Add(temp);
        }
    }

    public void RespawnLifebuoys()
    {
        foreach (var l in _lb)
            Destroy(l);

        _lb.Clear();
        Spawn();
    }

    private void Spawn()
    {
        if (first)
        {
            _startPos = 29f;
            first = false;
        }
        else
        {
            _startPos = -10f;
        }

        SpawnLifebuoy();
    }
}
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public bool first;
    public GameObject people;
    private readonly List<GameObject> _pp = new List<GameObject>();
    private float _startPos = -10f;

    private void Start()
    {
        Spawn();
    }

    private void SpawnPeople()
    {
        var maxObject = Random.Range(5, 25);
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

            var temp = Instantiate(people);
            temp.transform.position = new Vector3(posX, people.transform.position.y, transform.position.z + posZ);
            _pp.Add(temp);
        }
    }

    public void RespawnPeople()
    {
        foreach (var p in _pp)
            Destroy(p);

        _pp.Clear();
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

        SpawnPeople();
    }
}
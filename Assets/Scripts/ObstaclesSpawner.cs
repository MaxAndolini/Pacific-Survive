using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public bool first;
    public List<Obstacle> obstacles;
    private readonly List<GameObject> _obs = new List<GameObject>();
    private int _lastObject = -1;
    private float _startPos = -10f;

    private void Start()
    {
        Spawn();
    }

    private void SpawnObstacle()
    {
        GameObject obsObj;

        if (_lastObject == -1)
        {
            var obsObjRandom = Random.Range(0, obstacles.Count);
            obsObj = obstacles[obsObjRandom].ObsObject;
            _lastObject = obsObjRandom;
        }
        else
        {
            obsObj = GiveObject();
        }

        if (obsObj != null)
        {
            var emptyRandom = Random.Range(0, 100);
            if (emptyRandom > 30)
            {
                var position = obsObj.transform.position;
                var temp = Instantiate(obsObj,
                    new Vector3(position.x, position.y,
                        transform.position.z + _startPos), Quaternion.identity);
                _obs.Add(temp);
            }
            else
            {
                _lastObject = -1;
            }
        }
        else
        {
            _lastObject = -1;
        }

        if (_startPos < 120f)
        {
            _startPos += 10f;
            SpawnObstacle();
        }
    }

    public void RespawnObstacles()
    {
        foreach (var o in _obs)
            Destroy(o);

        _obs.Clear();
        _startPos = -10f;
        Spawn();
    }

    private void Spawn()
    {
        if (first)
        {
            _startPos = 30f;
            first = false;
        }
        else
        {
            _startPos = -20f;
        }

        SpawnObstacle();
    }

    private int[] detection(int side, int[] length)
    {
        int[] detect = { };
        if (side == 0 && length.Length == 1) // 0 - left and 1 - length
        {
            // 0 - left, 1 - middle, 2 - right, 0, 1 - left and middle
            var newObs = Random.Range(0, 4);
            detect = newObs == 3 ? new[] {0, 1} : new[] {newObs};
        }
        else if (side == 1 && length.Length == 1) // 1 - middle and 1 - length
        {
            // empty
        }
        else if (side == 2 && length.Length == 1) // 2 - right and 1 - length
        {
            // 0 - left, 1 - middle, 2 - right, 1, 2 - middle and right
            var newObs = Random.Range(0, 4);
            detect = newObs == 3 ? new[] {1, 2} : new[] {newObs};
        }
        else if (side == 0 && length.Length == 2) // 0 - left and 2 - length
        {
            // 0 - left, 1 - middle, 0, 1 - left and middle
            var newObs = Random.Range(0, 3);
            detect = newObs switch
            {
                2 => new[] {0, 1},
                _ => new[] {newObs}
            };
        }
        else if (side == 2 && length.Length == 2) // 2 - right and 2 - length
        {
            // 1 - middle, 2 - right, 1, 2 - middle and right
            var newObs = Random.Range(1, 4);
            detect = newObs switch
            {
                3 => new[] {1, 2},
                _ => new[] {newObs}
            };
        }

        return detect;
    }

    private GameObject GiveObject()
    {
        GameObject obj = null;
        var nextObjArray = detection(obstacles[_lastObject].Side, obstacles[_lastObject].Length); // next object
        if (nextObjArray.Length <= 0) return null;
        var count = obstacles.Count(t => t.Length.SequenceEqual(nextObjArray));
        var randomObj = Random.Range(0, count);
        count = 0;
        for (var i = 0; i < obstacles.Count; i++)
            if (obstacles[i].Length.SequenceEqual(nextObjArray))
            {
                if (count == randomObj)
                {
                    obj = obstacles[i].ObsObject;
                    _lastObject = i;
                    break;
                }

                count++;
            }

        return obj;
    }
}
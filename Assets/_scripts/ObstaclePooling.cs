using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooling : Singleton<ObstaclePooling>
{
    [SerializeField] GameObject obstaclePrefab;
    // [SerializeField] GameObject coinGroupPrefab;
    [SerializeField] int poolSize = 10;
    
    Queue<GameObject> obstaclePool = new Queue<GameObject>();
    // Queue<GameObject> coinPool = new Queue<GameObject>();

    void Start()
    {
        // spawn first obstacles
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            obj.SetActive(false);
            obstaclePool.Enqueue(obj);
        }

        // coins prefabs
        // for (int i = 0; i < poolSize; i++)
        // {
        //     GameObject obj = Instantiate(coinGroupPrefab);
        //     obj.SetActive(false);
        //     coinPool.Enqueue(obj);
        // }
    }

    public GameObject GetObstacle()
    {
        if (obstaclePool.Count > 0)
        {
            GameObject obj = obstaclePool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        
        return Instantiate(obstaclePrefab);
    }

    public void ReturnObstacle(GameObject obj)
    {
        obj.SetActive(false);
        obstaclePool.Enqueue(obj);
    }

    // public GameObject GetCoinGroup()
    // {
    //     if (coinPool.Count > 0)
    //     {
    //         GameObject obj = coinPool.Dequeue();
    //         obj.SetActive(true);
    //         return obj;
    //     }

    //     return Instantiate(coinGroupPrefab);
    // }

    // public void ReturnCoinGroup(GameObject obj)
    // {
    //     obj.SetActive(false);
    //     coinPool.Enqueue(obj);
    // }
}

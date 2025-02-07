using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    ScoreManager scoreManager;
    ObstacleFactory obstacleFactory;

    [Header("Obstacle Spawn Attributes")]
    [SerializeField] ObstaclePooling obstaclePool;
    [SerializeField] int minGap = 5;
    [SerializeField] int maxGap = 10;
    [SerializeField] float spawnOffsetDistance = 20f;

    // first spawn
    [SerializeField] float nextSpawnDistance = 5f;

    void Start()
    {
        // flyweight
        scoreManager = ScoreManager.Instance;
        obstacleFactory = ObstacleFactory.Instance;
    }

    void Update()
    {
        if (scoreManager.Meters >= nextSpawnDistance)
        {
            SpawnElements();
            nextSpawnDistance += Random.Range(minGap, maxGap);
        }
    }

    void SpawnElements()
    {
        // pick random lane
        int obstacleLane = Random.Range(-1, 1);
        Vector3 lanePos = LanesManager.Instance.GetLanePosition(obstacleLane);
        // obstacle spawn
        GameObject obstacle = obstaclePool.GetObstacle();
        
        // remove 'gfx' child object from old obstacle
        if (obstacle.transform.Find("Gfx") != null)
        {
            // bug: the next get random will fail because the previous one
            // was destroyed
            // TODO: use object pooling instead destroy
            Destroy(obstacle.transform.Find("Gfx"));
        }
        // generate new random gfx
        GameObject newGfx = obstacleFactory.GetRandomObstacle("Gravestone");
        newGfx.name = "Gfx";
        newGfx.transform.SetParent(obstacle.transform);
        newGfx.transform.localPosition = Vector3.zero;

        // calculate obstacle position
        obstacle.transform.position = lanePos + Vector3.forward * spawnOffsetDistance;

        // TODO: logic to spawn coins
        // int coinLane = LanesManager.Instance.GetRandomLane(obstacleLane);
        // GameObject coinGroup = obstaclePool.GetCoinGroup();
        // coinGroup.transform.position = new Vector3(coinLane, 0f, spawnOffsetDistance);
    }
}

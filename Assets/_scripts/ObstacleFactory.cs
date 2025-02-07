using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : Singleton<ObstacleFactory>
{
    [System.Serializable]
    public class ObstacleObjectCreation {
        public string obstacleName;
        public GameObject[] obstaclePrefabs;
    }
    [SerializeField] ObstacleObjectCreation[] obstacleCreations;

    Dictionary<string, GameObject[]> obstacles = new();

    // Start is called before the first frame update
    void Start()
    {
        // iterate through all obstacle creations
        foreach(ObstacleObjectCreation creation in obstacleCreations)
        {
            // auxiliar array for obstacles prefabs
            GameObject[] auxiliarObstacles = new GameObject[creation.obstaclePrefabs.Length];
            // auxiliar counter
            int count = 0;
            // iterate through all prefabs
            foreach(GameObject obstacleGO in creation.obstaclePrefabs)
            {
                // create a new game object
                GameObject newObstacleGO = Instantiate(obstacleGO);
                // clean hierarchy
                newObstacleGO.transform.SetParent(this.transform);
                // add new obstacle to auxiliar obstacles
                auxiliarObstacles[count] = newObstacleGO;
                // update counter
                count++;
                // hide gameobject
                newObstacleGO.SetActive(false);
            }
            // set the auxiliar to the obstacles control variable
            obstacles[creation.obstacleName] = auxiliarObstacles;
        }
    }

    public GameObject GetRandomObstacle(string obstacleName)
    {
        int rand = Random.Range(0, obstacles[obstacleName].Length-1);
        GameObject obstacleGO = obstacles[obstacleName][rand];
        obstacleGO.SetActive(true);
        return obstacleGO;
    }
}

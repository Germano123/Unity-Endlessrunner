using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    [SerializeField] private ObstaclePooling obstaclePool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            obstaclePool.ReturnObstacle(other.gameObject);
        }
        // else if (other.CompareTag("Coin"))
        // {
        //     obstaclePool.ReturnCoinGroup(other.gameObject);
        // }
    }
}

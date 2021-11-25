using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class EnemyManager : NetworkBehaviour
{
    PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    void Spawn ()
    {
        if(playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        if(playerHealth != null)
        {
            if (playerHealth.currentHealth <= 0f)
            {
                return;
            }

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            GameObject g = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            NetworkServer.Spawn(g);
        }
    }
}

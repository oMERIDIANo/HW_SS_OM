using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    List<GameObject> players;
    int random;
    public int size;

    void Awake ()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        size = players.Count;
        random = Random.Range(0, size);
        playerHealth = players[random].GetComponent<PlayerHealth>();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();       
    }


    void Update ()
    {
        if(size > 0)
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                nav.SetDestination(players[random].transform.position);
            }

            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth <= 0 && size > 1)
            {
                players.RemoveAt(random);
                size = players.Count;
                random = Random.Range(0, size);

                nav.SetDestination(players[random].transform.position);
            }
        }
    }
}

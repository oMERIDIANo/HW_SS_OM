using UnityEngine;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour
{
	public float restartDelay = 5f;
    Animator anim;
	float restartTimer;

    public int playersActive;
    public int playersDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (playersActive == 0)
        {
            return;
        }

        if(playersActive == playersDead)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Restarting Game");

        anim.SetTrigger("GameOver");

        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

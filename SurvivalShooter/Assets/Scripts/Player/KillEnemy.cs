using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KillEnemy : NetworkBehaviour
{

    void Awake()
    {

    }
    public void killEnemy(GameObject enemy)
    {
        CmdKillEnemy(enemy);
    }

    [Command]
    void CmdKillEnemy(GameObject e)
    {
        NetworkServer.Destroy(e);
    }
}

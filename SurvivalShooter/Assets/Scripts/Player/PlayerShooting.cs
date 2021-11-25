using UnityEngine;
using Mirror;

public class PlayerShooting : NetworkBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    float timer;
    float effectsDisplayTime = 0.2f;

    GunFiring gunFiring;

    void Awake ()
    {
        gunFiring = FindObjectOfType<GunFiring>();
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if(!isLocalPlayer)
        {
            return;
        }

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunFiring.StopShooting();
    }

    void Shoot()
    {
        timer = 0f;

        gunFiring.Shoot();
    }
}


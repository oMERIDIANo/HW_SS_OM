using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GunFiring : NetworkBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    int playerindex;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    GameObject gunBarrel;

    GunFiring gunFiring;

    // Start is called before the first frame update
    void Awake()
    {
        gunBarrel = transform.GetChild(1).gameObject;
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = gunBarrel.GetComponent<ParticleSystem>();
        gunLine = gunBarrel.GetComponent<LineRenderer>();
        gunLight = gunBarrel.GetComponent<Light>();
        gunFiring = GetComponent<GunFiring>();
        gunAudio = gunBarrel.GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        CmdShooting();
    }

    public void StopShooting()
    {
        CmdStopShooting();
    }

    [Command]
    void CmdStopShooting()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    [Command]
    void CmdShooting()
    {
        gunAudio.Play();

        gunParticles.Stop();
        gunParticles.Play();

        gunLight.enabled = true;

        gunLine.enabled = true;
        gunLine.SetPosition(0, gunBarrel.transform.position);

        shootRay.origin = gunBarrel.transform.position;
        shootRay.direction = gunBarrel.transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}

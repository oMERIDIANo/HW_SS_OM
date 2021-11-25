using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    public int startingHealth = 100;

    public int currentHealth;

    Slider healthSlider;
    Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    GameOverManager gameOverManager;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    public bool isDead = false;
    bool damaged;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
        damageImage = GameObject.Find("DamageImage").GetComponent<Image>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        gameOverManager = FindObjectOfType<GameOverManager>();
        gameOverManager.playersActive += 1;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
    
    public void TakeDamage (int amount)
    {
        if(!isLocalPlayer)
        {
            return;
        }

        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            gameOverManager.playersDead -= 1;
            CmdDeath ();
        }
    }

    [Command]
    void CmdDeath ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel ()
    {
        //SceneManager.LoadScene(0);
    }
}

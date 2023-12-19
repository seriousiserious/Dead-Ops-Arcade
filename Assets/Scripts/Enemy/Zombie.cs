using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float strength;
    [SerializeField] private float speed;
    [SerializeField] private Material faded;
    private Animator anim;
    private ParticleSystem blood;
    private bool isPaused;
    public AudioSource death, scream;


    void Awake()
    {
        anim = GetComponent<Animator>();
        blood = GetComponent<ParticleSystem>();
        blood.collision.SetPlane(0, GameObject.FindGameObjectWithTag("Plane").transform);
        scream = gameObject.AddComponent<AudioSource>();
        isPaused = false;
    }

    private void Start()
    {
        SoundManager.instance.StartCoroutine("PlayZombieScream", scream);
    }

    void Update()
    {
        if (health <= 0)
        {
            anim.Play("Die");
            death = gameObject.AddComponent<AudioSource>();
            death.volume = SaveGame.GetSoundVolume();
            SoundManager.instance.PlayZombieDeath(death);
            ScoreManager.instance.UpdateMulti(true); // Whenever a zombie dies the multiplier increases.
            Unalive();
            this.enabled = false;
            Invoke("Despawn", 10);
        }

        if(Time.timeScale == 0)
        {
            scream.Pause();
            isPaused = true;
        }
        else
        {
            if (isPaused)
            {
                scream.Play();
                isPaused = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;
        blood.Play();
    }

    public void Unalive()
    {
        GetComponent<ZombieMovement>().enabled = false;
        GetComponent<ZombieHit>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        tag = "Untagged";
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        SkinnedMeshRenderer[] rends = GetComponentsInChildren<SkinnedMeshRenderer>();
        Color objectColor = rends[0].material.color; // Realistically we only need one that will be applied to all the other renderers.
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material = faded;
            rends[i].shadowCastingMode = ShadowCastingMode.Off;
        }

        while (objectColor.a > 0)
        {
            float fadeAmount = objectColor.a - (2F * Time.deltaTime);
            for (int i = 0; i < rends.Length; i++)
            {
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                rends[i].material.color = objectColor;
            }
            yield return new WaitForSeconds(0.2F);
        }
    }

    public void Despawn()
    {
        EnemyManager.instance.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public void OnParticleCollision(GameObject other)
    {
        var x = (int)other.gameObject.GetComponentInParent<MainWeapon>().myWeapon; // Get the damage value right from the main weapon class and the myweapon type.
        TakeDamage(x);
        ScoreManager.instance.UpdateScore(10);
    }

    public void SetHealth(float h)
    {
        health = h;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetStrength(float st)
    {
        strength = st;
    }

    public float GetStrength()
    {
        return strength;
    }

    public void SetSpeed(float sp)
    {
        speed = sp;
    }

    public float GetSpeed()
    {
        return speed;
    }
}

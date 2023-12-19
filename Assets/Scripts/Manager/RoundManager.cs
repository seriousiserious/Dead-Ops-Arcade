using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    [SerializeField] private int round;
    [SerializeField] private float timer;
    [SerializeField] private float timeRemaining;
    [SerializeField] private List<GameObject> Bonuses;
    PlayerShoot player;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey("EVERLAUNCHED"))
            PlayerPrefs.SetInt("EVERLAUNCHED", 1);
        round = 1;
        timer = 30;
        timeRemaining = timer;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
    }

    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            EnemyManager.instance.StopAllCoroutines();
            if(GameObject.FindWithTag("Zombie") == null)
            {
                ScreenManager.instance.StopAllCoroutines(); // To prevent an annoying bug with the end of the round message during/right after bonus rounds.
                ScreenManager.instance.StartCoroutine("ScreenMessage", 1);
                round++;
           
                timeRemaining = timer * round * 0.5F;
                if (round - 1 % 5 > 0) // The value is normally set on false, however in some occurrences of bonus rounds it might be set on true, so this ensures that the value is set back to false when needed.
                {
                    EnemyManager.instance.SetGetter(false);
                    ScreenManager.instance.ResetShield();
                    if (player.multiWeapons == 3)
                        ScreenManager.instance.ResetAlienGun();
                }
                if (round % 5 > 0) // Zombies will spawn only during rounds that aren't bonus rounds.
                    EnemyManager.instance.StartCoroutine("SpawnEnemy");
            }
        }

        if(round % 5 == 0)
        {
            foreach(GameObject bonus in Bonuses)
            {
                bonus.SetActive(true); 
            }
        }
        else
        {
            foreach (GameObject bonus in Bonuses)
            {
                bonus.SetActive(false);
            }
        }
    }

    public float GetRemainingTime()
    {
        return timeRemaining;
    }

    public void SetRemainingTime(float t)
    {
        timeRemaining = t;
    }

    public int GetCurrentRound()
    {
        return round;
    }

    public void EditList()
    {
        foreach(GameObject bonus in Bonuses.ToArray())
        {
            if (bonus.transform.parent.CompareTag("RifleBonus"))
            {
                Destroy(bonus.transform.parent.gameObject, 2F); // This upgrade can be purchased only once, therefore we'll get rid of it once it is purchased.
                bonus.SetActive(false);
                Bonuses.Remove(bonus);
            }
            
            if (bonus.transform.parent.CompareTag("AlienGunBonus"))
                bonus.transform.parent.GetComponent<Rising>().enabled = true; // Once the rifle is purchased and obviously no longer available, a new temporary bonus will take its place.
        }
    }
}

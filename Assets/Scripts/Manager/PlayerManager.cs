using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] private float health;

    void Awake()
    {
        instance = this;
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;
        if(GameObject.FindWithTag("Zombie").GetComponent<Zombie>().GetStrength() > 0)
           GetComponent<AudioSource>().Play(); // We only play this if the player actually takes damage.
        ScreenManager.instance.RegulateBar(health);
        if(health <= 0)
        {
            ScreenManager.instance.StartCoroutine("ScreenMessage", 0);
            SoundManager.instance.StopAllCoroutines();
            LeaderBoard.UpdateLeaderboard(ScoreManager.instance.GetScore());
            SoundManager.instance.end.Play();
            Time.timeScale = 0;
        }
    }

    public void SetHealth(float h)
    {
        health = h;
        ScreenManager.instance.RegulateBar(health);
    }

    public float GetHealth()
    {
        return health;
    }
}

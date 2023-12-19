using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    [SerializeField] Text roundOver, bonusTxt, score, multiplier;
    [SerializeField] Slider fill;
    [SerializeField] Image fillBar;
    [SerializeField] Gradient barColor;
    [SerializeField] RawImage pistol, rifle, shield, raygun;
    [SerializeField] Canvas UI;
    [SerializeField] GameObject gameOverButtons;

    UnityEvent m_MyEvent = new UnityEvent();
    GameObject player;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown("x") && m_MyEvent != null)
                m_MyEvent.Invoke();
        }
    }

    public void RegulateBar(float health)
    {
        fill.value = health;
        fillBar.color = barColor.Evaluate(fill.normalizedValue);
    }

    public void ScoreCount(int s)
    {
        score.text = s.ToString();
    }

    public void MultiplierCount(int m)
    {
        multiplier.text = "x" + m.ToString();
    }

    public void SwitchWeapon(int x)
    {
        if(x == 1)
        {
            pistol.enabled = true;
            rifle.enabled = false;
        }
        else if(x == 2)
        {
            rifle.enabled = true;
            pistol.enabled = false;
        }
        else
        {
            raygun.enabled = true;
        }
    }

    IEnumerator ScreenMessage(int x) // It is actually called in the RoundManager script. 
    {
        roundOver.color = new Color(176F, 0F, 0F, 0F);
        if (x == 1)
        {
            if(RoundManager.instance.GetCurrentRound() % 5 == 0)
               roundOver.text = "Round " + RoundManager.instance.GetCurrentRound() + " is Complete (Bonus Round)";
            else roundOver.text = "Round " + RoundManager.instance.GetCurrentRound() + " is Complete";
            while (roundOver.color.a < 1)
            {
                float fadeAmount = roundOver.color.a + (2F * Time.deltaTime);
                roundOver.color = new Color(roundOver.color.r, roundOver.color.g, roundOver.color.b, fadeAmount);
                yield return new WaitForSeconds(0.1F);
            }
            yield return new WaitForSeconds(5F);
            while (roundOver.color.a > 0)
            {
                float fadeAmount = roundOver.color.a - (2F * Time.deltaTime);
                roundOver.color = new Color(roundOver.color.r, roundOver.color.g, roundOver.color.b, fadeAmount);
                yield return new WaitForSeconds(0.1F);
            }
        }
        else if(x == 0)
        {
            roundOver.text = "Game Over";
            roundOver.color = new Color(roundOver.color.r, roundOver.color.g, roundOver.color.b, 1F);
            UI.GetComponent<Image>().enabled = true;
            gameOverButtons.SetActive(true);
        }
    }

    public void BonusMessage(bool x, int y)
    {
        bonusTxt.color = new Color(255F, 255F, 255F, 1F);
        int points = ScoreManager.instance.GetScore();

        if (x == true)
        {
            if (y == 1)
            {
                bonusTxt.text = "Press X to purchase Rifle (40000 PTS)";
                if(points >= 40000) // Adding listener only IF the player has enough points. Same will be done for the other two.
                   m_MyEvent.AddListener(delegate { Copping(points, 40000, y); });
            }
            else if (y == 2)
            {
                bonusTxt.text = "Press X to purchase Shield (20000 PTS)";
                if(points >= 20000)
                   m_MyEvent.AddListener(delegate { Copping(points, 20000, y); });
            }
            else if (y == 3)
            {
                bonusTxt.text = "Press X to purchase Health (10000 PTS)";
                if(points >= 10000 && PlayerManager.instance.GetHealth() < 100) // Won't be able to buy if the player's health is already at its max. 
                   m_MyEvent.AddListener(delegate { Copping(points, 10000, y); });
            }
            else
            {
                bonusTxt.text = "Press X to purchase Raygun (100000 PTS)";
                if (points >= 100000)
                    m_MyEvent.AddListener(delegate { Copping(points, 100000, y); });
            }
        }
        else
        {
            bonusTxt.text = "";
            m_MyEvent.RemoveAllListeners();
        }
    }

    public void Copping(int s, int p, int y)
    {
        ScoreManager.instance.SetScore(s - p);
        if (y == 1)
        {
            player.GetComponent<PlayerShoot>().SetMultiWeapon(2);
            RoundManager.instance.EditList();
        }
        else if (y == 2)
            Invoke("SetShield", 0.1F);
        else if (y == 3)
            PlayerManager.instance.SetHealth(100);
        else
            Invoke("SetAlienGun", 0.1F);

        SoundManager.instance.bonus.Play();
        RoundManager.instance.SetRemainingTime(0); 
    }

    public void ResetShield()
    {
        shield.enabled = false;
    }

    public void SetShield()
    {
        EnemyManager.instance.SetGetter(true);
        shield.enabled = true;
    }

    public void ResetAlienGun()
    {
        raygun.enabled = false;
        pistol.gameObject.SetActive(true);
        rifle.gameObject.SetActive(true);
        player.GetComponent<PlayerShoot>().ResetWeapons();
    }

    public void SetAlienGun()
    {
        player.GetComponent<PlayerShoot>().SetMultiWeapon(3);
        pistol.gameObject.SetActive(false);
        rifle.gameObject.SetActive(false);
    }
}

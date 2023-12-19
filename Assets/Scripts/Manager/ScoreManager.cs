using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private int score, multiplier;

    void Awake()
    {
        instance = this;
        score = 0;
        multiplier = 1;
    }

    public void UpdateScore(int points)
    {
        score = score + (points * multiplier);
        ScreenManager.instance.ScoreCount(score);
    }

    public void UpdateMulti(bool x)
    {
        if (x == true)
            multiplier++;
        else multiplier = 1;
        ScreenManager.instance.MultiplierCount(multiplier);
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int s)
    {
        score = s;
        ScreenManager.instance.ScoreCount(score);
    }
}

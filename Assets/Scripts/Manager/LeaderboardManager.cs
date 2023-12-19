using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;
    [SerializeField] Text first, second, third, fourth, fifth;

    void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("0LBSCORE"))
            first.text = "1. " + SaveGame.GetLbUsername(0) + " " + SaveGame.GetLbScore(0);
        else first.text = "1. No one yet";

        if (PlayerPrefs.HasKey("1LBSCORE"))
            second.text = "2. " + SaveGame.GetLbUsername(1) + " " + SaveGame.GetLbScore(1);
        else second.text = "2. No one yet";

        if (PlayerPrefs.HasKey("2LBSCORE"))
            third.text = "3. " + SaveGame.GetLbUsername(2) + " " + SaveGame.GetLbScore(2);
        else third.text = "3. No one yet";

        if (PlayerPrefs.HasKey("3LBSCORE"))
            fourth.text = "4. " + SaveGame.GetLbUsername(3) + " " + SaveGame.GetLbScore(3);
        else fourth.text = "4. No one yet";

        if (PlayerPrefs.HasKey("4LBSCORE"))
            fifth.text = "5. " + SaveGame.GetLbUsername(4) + " " + SaveGame.GetLbScore(4);
        else fifth.text = "5. No one yet";
    }
}

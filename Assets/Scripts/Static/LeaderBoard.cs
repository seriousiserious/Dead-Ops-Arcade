using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard
{  
    public static void UpdateLeaderboard(int score)
    {
        int newScore = score;
        string newUsername = PlayerPrefs.GetString("USERNAME");
        int oldScore;
        string oldUsername;
        
        for(int i = 0; i < 5; i++)
        {
            if(PlayerPrefs.HasKey(i+"LBSCORE"))
            {
                if(SaveGame.GetLbScore(i) < newScore)
                {
                    oldScore = SaveGame.GetLbScore(i);
                    oldUsername = SaveGame.GetLbUsername(i);
                    SaveGame.SetLbScore(i, newScore);
                    SaveGame.SetLbUsername(i, newUsername);
                    newScore = oldScore;
                    newUsername = oldUsername;
                }
            }
            else
            {
                SaveGame.SetLbScore(i, newScore);
                SaveGame.SetLbUsername(i, newUsername);
                newScore = 0;
                newUsername = "";
            }
        }
    }
}

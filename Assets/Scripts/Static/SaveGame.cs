using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveGame
{
    public static void SetUsername(string username)
    {
        PlayerPrefs.SetString("USERNAME", username);
        PlayerPrefs.Save();
    }

    public static string GetUsername()
    {
        return PlayerPrefs.GetString("USERNAME");
    }

    public static void SetMusicVolume(float v)
    {
        PlayerPrefs.SetFloat("MUSIC", v);
        PlayerPrefs.Save();
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MUSIC");
    }

    public static void SetSoundVolume(float v)
    {
        PlayerPrefs.SetFloat("SOUND", v);
        PlayerPrefs.Save();
    }

    public static float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("SOUND");
    }

    public static void SetUp(string up)
    {
        PlayerPrefs.SetString("UP", up);
        PlayerPrefs.Save();
    }

    public static string GetUp()
    {
        return PlayerPrefs.GetString("UP");
    }

    public static void SetDown(string down)
    {
        PlayerPrefs.SetString("DOWN", down);
        PlayerPrefs.Save();
    }

    public static string GetDown()
    {
        return PlayerPrefs.GetString("DOWN");
    }

    public static void SetLeft(string left)
    {
        PlayerPrefs.SetString("LEFT", left);
        PlayerPrefs.Save();
    }

    public static string GetLeft()
    {
        return PlayerPrefs.GetString("LEFT");
    }

    public static void SetRight(string right)
    {
        PlayerPrefs.SetString("RIGHT", right);
        PlayerPrefs.Save();
    }

    public static string GetRight()
    {
        return PlayerPrefs.GetString("RIGHT");
    }

    public static void SetShoot(int s)
    {
        PlayerPrefs.SetInt("SHOOT", s);
        PlayerPrefs.Save();
    }

    public static int GetShoot()
    {
        return PlayerPrefs.GetInt("SHOOT");
    }

    public static void SetControls(int index)
    {
        PlayerPrefs.SetInt("CONTROLS", index);
        PlayerPrefs.Save();
    }

    public static int GetControls()
    {
        return PlayerPrefs.GetInt("CONTROLS");
    }

    public static void SetLbScore(int i, int s)
    {
        PlayerPrefs.SetInt(i + "LBSCORE", s);
        PlayerPrefs.Save();
    }

    public static int GetLbScore(int i)
    {
        return PlayerPrefs.GetInt(i + "LBSCORE");
    }

    public static void SetLbUsername(int i, string u)
    {
        PlayerPrefs.SetString(i + "LBUSERNAME", u);
        PlayerPrefs.Save();
    }

    public static string GetLbUsername(int i)
    {
        return PlayerPrefs.GetString(i + "LBUSERNAME");
    }

    public static void SetDifficulty(int d)
    {
        PlayerPrefs.SetInt("MODE", d);
        PlayerPrefs.Save();
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt("MODE");
    }
}

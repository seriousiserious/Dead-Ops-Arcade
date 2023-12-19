using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject partOne, partTwo, partThree, pause;
    [SerializeField] private Button next, prev, skip, back;
    [SerializeField] private Text x, up, down, left, right, shoot;
    [SerializeField] private AudioClip tap;
    [SerializeField] private Slider sound, music;
    [SerializeField] private Dropdown controls;
    [SerializeField] private InputField username;
    private AudioSource button;

    public void Start()
    {
        button = gameObject.AddComponent<AudioSource>();
        button.clip = tap;

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (PlayerPrefs.HasKey("EVERLAUNCHED"))
            {
                music.value = SaveGame.GetMusicVolume();
                sound.value = SaveGame.GetSoundVolume();
                controls.value = SaveGame.GetControls();
            }
            else
            {
                SaveGame.SetMusicVolume(0.1F);
                SaveGame.SetSoundVolume(0.1F);
            }

            if (controls.value == 0)
            {
                up.text = "W";
                SaveGame.SetUp("w");
                down.text = "S";
                SaveGame.SetDown("s");
                left.text = "A";
                SaveGame.SetLeft("a");
                right.text = "D";
                SaveGame.SetRight("d");
                shoot.text = "Left Click";
                SaveGame.SetShoot(0);
            }
            else
            {
                up.text = "Up Arrow";
                SaveGame.SetUp("up");
                down.text = "Down Arrow";
                SaveGame.SetDown("down");
                left.text = "Left Arrow";
                SaveGame.SetLeft("left");
                right.text = "Right Arrow";
                SaveGame.SetRight("right");
                shoot.text = "Right Click";
                SaveGame.SetShoot(1);
            }
        }

        if (SceneManager.GetActiveScene().name == "Tutorial") // This will be used to switch the skip/start button with a button to return to the menu.
        {
            if (PlayerPrefs.HasKey("EVERLAUNCHED"))
                back.gameObject.SetActive(true);
        }
    }

    public void Next()
    {
        if(partOne.activeInHierarchy)
        {
            partOne.SetActive(false);
            partTwo.SetActive(true);
            prev.gameObject.SetActive(true);
        }
        else
        {
            partTwo.SetActive(false);
            partThree.SetActive(true);
            next.gameObject.SetActive(false);
            skip.GetComponentInChildren<Text>().text = "Start";
        }
    }

    public void Prev()
    {
        if(partTwo.activeInHierarchy)
        {
            partTwo.SetActive(false);
            partOne.SetActive(true);
            prev.gameObject.SetActive(false);
        }
        else
        {
            partThree.SetActive(false);
            partTwo.SetActive(true);
            next.gameObject.SetActive(true);
            skip.GetComponentInChildren<Text>().text = "Skip";
        }
    }

    public void Exit()
    {
        x.text = "Are you sure? Your score will not be saved";
    }

    public void No()
    {
        x.text = "Pause";
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.soundTrack.Play();
    }

    public void LdGame()
    {
        StartCoroutine("LoadGame");
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSecondsRealtime(tap.length);
        SceneManager.LoadScene("PreLevel");
        yield return null;
    }

    public void LdTutorial()
    {
        StartCoroutine("LoadTutorial");
    }

    IEnumerator LoadTutorial()
    {
        yield return new WaitForSecondsRealtime(tap.length);
        SceneManager.LoadScene("Tutorial");
        yield return null;
    }

    public void LdMenu()
    {
        StartCoroutine("LoadMenu");
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSecondsRealtime(tap.length);
        if (SceneManager.GetActiveScene().name == "Level")
            Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        yield return null;
    }

    public void NewGame()
    {
        StartCoroutine("LoadNewGame");
    }

    IEnumerator LoadNewGame()
    {
        yield return new WaitForSecondsRealtime(tap.length);
        SceneManager.LoadScene("Level");
        yield return null;
    }

    public void StrGame() // Slightly different than LoadGame, basically I made this variant because the Level section is accessible from both the Menu and the Tutorial, when it's being loaded from the Menu we want to check if the game has been launched at least once, if it hasn't a tutorial will be loaded first; meanwhile from the Tutorial we don't need to do that check, because supposedly we just went through it.
    {
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(tap.length);
        if (PlayerPrefs.GetInt("EVERLAUNCHED") == 1)
            SceneManager.LoadScene("PreLevel");
        else SceneManager.LoadScene("Tutorial"); // If the game is being launched for the first time, the game will automatically start a brief tutorial.
        yield return null;
    }

    public void LdCredits()
    {
        StartCoroutine("LoadCredits");
    }

    IEnumerator LoadCredits()
    {
        yield return new WaitForSecondsRealtime(tap.length);
        SceneManager.LoadScene("Credits");
        yield return null;
    }

    public void LdLb()
    {
        StartCoroutine("LoadLb");
    }

    public void ExitGm()
    {
        StartCoroutine("ExitGame");
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSecondsRealtime(tap.length);
        Application.Quit();
        yield return null;
    }

    IEnumerator LoadLb()
    {
        yield return new WaitForSecondsRealtime(tap.length);
        SceneManager.LoadScene("Leaderboard");
        yield return null;
    }

    public void PlayButtonClick()
    {
        button.Play();
    }

    public void SaveMusicVolume()
    {
        SaveGame.SetMusicVolume(music.value);
    }

    public void SaveSoundVolume()
    {
        SaveGame.SetSoundVolume(sound.value);
    }

    public void ChangeControls()
    { 
        if(controls.value == 0)
        {
            up.text = "W";
            SaveGame.SetUp("w");
            down.text = "S";
            SaveGame.SetDown("s");
            left.text = "A";
            SaveGame.SetLeft("a");
            right.text = "D";
            SaveGame.SetRight("d");
            shoot.text = "Left Click";
            SaveGame.SetShoot(0);
        }
        else
        {
            up.text = "Up Arrow";
            SaveGame.SetUp("up");
            down.text = "Down Arrow";
            SaveGame.SetDown("down");
            left.text = "Left Arrow";
            SaveGame.SetLeft("left");
            right.text = "Right Arrow";
            SaveGame.SetRight("right");
            shoot.text = "Right Click";
            SaveGame.SetShoot(1);
        }

        SaveGame.SetControls(controls.value);
    }

    public void SubmitUsername()
    {
        if (username.text.Length == 0)
        {
            username.text = "Anon";
            Debug.Log(username.text);
        }
        SaveGame.SetUsername(username.text);
    }

    public void ChooseDifficulty()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "NormalMode")
            SaveGame.SetDifficulty(0);
        else SaveGame.SetDifficulty(1);
    }
}

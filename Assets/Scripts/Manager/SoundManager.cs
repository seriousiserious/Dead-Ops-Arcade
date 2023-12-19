using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource gunShot, end, bonus, soundTrack;
    [SerializeField] AudioClip pistol, ar, raygun, deathScream, bonusPurchase, gameOver, song;
    [SerializeField] AudioClip[] zombieScreams;

    void Awake()
    {
        instance = this;
        soundTrack = gameObject.AddComponent<AudioSource>();
        soundTrack.volume = SaveGame.GetMusicVolume();
        soundTrack.clip = song;
        soundTrack.loop = true;
        soundTrack.Play();

        gunShot = gameObject.AddComponent<AudioSource>();
        gunShot.volume = SaveGame.GetSoundVolume();
        gunShot.loop = true;
        
        end = gameObject.AddComponent<AudioSource>();
        end.volume = SaveGame.GetMusicVolume();
        end.clip = gameOver;
        end.loop = true;

        bonus = gameObject.AddComponent<AudioSource>();
        bonus.volume = SaveGame.GetSoundVolume();
        bonus.clip = bonusPurchase;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            soundTrack.Pause();
            gunShot.Pause();
        }
    }

    public void PlayGunShot(int weapon)
    {
        switch(weapon)
        {
            case 1:
                gunShot.clip = pistol;
                break;
            case 10:
                gunShot.clip = ar;
                break;
            case 100:
                gunShot.clip = raygun;
                gunShot.pitch = 1F;
                break;
        }
        
        if (!gunShot.isPlaying)
            gunShot.Play();
    }

    public void PlayZombieDeath(AudioSource x)
    {
        x.clip = deathScream;
        x.Play();
    }

    IEnumerator PlayZombieScream(AudioSource scream) // It's actually called in the Zombie script.
    {
        while (scream.enabled == true)
        {
            scream.clip = zombieScreams[Random.Range(0, zombieScreams.Length)];
            scream.volume = SaveGame.GetSoundVolume();
            scream.Play();
            yield return new WaitForSeconds(10F);
        }
    }

    public void StopGunShot()
    {
        gunShot.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWeapon : MainWeapon
{
    [SerializeField] private List<GameObject> bulletList;
    [SerializeField] private GameObject bullet;
    // [SerializeField] private int bulletLength; No longer used.
    [SerializeField] public Transform spawnPoint;

    private void Awake()
    {
        /* Initial implementation.
        bulletList = new GameObject[bulletLength];            
        for(int i = 0; i<bulletLength; i++)
        {
            bulletList[i] = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            bulletList[i].GetComponent<HitBullet>().SetBulletType((int)myWeapon);
            bulletList[i].SetActive(false);
        }*/
    }

    private void Update()
    {
        foreach(GameObject b in bulletList.ToArray())
        {
            if (b == null)
                bulletList.Remove(b);
        }
    }

    public void Shoot()
    {
        bullet.GetComponent<HitBullet>().SetBulletType((int)myWeapon);
        bulletList.Add(Instantiate(bullet, spawnPoint.position, spawnPoint.rotation));
        SoundManager.instance.PlayGunShot((int)myWeapon);

        /* Initial implementation. Now changed with literally one line of code and a parameter in the bullet prefab script that automatically destroys it after some time.
        for (int i = 0; i < bulletLength; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].GetComponent<SpawnBullet>().Spawn(spawnPoint);
                Adjust(true, bulletList[i]);
                break;
            }
        }
        
        if (bulletCount == bulletLength)
        {
            for(int i = 0; i<bulletLength; i++)
            {
                if (bulletList[i].activeInHierarchy)
                    Adjust(false, bulletList[i]);
            }
        }*/
    }

    /*public void Adjust(bool x, GameObject y)
    {
        StartCoroutine(Recount(x, y));
    }

    IEnumerator Recount(bool x, GameObject y)
    {
        if(x == true)
        {
            y.SetActive(true);
            bulletCount++;
        }
        else
        {
            y.SetActive(false);
            bulletCount--;
        }

        yield return null; 
    } */
}

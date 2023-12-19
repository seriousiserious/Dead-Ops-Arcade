using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Animator anim;
    private GameObject rifle, pistol, raygun; 
    private Renderer[] rendChildren;
    [SerializeField] public int multiWeapons;
    MeshRenderer p;
    ShootingWeapon pi, rif;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        multiWeapons = 1;
        pistol = GameObject.FindWithTag("Pistol");
        rifle = GameObject.FindWithTag("Rifle");
        raygun = GameObject.FindWithTag("RayGun");
        p = pistol.GetComponent<MeshRenderer>();
        rendChildren = rifle.GetComponentsInChildren<MeshRenderer>();
        SetRifle(false);
        raygun.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        pi = pistol.GetComponent<ShootingWeapon>();
        rif = rifle.GetComponent<ShootingWeapon>();
    }

    void Update()
    {
        if ((Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0) && multiWeapons == 2) // Change weapon with scrollwheel.
        {
            if (!p.enabled)
            {
                p.enabled = true;
                SetRifle(false);
                ScreenManager.instance.SwitchWeapon(1);
            }
            else
            {
                SetRifle(true);
                p.enabled = false;
                ScreenManager.instance.SwitchWeapon(2);
            }
        }
        else if(multiWeapons == 3)
        {
            rifle.SetActive(false);
            pistol.SetActive(false);
            raygun.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = true; // Enabling the raygun renderer so we can effectively use it.
            ScreenManager.instance.SwitchWeapon(3);
        }
        
        if (Input.GetMouseButton(SaveGame.GetShoot()))
        {
            StartCoroutine("Shoot");
        }
        else
        {
            StopCoroutine("Shoot");
            SoundManager.instance.StopGunShot();
            anim.SetBool("ShootRifle", false);
            anim.SetBool("ShootPistol", false);
        }
    }

    IEnumerator Shoot()
    {
        if (!p.enabled && multiWeapons != 3)
        {
            anim.SetBool("ShootRifle", true);
            anim.SetBool("ShootPistol", false);
            yield return new WaitForSeconds(0.15F);
            rif.Shoot();
        }
        else if(p.enabled || multiWeapons == 3) // Works for both the pistol and the raygun.
        {
            anim.SetBool("ShootPistol", true);
            anim.SetBool("ShootRifle", false);
            yield return new WaitForSeconds(0.15F);
            if (multiWeapons == 1 || multiWeapons == 2)
                pi.Shoot();
            else
                raygun.GetComponentInChildren<ParticleSystem>().Shoot();
        }
    }

    public void SetMultiWeapon(int x)
    {
        multiWeapons = x;
    }

    public void ResetWeapons()
    {
        raygun.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        rifle.SetActive(true);
        pistol.SetActive(true);
        multiWeapons = 2;
    }

    public void SetRifle(bool y)
    {
        for (int i = 0; i < rendChildren.Length; i++)
        {
            rendChildren[i].enabled = y;
        }
    }
}

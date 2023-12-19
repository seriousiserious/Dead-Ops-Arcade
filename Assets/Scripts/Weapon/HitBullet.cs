using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    // private GameObject weapon;
    private float ttLive;

    public void Awake()
    {
        if (damage == 1)
            ttLive = 0.3F; // Shorter range for pistol.
        else ttLive = 2;
        Destroy(gameObject, ttLive);

        /*if (damage == 1)
            weapon = GameObject.FindWithTag("Pistol");
        else if (damage == 5)
            weapon = GameObject.FindWithTag("Rifle");*/
    }

    public void SetBulletType(int damageAmount)
    {
        damage = damageAmount;
        /*if (damage == 1) 
            GetComponent<BulletMovement>().SetSpeed(1F);
        else if (damage == 5) 
            GetComponent<BulletMovement>().SetSpeed(5F);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Zombie"))
        {
            other.gameObject.GetComponent<Zombie>().TakeDamage(damage);
            ScoreManager.instance.UpdateScore(1);
            Destroy(gameObject);
            /* weapon.GetComponent<ShootingWeapon>().Adjust(false, gameObject); */
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHit : MonoBehaviour
{
    private Animator anim;

    public void Start()
    {
       anim = gameObject.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        StopAllCoroutines(); // Added this to fix a flaw within the logic behind the collision detection, where the user would keep taking damage (until enemy's deactivation) whenever a zombie was killed near hitting range: the StopAllCoroutines function in ZombieMovement would never be called in that specific instance.
    }

    public void Hit(float strength)
    {
        StartCoroutine(Hitting(strength));
    }

    IEnumerator Hitting(float s)
    {
        while (true)
        {
            anim.SetBool("Hit", true);
            yield return new WaitForSeconds(0.1F);
            PlayerManager.instance.TakeDamage(s);
            ScoreManager.instance.UpdateMulti(false); // Whenever a zombie deals damage to the player, the multiplier gets set back to 1, check the function for reference.
            yield return new WaitForSeconds(1);
            anim.SetBool("Hit", false);
            yield return new WaitForSeconds(1);
        }
    }
}

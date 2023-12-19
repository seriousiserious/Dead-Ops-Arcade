using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class ZombieMovement : MonoBehaviour
{
    private int mv = 0;
    private GameObject aux;
    private Animator anim;
    private float strength;
    
    void Start()
    {
        aux = GameObject.Find("Soldier");
        anim = gameObject.GetComponent<Animator>();
        strength = GetComponent<Zombie>().GetStrength();
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (mv == 0)
            Walk();
        else if (mv == 1)
            Walk(aux);   

        if (RoundManager.instance.GetCurrentRound() >= 5)
            anim.SetBool("Fast", true);
    }

    private void Walk()
    {
        transform.Translate(Vector3.forward * GetComponent<Zombie>().GetSpeed() * Time.deltaTime);
    }

    private void Walk(GameObject target) // Overload, this will be used as soon as the zombie comes out of the portal.
    {
        transform.LookAt(target.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, GetComponent<Zombie>().GetSpeed() * Time.deltaTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mv = 2; // Stay still.
            anim.SetBool("OutOfRange", true);
            GetComponent<ZombieHit>().Hit(strength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mv = 1;
            anim.SetBool("OutOfRange", false);
            GetComponent<ZombieHit>().StopAllCoroutines();
            anim.SetBool("Hit", false);
        }
        else if(other.gameObject.CompareTag("Passage"))
        {
            mv = 1;
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}

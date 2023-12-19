using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    protected float speed = 15F;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        GetComponent<TrailRenderer>().enabled = true;
        /*StartCoroutine("Trailing"); // Trailing is not instant, this coroutine will set the trailing effect to enabled.*/
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    /*IEnumerator Trailing()
    {
        yield return new WaitForSeconds(0.2F);
    }*/
}

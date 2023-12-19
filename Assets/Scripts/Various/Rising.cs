using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rising : MonoBehaviour
{
    private int stopper = 0;
    
    void Update()
    {
        if (RoundManager.instance.GetCurrentRound() % 5 == 0 && stopper == 0 && RoundManager.instance.GetRemainingTime() > 0) // Basically this happens every 5 rounds, it's a bonus round. 
        {
            transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        }
        else if(RoundManager.instance.GetRemainingTime() == 0 && stopper == 1 || RoundManager.instance.GetCurrentRound() % 5 > 0 && stopper == 1)
        {
            transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            RoundManager.instance.SetRemainingTime(20F);
            stopper = 1;
        }
        else if (other.gameObject.CompareTag("LowerPlatform"))
            stopper = 0;
    }
}

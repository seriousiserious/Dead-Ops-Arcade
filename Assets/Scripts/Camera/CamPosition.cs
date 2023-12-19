using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    private GameObject player;
    protected float speed = 3.5f;

    void Awake()
    {
        player = GameObject.Find("Soldier");
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{   
    public void Spawn(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
}

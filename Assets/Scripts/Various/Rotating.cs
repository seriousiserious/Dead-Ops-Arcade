using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        else
        transform.Rotate(0.0F, 5F, 0F, Space.World);
    }
}

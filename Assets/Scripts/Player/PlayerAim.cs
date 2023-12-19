using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point); // Look at the point
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)); // Clamp the x and z rotation
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim2D : MonoBehaviour
{
	private Camera cam;
	private GameObject doublePistol;
	private int setter;
	Image pistol;
	Shoot2D shoota;

	void Start()
	{
		cam = Camera.main;
		doublePistol = GameObject.FindGameObjectWithTag("Pistol");
		doublePistol.GetComponent<Image>().enabled = false;
		doublePistol.GetComponent<Shoot2D>().enabled = false;
		pistol = doublePistol.GetComponent<Image>();
		shoota = doublePistol.GetComponent<Shoot2D>();
		setter = 0;
	}

	void Update()
	{
		var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if ((Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0))
        {
			if (setter == 0)
			{
				pistol.enabled = true;
				shoota.enabled = true;
				setter = 1;
			}
			else
            {
				pistol.GetComponent<Image>().enabled = false;
				shoota.GetComponent<Shoot2D>().enabled = false;
				setter = 0;
			}
		}
	}
}

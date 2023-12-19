using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot2D : MonoBehaviour
{
    [SerializeField] public List<Image> bullets;
    [SerializeField] public Image bullet;

    private void Update()
    {
        foreach(Image b in bullets.ToArray())
        {
            if (b == null)
                bullets.Remove(b);
            else
                b.transform.SetParent(GameObject.FindWithTag("Image").transform);
        }

        if (Input.GetMouseButton(SaveGame.GetShoot()))
            StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        bullets.Add(Instantiate(bullet, gameObject.transform.Find("Barrel").position, gameObject.transform.Find("Barrel").rotation));
        yield return new WaitForSeconds(2F);
    }
}

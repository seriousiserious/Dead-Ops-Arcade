using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Moving2D : MonoBehaviour
{
    protected float speed = 20f;

    public void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(SaveGame.GetLeft()))
                transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);

            else if (Input.GetKey(SaveGame.GetRight()))
                transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);

            else if (Input.GetKey(SaveGame.GetUp()))
                transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);

            else if (Input.GetKey(SaveGame.GetDown()))
                transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
        }
    }
}

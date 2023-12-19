using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    protected float speed = 3.5f;
    protected Animator anim;
    protected GameObject pistol;
    private MeshRenderer pistolRenderer;
    public GameObject pause;
    protected Vector3 lastPos;

    UnityEvent m_MyEvent = new UnityEvent();

    void Awake()
    {
        anim = GetComponent<Animator>();
        pistol = GameObject.FindWithTag("Pistol");
        pistolRenderer = pistol.GetComponent<MeshRenderer>();
        m_MyEvent.AddListener(PauseGame);
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (Input.anyKey)
        {
            if (Input.GetKey(SaveGame.GetLeft()))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            }

            else if (Input.GetKey(SaveGame.GetRight()))
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

            else if (Input.GetKey(SaveGame.GetUp()))
                transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

            else if (Input.GetKey(SaveGame.GetDown()))
                transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

            else if (Input.GetKey("escape"))
            {
                m_MyEvent.Invoke();
                return;
            }

            else
            {
                anim.SetBool("RunRifle", false);
                anim.SetBool("RunPistol", false);
                return;
            }

            if (pistolRenderer.enabled || GetComponent<PlayerShoot>().multiWeapons == 3) // Running animations for pistol and raygun.
            {
                anim.SetBool("RunPistol", true);
                anim.SetBool("RunRifle", false);
            }
            else
            {
                anim.SetBool("RunRifle", true);
                anim.SetBool("RunPistol", false);
            }
        }

        else
        {
            anim.SetBool("RunRifle", false);
            anim.SetBool("RunPistol", false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RifleBonus"))
            ScreenManager.instance.BonusMessage(true, 1);
        else if (other.gameObject.CompareTag("ShieldBonus"))
            ScreenManager.instance.BonusMessage(true, 2);
        else if (other.gameObject.CompareTag("HealthBonus"))
            ScreenManager.instance.BonusMessage(true, 3);
        else if (other.gameObject.CompareTag("AlienGunBonus"))
            ScreenManager.instance.BonusMessage(true, 4);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RifleBonus"))
            ScreenManager.instance.BonusMessage(false, 1);
        else if (other.gameObject.CompareTag("ShieldBonus"))
            ScreenManager.instance.BonusMessage(false, 2);
        else if (other.gameObject.CompareTag("HealthBonus"))
            ScreenManager.instance.BonusMessage(false, 3);
        else if (other.gameObject.CompareTag("AlienGunBonus"))
            ScreenManager.instance.BonusMessage(false, 4);
    }
}

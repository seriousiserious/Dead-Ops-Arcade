using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] public List<GameObject> enemies;
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public GameObject enemy;
    private int rValue = 0;
    private float /*precValue,*/ rTime;
    private bool setter;

    void Awake()
    {
        instance = this;
        setter = false;
        List<Transform> transforms = new List<Transform>(GameObject.FindWithTag("Spawn").GetComponentsInChildren<Transform>());
        transforms.Remove(GameObject.FindWithTag("Spawn").transform); // Whoever invented GetComponentsInChildren is a fucking idiot
        spawnPoints = transforms.ToArray();
        StartCoroutine("SpawnEnemy");
        
        /*enemies = new GameObject[enemyLength];       Initial implementation, now changed into a dynamic list.
        for(int i=0; i<enemyLength; i++)
        {
            enemies[i] = Instantiate(enemy, new Vector3(0, 0, 0), new Quaternion());
            enemies[i].SetActive(false);
        }*/
    } 

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            rTime = Random.Range(1, 5);
            yield return new WaitForSeconds(rTime);
            if (RoundManager.instance.GetRemainingTime() > 0)
            {
                /*for (int i = 0; i < enemyLength; i++) Initial implementation.
                {
                    if (!enemies[i].activeInHierarchy)
                    {
                        SetUp(enemies[i]);
                        enemyCount++;
                        break;
                    }
                }*/
                enemies.Add(Instantiate(enemy, new Vector3(0, 0, 0), new Quaternion()));
                if(setter == false)
                    SetUp(enemies[enemies.Count - 1], RoundManager.instance.GetCurrentRound());
                else
                    SetUp(enemies[enemies.Count - 1], RoundManager.instance.GetCurrentRound(), setter);
            }
        }
    }

    public void SetUp(GameObject x, int round)
    {
        var Component = x.GetComponent<Zombie>();
        /*x.GetComponent<Zombie>().enabled = true;          All the commented stuff starting from here is no longer needed since the implementation was changed from a static array to a dynamic list. */
        
        Component.SetHealth(100 * round * 0.5F);
        if(round * 0.3F > 2.5F)
            Component.SetSpeed(2.5F); // WE actually don't want them to go faster than this.
        else Component.SetSpeed(round * 0.3F);
        if(SaveGame.GetDifficulty() == 1)
            Component.SetStrength(100F); // One hit death mode.
        else Component.SetStrength(5F * round * 0.5F);

        /*x.tag = "Zombie";
        x.GetComponent<ZombieMovement>().enabled = true;
        x.GetComponent<ZombieHit>().enabled = true;
        x.GetComponent<CapsuleCollider>().enabled = true;
        x.GetComponent<NavMeshAgent>().enabled = true;*/
        
        rValue = Random.Range(1, spawnPoints.Length);
        x.transform.position = spawnPoints[rValue].position;
        x.transform.rotation = spawnPoints[rValue].rotation;
        
        /*x.SetActive(true);*/
    }

    public void SetUp(GameObject x, int round, bool y) // Overload, the difference is the zombies will not harm the player for one whole round.
    {
        var Component = x.GetComponent<Zombie>();
        Component.SetHealth(100 * round * 0.3F);
        if (round * 0.3F > 2.5F)
            Component.SetSpeed(2.5F); // WE actually don't want them to go faster than this.
        else Component.SetSpeed(round * 0.3F);
        Component.SetStrength(0F);
        rValue = Random.Range(1, spawnPoints.Length);
        x.transform.position = spawnPoints[rValue].position;
        x.transform.rotation = spawnPoints[rValue].rotation;
    }

    public void SetGetter(bool x)
    {
        setter = x;
    }
}

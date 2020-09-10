using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField]

    private List<SpawnPointEntity> Points;
    public List<MonsterAI> Monsters;
    public List<HumanAI> Humans;

    public int SpeedOfSpawn = 200;
    public int CountMonster = 10;
    public int CountHuman = 4;

    [SerializeField]
    private GameObject Human;
    [SerializeField]
    private GameObject Monster;

    public Transform ZeroPos;

    private bool ChangeTime = true;
    public float DelaySpawn = 1;

    void Start()
    {
        DelaySpawn = 10;
        Human = GameObject.Find("Human");
        Monster = GameObject.Find("Monster");
        FindSpawnPoints();
        FindHumans(CountHuman);
        FindMonsters(CountMonster);
    }

    private int j = 0;
    private int h = 0;
    private int m = 0;
    

    void FixedUpdate()
    {
        SpawnObject(SpeedOfSpawn);
        if(ChangeTime)
            StartCoroutine(ChangeSpeedOfSpawn());
    }

    //Для игры используется пул объектов, поэтому определённое число ботов создаётся заранее и размещается в недоступной для игрока точке.

    void FindHumans(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            Instantiate(Human, ZeroPos);
        }
        Humans = new List<HumanAI>(FindObjectsOfType<HumanAI>());
    }
    void FindMonsters(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            Instantiate(Monster, ZeroPos);
        }
        Monsters = new List<MonsterAI>(FindObjectsOfType<MonsterAI>());
    }

    //Идёт поиск всех точек для спавна ботов.
    void FindSpawnPoints()
    {
        Points = new List<SpawnPointEntity>(GetComponentsInChildren<SpawnPointEntity>());
    }

    //Поиск точки для спавна одного бота.
    public Vector3 FindPointForSpawn()
    {
        int i = Random.Range(0, 8);
        return Points[i].transform.position;
    }

    //Наугад через определённое время спавнится либо монстр либо человек с разным шансом появления.
    void SpawnObject(int Timer)
    {
        j++;
        if (j >= Timer)
        {
            j = 0;
            int i = Random.Range(0, 11);
            if (i % 5 == 0)
            {
                if (h < Humans.Count)
                {
                    Humans[h].transform.position = FindPointForSpawn();
                    Humans[h].Active = true;
                    h++;
                }
                else
                {
                    h = 0;
                    Humans[h].transform.position = FindPointForSpawn();
                    Humans[h].Active = true;
                }
            } else
            {
                if (m < Monsters.Count)
                {
                    Monsters[m].SetSetting();
                    Monsters[m].transform.position = FindPointForSpawn();
                    Monsters[m].Active = true;
                    m++;
                } else
                {
                    m = 0;
                    Monsters[m].SetSetting();
                    Monsters[m].transform.position = FindPointForSpawn();
                    Monsters[m].Active = true;
                }
            }
        }
    }

    //Спавн ботов со временем происходит быстрее.
    IEnumerator ChangeSpeedOfSpawn()
    {
        ChangeTime = false;
        if (SpeedOfSpawn > 20)
        {
            SpeedOfSpawn -= 10;
            yield return new WaitForSeconds(1f+DelaySpawn);
        } else if (SpeedOfSpawn >= 8)
        {
            SpeedOfSpawn -= 1;
            yield return new WaitForSeconds(2f+DelaySpawn);
        } else if (SpeedOfSpawn > 0)
        {
            yield return null;
        }
        

        ChangeTime = true;
    }
}


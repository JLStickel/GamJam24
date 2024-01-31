using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public int curWave = 1;
    public float wave1Time;
    private float wave1TimeStart;
    public bool finishedCurrent = false;
    public int curObject = 0;
    public List<Spawn> waveSpawnerWave1 = new List<Spawn>();
    public List<Transform> spawnPositions = new List<Transform>();
    public Image waveBar;
    PlayerController player;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        foreach(var item in waveSpawnerWave1)
        {
            wave1Time += item.waitTime;
        }
        wave1TimeStart = wave1Time;
    }

    // Update is called once per frame
    void Update()
    {
        if (curWave == 1)
        {


            wave1Time -= Time.deltaTime;
            waveBar.fillAmount = 1 - (wave1Time / wave1TimeStart);
            waveSpawnerWave1[curObject].waitTime -= Time.deltaTime;
            if(waveSpawnerWave1[curObject].waitTime <= 0)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length <= 4)
                {
                    for (int i = 0; i < waveSpawnerWave1[curObject].spawnedObjects.Count; i++)
                        Instantiate(waveSpawnerWave1[curObject].spawnedObjects[i], spawnPositions[Random.Range(0, spawnPositions.Count - 1)].position, Quaternion.identity);

                    curObject++;
                    if (curObject >= waveSpawnerWave1.Count)
                        curWave = 2;
                }
            }
            if (wave1Time <= 0)
                ChecklastEnemy();
        }
    }

    public void ChecklastEnemy()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length<=1)
        {
            player.shield.SetActive(true);
        }
    }
}

[System.Serializable]
public class Spawn
{
   public float waitTime;
   public List<GameObject> spawnedObjects = new List<GameObject>();
}

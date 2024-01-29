using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int curWave = 1;
    public bool finishedCurrent = false;
    public int curObject = 0;
    public List<Spawn> waveSpawnerWave1 = new List<Spawn>();
    public List<Transform> spawnPositions = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (curWave == 1)
        {
            waveSpawnerWave1[curObject].waitTime -= Time.deltaTime;
            if(waveSpawnerWave1[curObject].waitTime <= 0)
            {
                for (int i = 0; i < waveSpawnerWave1[curObject].spawnedObjects.Count; i++)
                    Instantiate(waveSpawnerWave1[curObject].spawnedObjects[i], spawnPositions[Random.Range(0, spawnPositions.Count - 1)].position, Quaternion.identity);

                curObject++;
                if (curObject >= waveSpawnerWave1.Count)
                    curWave = 2;
            }
        }
    }
}

[System.Serializable]
public class Spawn
{
   public float waitTime;
   public List<GameObject> spawnedObjects = new List<GameObject>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance
    {
        get; private set;
    }
    public List<PatrolPoints> patrolWays = new List<PatrolPoints>();

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Transform> ClosestPatrolPoint(Vector3 position)
    {
        PatrolPoints closestPoint = new PatrolPoints();
        float closestPos = Mathf.Infinity;

        foreach(var item in patrolWays)
        {
            float dist = Vector2.Distance(position, item.middle.position);
            if(dist<closestPos)
            {
                closestPoint = item;
                closestPos = dist;
            }
        }


        return closestPoint.patrolPoints;
    }
}
[System.Serializable]
public class PatrolPoints 
{
    public Transform middle;
    public List<Transform> patrolPoints = new List<Transform>();
}
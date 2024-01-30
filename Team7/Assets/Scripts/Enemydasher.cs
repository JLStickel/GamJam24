using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemydasher : MonoBehaviour
{
    [Header("Important values")]
    public int hp;
    public System.Guid guid;
    public List<GameObject> droppedItems = new List<GameObject>();
    [Header("Movement")]
    public float speed;
    [SerializeField]
    private float minDistToPoint;
    [SerializeField]
    private Transform curPoint;
    private int curPointNum;
    public List<Transform> patrolPoints;
    public enum enemyState
    {
    }
    public enemyState state;
    [SerializeField]
    public float loadUpTime;
    private float loadUpTimeStart;


    [Header("FOV")]
    public float seePlayerRadius;
    [SerializeField] private bool drawGiz;
    [SerializeField] private LayerMask playerMask;
    [SerializeField]
    private GameObject player;


    bool isDashing = false;
    float dashTime;
    float cooldown = 5f;
    public float dashForce = 20;
    public float dashDuration = 0.2f;


    private void Awake()
    {
        loadUpTimeStart = loadUpTime;
        guid = System.Guid.NewGuid();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {

        patrolPoints = EnemyManager.Instance.ClosestPatrolPoint(transform.position);

        curPoint = patrolPoints[curPointNum];
    }

    void Update()
    {
        if (Time.time >= cooldown)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= 100f)
            {
                StartDash();
            }
            cooldown = Time.time + 5f;
        }



        if (hp <= 0)
        {
            foreach (var item in droppedItems)
                Instantiate(item, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (!isDashing)
        {
            
            Vector3 dir = player.transform.position - transform.position;
            transform.position += dir.normalized * speed / 3 * Time.deltaTime;
           
        }

        if (isDashing && Time.time >= dashTime)
        {
            EndDash();
        }

    }

    public void Patrol()
    {
        Vector3 dir = curPoint.position -  transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
        float distanceToPoint = Vector3.Distance(transform.position, curPoint.position);
        if(distanceToPoint < minDistToPoint)
        {
            if (curPointNum >= patrolPoints.Count-1)
            {
                curPointNum = 0;
            }
            else
                curPointNum++;

            curPoint = patrolPoints[curPointNum];
        }
    }

    void StartDash()
    {
        isDashing = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position + (player.transform.position - transform.position).normalized * Mathf.Min(20f, Vector3.Distance(transform.position, player.transform.position));
        dashTime = Time.time + dashDuration;

        StartCoroutine(DashCoroutine(startPos, targetPos));
    }

    IEnumerator DashCoroutine(Vector3 startPos, Vector3 targetPos)
    {
        float elapsedTime = 0f;

        while (Time.time < dashTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        EndDash();
    }


    void EndDash()
    {
        isDashing = false;
    }

}




   
   
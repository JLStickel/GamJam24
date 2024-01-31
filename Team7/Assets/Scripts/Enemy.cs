using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Important values")]
    public int hp =1;
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
        patrol,
        shoot
    }
    public enemyState state;
    [Header("Shoot")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform shootPos;
    public float loadUpTime;
    private float loadUpTimeStart;


    [Header("FOV")]
    public float seePlayerRadius;
    [SerializeField] private bool drawGiz;
    [SerializeField] private LayerMask playerMask;
    [SerializeField]
    private GameObject player;



    private void Awake()
    {
        loadUpTimeStart = loadUpTime;
        guid = System.Guid.NewGuid();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {

        patrolPoints = EnemyManager.Instance.ClosestPatrolPoint(transform.position);

        curPoint = patrolPoints[curPointNum];
    }

    // Update is called once per frame
    void Update()
    {
        if(state == enemyState.patrol)
            Patrol();

        if (state == enemyState.shoot)
            Shoot();

        if (hp <= 0)
        {
            foreach (var item in patrolPoints)
                Instantiate(item, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        FOV();
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

    public void FOV()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, seePlayerRadius, playerMask);
        if(player != null)
        {
            if(state != enemyState.shoot)
            state = enemyState.shoot;
        }
        else
        {
            loadUpTime = loadUpTimeStart;
            state = enemyState.patrol;
        }
    }

    public void Shoot()
    {
        loadUpTime -= Time.deltaTime;
        Vector3 dir = player.transform.position - transform.position;
        transform.position += dir.normalized * speed /3 * Time.deltaTime;
        if (loadUpTime <= 0)
        {
            Vector3 targ = player.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            Instantiate(bullet, shootPos.position, Quaternion.Euler(new Vector3(0, 0, angle)))
                .GetComponent<EnemyBullet>().guid = guid;
            loadUpTime = loadUpTimeStart;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGiz)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, seePlayerRadius);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject != this.gameObject)
            {
                Enemydasher otherEnemy = collision.gameObject.GetComponent<Enemydasher>();
                if (otherEnemy != null)
                {
                    otherEnemy.TakeDamage(1);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}

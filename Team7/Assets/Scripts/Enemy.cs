using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Important values")]
    public int hp =1;
    public System.Guid guid;
    public string guidd;
    public List<GameObject> droppedItems = new List<GameObject>();
    [Header("Movement")]
    public float speed;
    [SerializeField]
    private float minDistToPoint;
    [SerializeField]
    private Transform curPoint;
    private int curPointNum;
    public List<Transform> patrolPoints;
    public Vector3 _patrolDir;
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
    public Vector3 _shootDir;

    [Header("Enemy overlap")]
    public float enemyOverlapRadius;
    public LayerMask enemyMask;
    private Transform closestEnemy;
    private bool moveAway;

    [Header("FOV")]
    public float seePlayerRadius;
    [SerializeField] private bool drawGiz;
    [SerializeField] private LayerMask playerMask;
    [SerializeField]
    private GameObject player;

    [Header("Animation")]
    public Vector3 _animDir;

    Rigidbody2D rb;

    public delegate void _enemyEvents();
    public event _enemyEvents Shot;

    private void Awake()
    {
        loadUpTimeStart = loadUpTime;
        guid = System.Guid.NewGuid();
        guidd = guid.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

        patrolPoints = EnemyManager.Instance.ClosestPatrolPoint(transform.position);

        curPoint = patrolPoints[curPointNum];
        Shot += EmptyFunction;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == enemyState.patrol)
        {
            Patrol();
            _animDir = _patrolDir;
        }

        if (state == enemyState.shoot)
        {
            Shoot();
            _animDir = _shootDir;
        }

        if (hp <= 0)
        {
            foreach (var item in droppedItems)
                Instantiate(item, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        FOV();
        EnemyOverlap();
    }

    public void Patrol()
    {
        _patrolDir = curPoint.position -  transform.position;
        transform.position += _patrolDir.normalized * speed * Time.deltaTime;
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

    public void EnemyOverlap()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, enemyOverlapRadius, enemyMask);
        if(enemies!= null)
        {
            if (enemies.Length > 1)
            {
                float closestDistance = Mathf.Infinity;
                foreach (var item in enemies)
                {
                    float dist = Vector3.Distance(transform.position, item.transform.position);
                    if (dist < closestDistance && item.transform != transform)
                    {
                        closestDistance = dist;
                        closestEnemy = item.transform;
                    }
                }

                Vector3 dir =   closestEnemy.position- transform.position;
                transform.position -= dir.normalized * speed * 1.5f * Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        loadUpTime -= Time.deltaTime;
        _shootDir = player.transform.position - transform.position;

        transform.position += _shootDir.normalized * speed  * Time.deltaTime;
        if (loadUpTime <= 0)
        {
            
            
            if(!Physics2D.Raycast(transform.position, _shootDir, Vector3.Distance(transform.position,player.transform.position), enemyMask))
            {
                SpawnBullet();
            }


            
        }
    }

    public void SpawnBullet()
    {
        Shot();
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        Instantiate(bullet, shootPos.position, Quaternion.Euler(new Vector3(0, 0, angle)))
                .GetComponent<EnemyBullet>().shooterGuid = guid;

        loadUpTime = loadUpTimeStart;
    }

    private void OnDrawGizmos()
    {
        if (drawGiz)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, enemyOverlapRadius);
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

    private void EmptyFunction()
    {

    }
}

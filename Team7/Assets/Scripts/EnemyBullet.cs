using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBullet : MonoBehaviour
{

    public float speed;
    public Guid guid;
    public int damage;
    [SerializeField]
    private float lifeTime = 15;
    private Vector3 curPos;
    private Vector3 oldPos;
    public LayerMask playerMask;
    public LayerMask enemyMask;
    private PlayerController playerC;
    private HealthManager healthM;
    // Start is called before the first frame update
    void Start()
    {
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthM = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position +=transform.right* speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);
        CheckOverlap();
    }

    public void CheckOverlap()
    {
        curPos = transform.position;

        float dist = Vector3.Distance(curPos, oldPos);
        if(Physics2D.Raycast(oldPos,curPos,dist, playerMask))
        {
            PlayerHit();
        }
        
        RaycastHit2D hit= Physics2D.Raycast(oldPos, curPos, dist, enemyMask);
        if (hit == true)
        {
            if (hit.transform.GetComponent<Enemy>() != null && guid != null)
            {
                if (hit.transform.GetComponent<Enemy>().guid != guid)
                {
                    hit.transform.GetComponent<Enemy>().hp -= damage;

                    Destroy(gameObject);
                }
            }
            else
            {
                if (hit.transform.GetComponent<Enemydasher>().guid != guid)
                {
                    hit.transform.GetComponent<Enemydasher>().hp -= damage;

                    Destroy(gameObject);
                }
            }
        }

        oldPos = transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(curPos, oldPos);
    }

    public void PlayerHit()
    {
        if (!playerC.invicibleDash  )
        {
            if (!playerC.isDashing)
            {
                healthM.healthAmount -= damage;
                Destroy(gameObject);
            }
        }
        else
        {
            healthM.healthAmount -= damage;
            Destroy(gameObject);
        }
    }
    
    public void EnemyHit()
    {
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayerHit();
        if(collision.gameObject.CompareTag("Enemy"))
        {
            
            if(collision.gameObject.GetComponent<Enemy>().guid != guid )
            {
                collision.transform.GetComponent<Enemy>().hp -= damage;
                EnemyHit();
            }
        }
    }
}

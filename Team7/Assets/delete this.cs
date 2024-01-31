using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletethis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayerHit();
        if (collision.gameObject.CompareTag("Enemy") && active)
        {


            if (collision.transform.GetComponent<Enemy>() != null)
            {

                collision.transform.GetComponent<Enemy>().hp -= damage;

                Destroy(gameObject);
            }
            else if (collision.transform.GetComponent<Enemydasher>() != null)
            {

                collision.transform.GetComponent<Enemydasher>().hp -= damage;

                Destroy(gameObject);
            }
        }
    }
}

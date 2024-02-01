
using UnityEngine;

public class SmallXP : MonoBehaviour
{
    [Tooltip("Value will be maximally 4 more or 4 less")]
    public int roughValue;
    public float playerRadius;
    public LayerMask playerMask;
    public float speed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position,playerRadius,playerMask))
        {
            Vector3 dir = player.transform.position - transform.position;
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GiveXP();
        }
    }

    public void GiveXP()
    {
        UpgradeManager.Instance.curPlayerXP += Random.Range(roughValue - 4, roughValue + 4);
        UpgradeManager.Instance.setXP = true;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRadius);
    }

}

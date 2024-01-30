using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool canUseShield;
    public bool usingShield;

    public float shieldLoadupTime  = 5f;
    private float shieldLoadupTimeStart;
    public float shieldUsageTime  = .3f;
    private float shieldUsageTimeStart;

    [SerializeField] private GameObject model;

    Collider2D coll;
    private void Awake()
    {
        shieldUsageTimeStart = shieldUsageTime;
        shieldLoadupTimeStart = shieldLoadupTime;
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
        model.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canUseShield)
        {
            usingShield = true;

            coll.enabled = true;
            model.SetActive(true);
        }
            
        if(usingShield)
        {
            shieldUsageTime -= Time.deltaTime;
            if(shieldUsageTime<=0)
            {
                shieldUsageTime = shieldUsageTimeStart;
                usingShield = false;
                canUseShield = false;
                coll.enabled = false;
                model.SetActive(false);
            }
            Vector3 targ = GetMouseWorldPos();
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        if(!canUseShield)
        {
            shieldLoadupTime -= Time.deltaTime;
            if(shieldLoadupTime <= 0)
            {
                shieldLoadupTime = shieldLoadupTimeStart;
                canUseShield = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") && usingShield)
        {
            collision.transform.Rotate(new Vector3(0, 0, 180));
        }
    }

    public Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

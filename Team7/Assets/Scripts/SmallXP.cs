using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallXP : MonoBehaviour
{
    [Tooltip("Value will be maximally 4 more or 4 less")]
    public int roughValue;
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
}

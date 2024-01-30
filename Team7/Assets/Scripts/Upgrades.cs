using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private PlayerController player;
    private HealthManager healthM;
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthM = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>();
        maxHealth = healthM.healthAmount;
    }

    public void SetMaxHP()
    {
        healthM.healthAmount = maxHealth;
    }
    
    public void SetMaxHPAndAdd(float val)
    {
        maxHealth += val;
        healthM.healthAmount = maxHealth;
    }

    public void LongerDashLength()
    {
        player.dashForce *= 1.5f;
    }

    public void IncreaseSpeed()
    {
        player.speed *= 1.3f;
    }

    public void InvincibleDash()
    {
        player.invicibleDash = true;
    }

    public void ActivateShield()
    {
        player.shield.SetActive(true);
    }
}

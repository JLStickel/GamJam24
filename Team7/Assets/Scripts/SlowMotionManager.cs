using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotionManager : MonoBehaviour
{
    public System.Guid guid;
    public Image Slowbar;
    public float dashAmount = 1f;
    private float dashAmountMax;
    public float fillSpeed = 5f;
    public GameObject Player;

    PlayerController playerController;


    public float blueBar = 1f;
    public float targetTime = 1.0f;

    float cooldownTimer = 0f;
    float cooldownDecreaseRate = 1f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<PlayerController>();
        dashAmountMax = dashAmount;
    }
    void Update()
    {
        blueBar -= Time.deltaTime;
        // remove this when game finished
        if (Input.GetKeyDown(KeyCode.H))
        {
            SlowUnload(1f);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            slowLoad(1f);
        }
        if (playerController.targetCooldown <= 0f)
        {

            slowLoad(1f);
            blueBar = 10f;

        }
        if (playerController.targetCooldown > 0f)
        {
            SlowUnload(10f);
        }

        // smoother
        float targetFillAmount = dashAmount / dashAmountMax;
        Slowbar.fillAmount = Mathf.Lerp(Slowbar.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);

    }

    public void SlowUnload(float damage)
    {
        dashAmount -= damage;
        dashAmount = Mathf.Clamp(dashAmount, 0, 10);
    }

    public void slowLoad(float healingAmount)
    {
        dashAmount += healingAmount;
        dashAmount = Mathf.Clamp(dashAmount, 0, 10);
    }
}




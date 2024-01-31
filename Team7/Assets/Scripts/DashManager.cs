using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DashManager : MonoBehaviour
{
    public System.Guid guid;
    public Image dashBar;
    public float dashAmount = 1f;
    private float dashAmountMax;
    public float fillSpeed = 5f;
    public GameObject Player;

    PlayerController playerController;



    public float targetTime = 1.0f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<PlayerController>();
        dashAmountMax = dashAmount;
    }
    void Update()
    {
        // remove this when game finished
        if (Input.GetKeyDown(KeyCode.T))
        {
            Unload(1f);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Load(1f);
        }

        if (!playerController.isDashing && playerController.canDash)
        {
            Load(1f);
        }
        if (playerController.isDashing && !playerController.canDash)
        {
            Unload(1f);
        }
        // smoother
        float targetFillAmount = dashAmount / dashAmountMax;
        dashBar.fillAmount = Mathf.Lerp(dashBar.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);

    }

    public void Unload(float damage)
    {
        dashAmount -= damage;
        dashAmount = Mathf.Clamp(dashAmount, 0, 1);
    }

    public void Load(float healingAmount)
    {
        dashAmount += healingAmount;
        dashAmount = Mathf.Clamp(dashAmount, 0, 1);
    }
}
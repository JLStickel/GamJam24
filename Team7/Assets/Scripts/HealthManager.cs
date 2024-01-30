using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public float fillSpeed = 5f;

    void Update()
    {
        // remove this when game finished
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(20f);
        }

        // smoother
        float targetFillAmount = healthAmount / 100f;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
    }
}
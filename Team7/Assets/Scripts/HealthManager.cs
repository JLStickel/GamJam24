using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public System.Guid guid;
    public Image healthBar;
    public float healthAmount = 3f;
    private float healthAmountMax;
    public float fillSpeed = 5f;
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        healthAmountMax = healthAmount;
    }
    void Update()
    {
        // remove this when game finished
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(1f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(1f);
        }

        // smoother
        float targetFillAmount = healthAmount / healthAmountMax;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);
        if (healthAmount == 0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Destroy(Player);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 3);
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 3);
    }
}
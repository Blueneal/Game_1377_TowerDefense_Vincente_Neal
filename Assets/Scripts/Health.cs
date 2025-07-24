using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }

    public bool IsDead()
    {
        return currentHealth > 0;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        }
        healthText.text = "Health: " + currentHealth;
    }
}

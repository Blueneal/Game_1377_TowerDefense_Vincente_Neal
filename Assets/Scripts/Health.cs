using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int maxHealth = 20;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void Update()
    {
        if (currentHealth <= 0)
        {
            gameManager.GameOver();
        }
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
        healthSlider.value = currentHealth;
    }
}

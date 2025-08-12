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

    /// <summary>
    /// Deletes the enemy object when health reaches 0
    /// </summary>
    public void Update()
    {
        if (currentHealth <= 0)
        {
            gameManager.GameOver();
        }
    }

    /// <summary>
    /// Deals damage to the current health based off of the amount of damage by the enemy
    /// </summary>
    /// <param name="damageAmount"></param>
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        }
        healthSlider.value = currentHealth;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class NPCHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    float currentHealth;

    public Slider slider;

    void Start()
    {
        currentHealth = maxHealth;

        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (slider != null)
            slider.value = currentHealth;

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}

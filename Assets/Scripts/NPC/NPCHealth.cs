using UnityEngine;
using UnityEngine.UI;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int health;
    public Slider healthSlider;

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        healthSlider.value = health;
        if (health <= 0)
            Destroy(gameObject);
    }
}

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;

    public int maxAmmo = 100;
    public int ammo = 100;

    public float maxStamina = 100f;
    public float stamina = 100f;

    public void ChangeHealth(int value)
    {
        health += value;
    }

    public void ChangeAmmo(int value)
    {
        ammo += value;
    }

}

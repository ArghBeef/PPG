using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    Slider staminaSlider;
    public PlayerStats playerStats;

    void Start()
    {
        staminaSlider = this.gameObject.GetComponent<Slider>();
        staminaSlider.maxValue = playerStats.maxStamina;
        staminaSlider.value = playerStats.stamina;
    }

    void Update()
    {
        staminaSlider.value = playerStats.stamina;
    }
}
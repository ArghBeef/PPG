using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public PlayerStats playerStats;


    void Update()
    {
        textUI.text = playerStats.health.ToString();
    }
}

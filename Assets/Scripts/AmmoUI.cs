using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public PlayerStats playerStats;


    void Update()
    {
        textUI.text = playerStats.ammo.ToString();
    }
}

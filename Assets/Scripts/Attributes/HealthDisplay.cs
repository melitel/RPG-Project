using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI healthText;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (health != null && healthText != null)
            {
                // Update the text with the player's health percentage
                //healthText.text = "Health: " + health.GetPercentage().ToString() + "%";
                healthText.text = "Health: " + health.GetHealthPoints() + "/" + health.GetMaxHealthPoints();
            }
        }
    }
}

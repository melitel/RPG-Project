using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Health targetHealth;
        TextMeshProUGUI enemyHealthText;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            enemyHealthText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            targetHealth = fighter.GetTarget();

            if (targetHealth == null)
            {
                enemyHealthText.text = "Enemy: " + "N/A";
                return;
            }

            enemyHealthText.text = "Enemy: " + targetHealth.GetHealthPoints() + "/" + targetHealth.GetMaxHealthPoints();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        TextMeshProUGUI experienceText;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            experienceText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (experience != null && experienceText != null)
            {
                experienceText.text = "XP: " + experience.GetPoints().ToString();
            }
        }
    }
}


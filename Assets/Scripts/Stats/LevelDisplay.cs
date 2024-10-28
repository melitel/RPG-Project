using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats level;
        TextMeshProUGUI levelText;
        private void Awake()
        {
            level = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            levelText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (level != null && levelText != null)
            {
                levelText.text = "Level: " + level.GetLevel().ToString();
            }
        }
    }
}


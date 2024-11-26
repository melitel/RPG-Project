using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI damageText;

        public void UpdateDamageText(float damage)
        {
            damageText.text = damage.ToString("F0");
        }
        public void DestroyText()
        {
            Destroy(gameObject);
        }
    }
}

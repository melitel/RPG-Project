using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] GameObject healthBar;
        [SerializeField] Health healthComponent = null;
        [SerializeField] Canvas rootCanvas = null;
        void Update()
        {
            float healthPercentage = healthComponent.GetPercentage()/100;
            if (Mathf.Approximately(healthPercentage, 0) || 
                Mathf.Approximately(healthPercentage, 1))
            { 
               rootCanvas.enabled = false;
                return;
            }
            rootCanvas.enabled = true;
            healthBar.transform.localScale = new Vector3(healthPercentage, 1, 1);
        }
    }
}

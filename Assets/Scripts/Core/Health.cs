using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {

    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead() 
        { 
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            //print(healthPoints);
            Die();
        }

        private void Die()
        {
            if (healthPoints == 0 && !isDead)
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
        }
    }
}

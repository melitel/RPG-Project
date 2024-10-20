using Newtonsoft.Json.Linq;
using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core {

    public class Health : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        //bool wasDeadLastFrame = false;

        public bool IsDead() 
        { 
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints <= 0) 
            {
                Die();
            }
        }

        private void Die()
        {
            if(isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();

            //if (healthPoints == 0 && !isDead)
            //{
            //    isDead = true;
            //    GetComponent<Animator>().SetTrigger("die");
            //    GetComponent<ActionScheduler>().CancelCurrentAction();
            //}
        }

        public JToken CaptureAsJToken()
        {
            print(healthPoints);
            return JToken.FromObject(healthPoints);
        }

        public void RestoreFromJToken(JToken state)
        {
            healthPoints = state.ToObject<float>();
            print(healthPoints);

            UpdateState();
        }

        private void UpdateState()
        {
            if (healthPoints <= 0) 
            { 
                Die();
            }
        }
    }
}

using Newtonsoft.Json.Linq;
using RPG.Saving;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Stats;
using System.Numerics;
using System;

namespace RPG.Attributes {

    public class Health : MonoBehaviour, IJsonSaveable
    {
        float healthPoints = -1f;        
        bool isDead = false;

        private void Start()
        {
            BaseStats baseStats = GetComponent<BaseStats>();
            if (healthPoints < 0 && baseStats != null)
            {
                healthPoints = baseStats.GetStat(Stat.Health);
            }
            else if (baseStats == null)
            {
                Debug.LogError("BaseStats component not found on the GameObject!", this);
            }
        }

        public bool IsDead() 
        { 
            return isDead;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints <= 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }        

        public float GetPercentage()
        {
            float percentComplete = (float)Math.Round((float)(100 * healthPoints) / GetComponent<BaseStats>().GetStat(Stat.Health));
            return percentComplete;
        }

        private void Die()
        {
            if(isDead) return;

            isDead = true;            

            // Make Rigidbody kinematic to prevent flying off
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }            
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();

            if (experience != null) 
            { 
                float experienceReward = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
                experience.GainExperience(experienceReward);            
            }
        }

        public JToken CaptureAsJToken()
        {
            //print(healthPoints);
            return JToken.FromObject(healthPoints);
        }

        public void RestoreFromJToken(JToken state)
        {
            healthPoints = state.ToObject<float>();
            //print(healthPoints);

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

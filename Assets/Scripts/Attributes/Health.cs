using Newtonsoft.Json.Linq;
using RPG.Saving;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Numerics;
using GameDevTV.Utils;
using RPG.Stats;

namespace RPG.Attributes {

    public class Health : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] UnityEvent<float> takeDamage;
        [SerializeField] UnityEvent onDie;

        BaseStats baseStats;
        LazyValue<float> healthPoints;        
        bool isDead = false;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        { 
            return baseStats.GetStat(Stat.Health);
        }
        private void Start()
        {
            healthPoints.ForceInit();            
        }

        private void OnEnable()
        {
            baseStats.onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            baseStats.onLevelUp -= RegenerateHealth;
        }

        public bool IsDead() 
        { 
            return isDead;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + "took damage: " + damage);

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            if (healthPoints.value <= 0)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
            else 
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetHealthPoints()
        { 
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            float percentComplete = (float)Math.Round((float)(100 * healthPoints.value) / GetComponent<BaseStats>().GetStat(Stat.Health));
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
        private void RegenerateHealth()
        {
            healthPoints.value = baseStats.GetStat(Stat.Health);
        }

        public JToken CaptureAsJToken()
        {            
            return JToken.FromObject(healthPoints.value);
        }

        public void RestoreFromJToken(JToken state)
        {
            healthPoints.value = state.ToObject<float>();
            UpdateState();
        }

        private void UpdateState()
        {
            if (healthPoints.value <= 0) 
            { 
                Die();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using Newtonsoft.Json.Linq;
using System;

namespace RPG.Stats 
{
    public class Experience : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] float experiencePoints = 0;
        
        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }
        public float GetPoints()
        {
            return experiencePoints;
        }
        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(experiencePoints);
        }        

        public void RestoreFromJToken(JToken state)
        {
            experiencePoints = state.ToObject<float>();
        }        
    }

}
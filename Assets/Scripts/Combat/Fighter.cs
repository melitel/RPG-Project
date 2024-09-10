using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        private void Update()
        {
            if (target != null)
            {
                // If the distance is greater than weaponRange, keep moving towards the target
                if (GetDistance() > weaponRange)
                {
                    GetComponent<Mover>().MoveTo(target.position);
                }
                else
                {
                    // Stop the player when within weapon range
                    GetComponent<Mover>().Stop();
                }
            }
        }

        private float GetDistance()
        {
            // Check the distance between the player and the target
            return Vector3.Distance(transform.position, target.position);
        }

        public void Attack(CombatTarget combatTarget) 
        {   
            target = combatTarget.transform;
            print("Take that!");
        }

        public void Cancel() { 
            target = null;
        }
    }
}

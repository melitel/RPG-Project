using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 20f;
        
        Transform target;
        float timeSinceLastAttack = 0;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target != null)
            {                
                // If the distance is greater than weaponRange, keep moving towards the target
                if (GetDistance() > weaponRange)
                {
                    GetComponent<Mover>().MoveToDestination(target.position);
                }
                else
                {
                    // Stop the player when within weapon range
                    GetComponent<Mover>().Cancel();                    
                    AttackBehaviour();
                }
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;                
            }
        }

        //Animation Event
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }

        private float GetDistance()
        {
            // Check the distance between the player and the target
            return Vector3.Distance(transform.position, target.position);
        }

        public void Attack(CombatTarget combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
            print("Take that!");
        }

        public void Cancel() { 
            target = null;
            //GetComponent<Animator>().ResetTrigger("attack");
        }
        
    }
}

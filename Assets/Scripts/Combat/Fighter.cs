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
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform handTransform = null;   
        [SerializeField] Weapon defaultWeapon = null;
        
        Health target;

        float timeSinceLastAttack = Mathf.Infinity;

        Weapon currentWeapon = null;

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            // If the distance is greater than weaponRange, keep moving towards the target
            if (GetDistance() > currentWeapon.GetRange())
            {
                GetComponent<Mover>().MoveToDestination(target.transform.position, 1f);
            }
            else
            {
                // Stop the player when within weapon range
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon) 
        {
            currentWeapon = weapon;
            if (currentWeapon != null)
            {
                Animator animator = GetComponent<Animator>();
                currentWeapon.Spawn(handTransform, animator);            
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {   
            if (target == null) return;
            target.TakeDamage(currentWeapon.GetDamage());            
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }                
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        private float GetDistance()
        {
            // Check the distance between the player and the target
            return Vector3.Distance(transform.position, target.transform.position);
        }

        public void Attack(GameObject combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            //print("Take that!");
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}

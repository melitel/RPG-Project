using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 3f;
        [SerializeField] float lifeAfterImpact = 2f;
        [SerializeField] GameObject[] destroyOnHit = null;
        Health target = null;
        float damage = 0;

        void Start()
        {
            transform.LookAt(GetAimLocation());
        }
        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }


        public void SetTarget(Health target, float dmg)
        {
            this.target = target;
            this.damage = dmg;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) { 
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(damage);

            speed = 0;

            if (hitEffect) 
            {
                GameObject fireTrail = Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            foreach (GameObject toDestroy in destroyOnHit) 
            { 
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}


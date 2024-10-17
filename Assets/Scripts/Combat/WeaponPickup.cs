using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon pickupWeapon = null;
        [SerializeField] float respawnTime = 5f;
        Collider pickupCollider;

        private void Start()
        {
            // Cache the collider
            pickupCollider = GetComponent<Collider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") 
            {
                other.GetComponent<Fighter>().EquipWeapon(pickupWeapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {            
            if (pickupCollider != null) pickupCollider.enabled = shouldShow;                        
            SetChildrenActive(shouldShow);
        }
        
        private void SetChildrenActive(bool isActive)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(isActive);
            }
        }
    }
}


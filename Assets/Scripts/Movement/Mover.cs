using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMeshAvent;

        private void Start()
        {
            navMeshAvent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            UpdateAnimator();
        }

        //public void StartMoveAction(Vector3 destination)
        //{
        //    GetComponent<Fighter>().Cancel();
        //    MoveTo(destination);
        //}
        public void MoveTo(Vector3 destination)
        {
            navMeshAvent.destination = destination;
            navMeshAvent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAvent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAvent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}

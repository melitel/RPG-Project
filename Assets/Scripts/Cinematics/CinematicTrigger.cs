using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics 
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool cinematicTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !cinematicTriggered)
            {
                cinematicTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}

using System;
using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public AgentMoveToHero Follow;

        private void Start()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;

            SwitchFollowOff();
        }

        private void OnTriggerExit(Collider obj) => 
            SwitchFollowOff();

        private void OnTriggerEnter(Collider obj) => 
            SwitchFollowOn();

        private void SwitchFollowOff() =>
            Follow.enabled = false;

        private void SwitchFollowOn() =>
            Follow.enabled = true;
    }
}
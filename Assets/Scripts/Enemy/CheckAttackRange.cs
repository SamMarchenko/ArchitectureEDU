﻿using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public Attack Attack;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;

            Attack.DisableAttack();
        }

        private void OnTriggerExit(Collider obj)
        {
            Attack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj)
        {
            Attack.EnableAttack();
        }
    }
}
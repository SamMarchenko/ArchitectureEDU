using Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.Enemy
{
    public class AgentMoveToHero : Follow
    {
        public NavMeshAgent Agent;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Update() =>
            SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
                Agent.destination = _heroTransform.position;
        }
    }
}
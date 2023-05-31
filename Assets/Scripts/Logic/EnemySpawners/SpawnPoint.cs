using Data;
using Enemy;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        public string Id { get; set; }
        
        
        public bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        public bool Slain => _slain;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
           var monster = _factory.CreateMonster(MonsterTypeId, transform);
           _enemyDeath = monster.GetComponent<EnemyDeath>();
           _enemyDeath.Happend+= Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happend -= Slay;
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }
    }
}